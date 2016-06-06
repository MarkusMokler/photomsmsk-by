module App.Views {
    export class CalendarModal extends Backbone.View<Backbone.Model> {
        constructor(model?) {
            super(model);
            this.template = Tools.getTemplate("#calendar-modal-view-template");
            this.src = "";
            this.events = {
                "click .uk-modal": "close",
                "click .uk-modal-close": "close",
                "click .uk-modal-dialog": "none",
                "click .role-close-dialog": "close"
            }
        }
        
        template: (...data:any[]) => string;
        eventCollection: Collections.EventCollection;
        validateEventCollection: Collections.EventCollection;
        calendar:PhotomskCalendar;
        src: string;

       initialize(options) {
           this.eventCollection = new App.Collections.EventCollection();
               this.validateEventCollection = new App.Collections.EventCollection();
           //     this.listenTo(this.eventCollection, "add", this.addEvent);
       }
        render() {
            var self = this;
            this.$el.html(this.template(this.model.toJSON()));
            this.calendar = new App.Views.PhotomskCalendar({ model: this.model.get('rents') });

            this.$('.role-booking-calendar').html(this.calendar.render().el);
            this.calendar.updateView();
        //    this.listenTo(this.calendar, 'eventAdded', this.eventAdded);

            //      var tmp = _.template($("#rentcalendar-header-view-template").template());
            //    this.$(".calendar-header-placeholder").append(tmp({ rents: this.model.get('rents') }));
            return this;
        }

        //addToCart(event) {
        //    var self = this;

        //    $.get("/api/Events", {
        //        validate: true,
        //        start: event.get("start").format(),
        //        end: event.get("end").format(),
        //        calendarIDs: event.get("calendarIDs")
        //    }, function (data) {

        //            if (data.error) {
        //                UIkit.notify({
        //                    message: data.status,
        //                    status: 'danger',
        //                    timeout: 5000,
        //                    pos: 'bottom-right'
        //                });
        //                return;
        //            }

        //            $.each(data, function () {
        //                this.className = 'role-add-booking';
        //                self.calendar.fullCalendar('renderEvent', this, true);
        //                self.$el.find("#booking-placeholder").append(self.bookingTemplate(this));
        //                self.validateEventCollection.add(this);
        //            });
        //        });

        //}
        //booking() {
        //    var self = this;
        //    if (this.validateEventCollection.length == 0) {
        //        UIkit.notify({
        //            message: 'Не выбрано время, бронируемое за человеком =)',
        //            status: 'danger',
        //            timeout: 5000,
        //            pos: 'bottom-right'
        //        });
        //        return;
        //    }
        //    this.validateEventCollection.each(function (model, index) {
        //        model.save(model.toJSON(), {
        //            success: function (model) {
        //                UIkit.notify({
        //                    message: 'Забронированно с ' + new moment(model.get("start")).format("lll") + " по " + new moment(model.get("start")).format("lll"),
        //                    status: 'info',
        //                    timeout: 5000,
        //                    pos: 'bottom-right'
        //                });
        //                $('#calendar').fullCalendar("refetchEvents");

        //                self.validateEventCollection.reset();
        //                self.$el.find("#booking-placeholder").html('');
        //            },
        //            error: function (mdel) {
        //                UIkit.notify({
        //                    message: 'Невозможно забронировать студию с такими параметрами, проверьте введенную информацию',
        //                    status: 'danger',
        //                    timeout: 5000,
        //                    pos: 'bottom-right'
        //                });
        //            }
        //        });
        //    });
        //    this.calendar.fullCalendar('removeEvents', function (event) {
        //        return event.className == "role-add-booking";
        //    });

        //}
        //cleanData() {
        //    this.validateEventCollection.reset();
        //    this.eventCollection.reset();
        //    this.$el.find("#booking-placeholder").html('');
        //    this.calendar.fullCalendar('removeEvents', function (event) {
        //        return event.className == "role-add-booking";
        //    });
        //}

        close() {
            this.remove();
            this.trigger("close", this.model);
        }
        none() {
            return false;
        }
    }
}