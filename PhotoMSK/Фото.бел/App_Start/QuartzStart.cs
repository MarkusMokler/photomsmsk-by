using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Autofac;
using PhotoMSK.Data;
using PhotoMSK.Data.Migrations;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Realisation;
using Quartz;
using Quartz.Impl;

namespace Fotobel
{
    public class QuartzStart
    {
        static readonly Lazy<ISchedulerFactory> SchedFact = new Lazy<ISchedulerFactory>(() => new StdSchedulerFactory());

        public static void Run(IContainer container)
        {
            if (SchedFact.IsValueCreated)
                return;

            var scheduler = container.Resolve<IScheduler>();
            scheduler.Start();


            // define the job and tie it to our HelloJob class
            IJobDetail indexingJob = JobBuilder.Create<SmsJob>()
                .WithIdentity("SmsJob", "group1")
                .Build();

            IJobDetail linkActivityJob = JobBuilder.Create<LinkActivityJob>()
               .WithIdentity("LinkActivityJob", "group1")
               .Build();


            IJobDetail removeLocksJob = JobBuilder.Create<RemoveLocksJob>()
               .WithIdentity("RemoveLocksJob", "group1")
               .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger indexingTrigger = TriggerBuilder.Create()
              .WithIdentity("myTrigger", "group1")
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInSeconds(1)
                  .RepeatForever())
              .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger removeLocksJobTrigger = TriggerBuilder.Create()
              .WithIdentity("removeLocksJobTriger", "group1")
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInSeconds(10)
                  .RepeatForever())
              .Build();

            ITrigger linkActivityJobTrigger = TriggerBuilder.Create()
             .WithIdentity("linkActivityJobTrigger", "group1")
             .StartNow()
             .WithSimpleSchedule(x => x
                 .WithIntervalInSeconds(300)
                 .RepeatForever())
             .Build();


            scheduler.ScheduleJob(indexingJob, indexingTrigger);
            scheduler.ScheduleJob(linkActivityJob, linkActivityJobTrigger);
            scheduler.ScheduleJob(removeLocksJob, removeLocksJobTrigger);
        }
    }

    public class SmsJob : IJob
    {
        private readonly ISmsAssistent _smsAssistent;

        public SmsJob(ISmsAssistent smsAssistent)
        {
            _smsAssistent = smsAssistent;
        }

        public void Execute(IJobExecutionContext context)
        {

            List<SmsMessage> messages;
            using (var appontext = new AppContext())
            {
                messages = appontext.SmsMessages.Take(10).ToList();

                appontext.SmsMessages.RemoveRange(messages);
                appontext.SaveChanges();

            }

            foreach (var message in messages)
            {
                _smsAssistent.SendMessage(message.Phone, message.Message);
            }
        }
    }


    public class RemoveLocksJob : IJob
    {
        private readonly IEventLocker _locker;

        public RemoveLocksJob(IEventLocker locker)
        {
            _locker = locker;
        }

        public void Execute(IJobExecutionContext context)
        {

            _locker.ClearLocks();
        }
    }


    public class LinkActivityJob : IJob
    {

        public void Execute(IJobExecutionContext context)
        {

            List<CallActivity> messages;
            using (var appontext = new AppContext())
            {
                DateTime stime = DateTime.Now;
                var callActivities = appontext.CallActivities
                    .Where(x => x.EventID == null && DbFunctions.DiffMinutes(x.ActivityTime, stime) > 10).ToList();

                var nextDay = DateTime.Today.AddDays(1);

                foreach (var callActivity in callActivities)
                {
                    var activies = appontext.EventActivities.Where(
                        activity =>
                        activity.ActivityTime >= DateTime.Today && activity.ActivityTime < nextDay &&
                        activity.RouteID == callActivity.RouteID &&
                        activity.Event.User.Phones.Select(z => z.Phone.Number)
                        .Contains(callActivity.Description)).ToList();

                    if (activies.Any())
                    {
                        var eventActivity = activies.OrderBy(x => Math.Abs((x.ActivityTime - callActivity.ActivityTime).TotalMinutes)).First();
                        callActivity.Event = eventActivity.Event;

                    }
                    appontext.SaveChanges();
                }
            }
        }
    }

}