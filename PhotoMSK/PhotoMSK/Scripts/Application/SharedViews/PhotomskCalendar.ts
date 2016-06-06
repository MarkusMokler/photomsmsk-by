///<reference path="../Models/CalendarModel.ts"/>
///<reference path="../Tools.ts"/>
///<reference path="../../typings/fullCalendar/fullCalendar.d.ts"/>

module App.Views {
    import CalendarModel = Models.CalendarModel;

    export class PhotomskCalendarOptions implements Backbone.ViewOptions<Models.CalendarModel> {
        model: Models.CalendarModel;
        calendarOption: any;
    }

    /*
    * Events:
    * eventAdded({event}); trigered when event added to calendar.
    *
    */
    export class PhotomskCalendar extends Backbone.View<Models.CalendarModel>{
        constructor(model?) {
            this.tmpEvent = null;
            this.template = Tools.getTemplate("#calendar-view-template");
            this.src = "";
            this.events = {
                "click .role-close-dialog": "close",
                "click .role-hall-tabs button": "changeCalendar"
            }
            super(model);
        }

        src: string;
        template: (...data: any[]) => string;
        tmpEvent: any;
        arr: any[];
        eventSources: any[];
        options: PhotomskCalendarOptions;
        calendar: JQuery;
        popover: AdminEventPopover;

        initialize(options: PhotomskCalendarOptions) {
            this.eventSources = this.getCalendars();
            this.options = options;
        }
        render(): PhotomskCalendar {
            var self = this;

            this.$el.html(this.template(this.model));

            if (this.model.halls.length > 1) {
                var tmp = _.template($("#calendar-header-view-template").html());

                this.$(".calendar-header-placeholder").append(tmp({ halls: this.model.halls }));
            }

            this.eventSources = this.getCalendars();
            this.calendar = this.$('.calendar-placeholder');

            this.calendar.fullCalendar($.extend(this.options.calendarOption || {}, {
                defaultView: 'agendaWeek',
                lang: "ru",
                slotMinutes: 30,
                allDaySlot: false,
                selectable: true,
                editable: true,
                firstDay: 1,
                select(start, end, allDay) {
                    var event = {
                        title: "Бронирование",
                        start: start,
                        end: end,
                        color: "red",
                        className: "role-add-booking"
                    };

                    self.tmpEvent = event;
                    self.addEvent();
                    self.calendar.fullCalendar('renderEvent', event, true);
                    self.calendar.fullCalendar('unselect');

                },
                eventClick: function (calEvent, jsEvent, view) {
                    self.calendarEventClick(calEvent, jsEvent, view);
                },
                eventDrop: function (event, delta, revertFunc) {
                    self.updateEvent(event, delta, revertFunc);
                },
                eventResize: function (event, delta, revertFunc) {
                    self.updateEvent(event, delta, revertFunc);
                },
                columnFormat: {
                    month: 'ddd',
                    week: '%D/% #ddd/#',
                    day: '(#)dddd(/#) M/d'
                },
                eventRender: function (event, element) {
                    if (event.title == 'online') {
                        element.find('.fc-title').html('<span class="uk-badge uk-badge-warning">Онлайн</span>');
                    }
                    if (event.tags != null && typeof (event.tags) != "undefined" && event.tags != '') {
                        var tags = event.tags.split(',');
                        _.each(tags, tag => {
                            element.find('.fc-content').append('<span class="uk-badge uk-badge-warning">' + tag + '</span>');
                        });
                    }
                },
                eventSources: self.eventSources
            }));
            return this;
        }

        getCalendars(id?) {
            var arr = [];
           
            if (typeof id == 'undefined' || id == null) {

                $.each(this.model.halls, function () {
                    var value = this;
                    arr.push(
                        {
                            id: value.calendarID,
                            url: App.makeUrl("/api/Events"),
                            type: 'GET',
                            data: {
                                calendarId: value.calendarID,
                            },
                            error: function () {
                                alert('there was an error while fetching events!');
                            },
                            color: value.color, // a non-ajax option
                            textColor: 'black' // a non-ajax option
                        });
                });
            } else {
                $.each(this.model.halls, function () {
                    var value = this;
                    if (value.calendarID !== id)
                        return;

                    arr.push(
                        {
                            id: value.calendarID,
                            url: App.makeUrl('/Api/Events'),
                            type: 'GET',
                            data: {
                                calendarId: value.calendarID
                            },
                            error: function () {
                                alert('there was an error while fetching events!');
                            },
                            color: value.color, // a non-ajax option
                            textColor: 'black' // a non-ajax option
                        });
                });
            }
            return arr;
        }

        changeCalendar(event) {
            $(event.currentTarget).parent().parent().find(".uk-active").removeClass("uk-active");
            $(event.currentTarget).addClass("uk-active");
            var self = this;

            var cid = $(event.currentTarget).attr("data-calendar");

         //   this.calendar.fullCalendar("removeEvents");

            $.each(this.eventSources, (index,value) => {
                self.calendar.fullCalendar("removeEventSource", value);
            });


            this.eventSources = this.getCalendars(cid);

            $.each(this.eventSources, (index,value) => {
                self.calendar.fullCalendar("addEventSource", value);
            });

            this.calendar.fullCalendar("refetchEvents");
        }
        calendarEventClick(calEvent, jsEvent, view) {
            var self = this;

            if (typeof this.popover !== 'undefined')
                this.popover.remove();
            var elem = new AdminEventPopover({ model: calEvent }).render();

            elem.$el.css("top", jsEvent.pageY);
            elem.$el.css("left", jsEvent.pageX - 125);

            this.popover = elem;
            this.listenToOnce(this.popover, "close", function () {
                self.calendar.fullCalendar('refetchEvents');
            });
            this.$el.append(elem.el);
        }
        updateEvent(event, delta, revertFunc) {
            var mmd = new App.Models.EventModel({
                id: event.id
            });

            mmd.save({
                start: event.start,
                end: event.end
            });
        }
        addEvent() {
            if (this.model.halls.length > 1) {
                var model = new CalendarModel();
                this.listenToOnce(model, "sync", this.showCalendarChooser);
                model.fetch({
                    data: { routeID: App.owner, date: this.tmpEvent.start.format() }
                });

            } else {
                this.addToCart(this.model);
            }
        }
        showCalendarChooser(model, options) {
            var view = new CalendarChooserModal({ model: model });

            this.listenToOnce(view, "save", this.addToCart);
            this.listenToOnce(view, "remove", this.remove);

            this.$el.append(view.render().el);
        }
        addToCart(arr) {
            var self = this;

            $.each(arr, function () {
                var event = $.extend(true, {}, self.tmpEvent);
                event.calendarID = this;
                self.trigger('eventAdded', event);
            });
        }
        removeEvent(event) {
            this.tmpEvent = null;
        }
        reset() {
            var self = this;
            self.calendar.fullCalendar("refetchEvents");
            self.calendar.fullCalendar('removeEvents', function (event) {
                return event.className == "role-add-booking";
            });
        }
        updateView() {
            this.calendar.fullCalendar('render');
        }
    }
}