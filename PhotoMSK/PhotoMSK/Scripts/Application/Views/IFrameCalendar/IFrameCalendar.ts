module App.Views {
    
    export class FrameCalendar extends Backbone.View<Backbone.Model>{
        constructor(model?) {
            this.el = "body";
            this.events = {
                "click .role-hall-tabs button": "changeCalendar"
            };
            super(model);
        }
        
        eventSources: any[];
        eventCollection: Collections.EventCollection;
        calendar: JQuery;
        arr: any[];

        initialize(options) { }

    render () {
        var self = this;

        var tmp = _.template($("#calendar-header-view-template").html());

        $(".calendar-header-placeholder").append(tmp({ halls: this.model }));

        this.eventSources = this.getCalendars();

        this.calendar = $("#calendar").fullCalendar({
            defaultView: 'agendaWeek',
            slotMinutes: 30,
            allDaySlot: false,
            selectable: true,
            lang: "ru",
            editable: true,
            firstDay:1,
            select: (start, end, allDay) => {
                var event =new Models.EventModel( {
                    title: "Бронирование",
                    start: start,
                    end: end,
                    color: "red"
                });

                this.eventCollection.add(event);

                this.calendar.fullCalendar('renderEvent', event.toJSON(), true);

                this.calendar.fullCalendar('unselect');

            },
            columnFormat: {
                month: 'ddd',
                week: '%D/% #ddd/#',
                day: '(#)dddd(/#) M/d'
            },

            eventSources: self.eventSources
        });
        return this;
    }

    getCalendars (id?) {

        this.arr = [];
        var self = this;

        if (typeof id == 'undefined' || id == null) {

            $.each(this.model, function (index, value) {
                self.arr.push(
                {
                    id: value.id,
                    url: App.makeUrl('/Api/Events'),
                    type: 'GET',
                    data: {
                        calendarID: value.id,
                    },
                    error() {
                        alert('there was an error while fetching events!');
                    },
                    color: value.color, // a non-ajax option
                    textColor: 'black' // a non-ajax option
                });
            });
        }
        else {
            $.each(this.model, function (index, value) {
                if (value.id != id)
                    return;

                self.arr.push(
                {
                    id: value.id,
                    url: App.makeUrl('/Api/Events'),
                    type: 'GET',
                    data: {
                        calendarID: value.id,
                    },
                    error() {
                        alert('there was an error while fetching events!');
                    },
                    color: value.color, // a non-ajax option
                    textColor: 'black' // a non-ajax option
                });
            });
        }
        return self.arr;
    }
    changeCalendar (event) {
        var self = this;

        $(event.currentTarget).parent().parent().find(".uk-active").removeClass("uk-active");
        $(event.currentTarget).addClass("uk-active");

        var cid = $(event.currentTarget).attr("data-calendar");

        this.calendar.fullCalendar('removeEvents');

        $.each(this.eventSources, function () {
            self.calendar.fullCalendar('removeEventSource', this);
        });

        this.eventSources = this.getCalendars(cid);

        $.each(this.eventSources, function () {
            self.calendar.fullCalendar('addEventSource', this);
        });

        this.calendar.fullCalendar('refetchEvents');
    }
}
}