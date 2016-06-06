module App.Views {
    import UserInformationModel = Models.UserInformationModel;
    import PhotostudioModel = App.PhotostudioModel;

    export class CallHandlerModel extends Backbone.Model {
        userInformation: UserInformationModel;
        photostudios: Backbone.Collection<PhotostudioModel>;
        phone: string;
    }
    export class UserEventCollection extends Backbone.Collection<Models.EventModel>{
        constructor(model?) {
            this.url = App.makeUrl("/api/UserEvents");
            super(model);
        }

        url: string;
    };

    export class CallHandler extends Backbone.View<CallHandlerModel>{
        constructor(model?) {
            this.el = "body";
            this.eventCollection = new UserEventCollection();
            this.autoItem = Tools.getTemplate("#autocomplete-userinfo-view-template");
            this.newitem = Tools.getTemplate("#autocomplete-new-view-template");
            this.bookingTemplate = Tools.getTemplate("#booking-view-template");
            this.row = Tools.getTemplate("#event-row-view-template");           
            this.newUser = false;
            this.popover = null;   
            this.events = {
                "focus .role-user-information-search": "autofocus",
                "blur .role-user-information-search": "autoblur",
                "click .booking-button": "booking",
                "click .role-hall-tabs button": "changeCalendar",
                "click .role-aditional-show": "showAditional"
            }
            super(model);
        }

        eventCollection: UserEventCollection;
        autoItem: (...data: any[]) => string;
        newitem: (...data: any[]) => string;
        bookingTemplate: (...data: any[]) => string;
        row: (...data:any[]) => string;
        userEventCollection: UserEventCollection;
        validateEventCollection: UserEventCollection;
        newUser: boolean;
        popover: any;
        selectedUser: Models.UserInformationModel;
        calendar: PhotomskCalendar;
        eventSources: any[];

        initialize(options) {
            //  this.listenTo(this.eventCollection, "add", this.addEvent);

            this.validateEventCollection = new Collections.EventCollection();
            this.listenTo(this.validateEventCollection, "add", this.addValidatedEvent);

            this.userEventCollection = new UserEventCollection();
            this.listenTo(this.userEventCollection, "reset", this.userEventCollectionReset);


            if (this.model.userInformation != null) {
                this.newUser = false;
                this.userEventCollection.fetch({ data: { id: this.model.userInformation.id }, reset: true });

                this.selectedUser = this.model.userInformation;
            } else
                this.newUser = true;
        }
        render() {
            var self = this;

            $.each(this.model.photostudios, function () {

                var cal = new App.Views.PhotomskCalendar({ model: this });

                App.owner = this.id;

                self.$('#' + this.shortcut + "-calendar").append(cal.render().el);

                self.listenTo(cal, 'eventAdded', self.eventAdded);
                cal.updateView();
            });

            ///  $('.role-aditional-tags').tagit({});

            return this;
        }

        userEventCollectionReset() {
            $("#events").html('');
            this.userEventCollection.each(model => this.userEventCollectionAdd(model));
            return this;
        }
        userEventCollectionAdd(item) {
            $("#events").append(this.row(item.toJSON()));
        }

        eventAdded(event) {
            this.eventCollection.add(event);
            var self = this;
            $.get(App.makeUrl("/api/Events"),

                { validate: false, start: event.start.format(), end: event.end.format(), calendarIDs: [event.calendarID] }, function (data) {

                    if (data.error) {
                        UIkit.notify({
                            message: data.status,
                            status: 'danger',
                            timeout: 5000,
                            pos: 'bottom-right'
                        });
                        return;
                    }

                    $.each(data, function () {
                        self.validateEventCollection.add(this);
                    });
                });
        }
        addValidatedEvent(event) {
            this.$el.find("#booking").append(this.bookingTemplate(event.toJSON()));
            var price = 0;
            this.validateEventCollection.each(eevent => price += eevent.get("summ"));

            this.$(".role-booking-total-price").html(Tools.formatMoney(price));
        }
        booking() {
            var self = this;
            if (this.newUser) {
                this.selectedUser = new UserInformationModel(
                    {
                        firstName: $("#firstName").val(),
                        lastName: $("#lastName").val(),
                        UserPhone: self.model.phone,
                        admin: true
                    });
            }

            if (typeof self.selectedUser == 'undefined' || self.selectedUser == null) {
                UIkit.notify({
                    message: 'Не введена инфорамция о пользователе',
                    status: 'danger',
                    timeout: 5000,
                    pos: 'top-center'
                });
                return;
            }
            if (this.eventCollection.length == 0) {
                UIkit.notify({
                    message: 'Не выбрано время, бронируемое за человеком =)',
                    status: 'danger',
                    timeout: 5000,
                    pos: 'top-center'
                });
                return;
            }

            var model = new App.Models.BookingModel({ isAdmin: true, UserInformation: self.selectedUser, events: self.eventCollection.toJSON() });
            this.listenTo(model, "sync", this.successSync);
            this.listenTo(model, "error", this.errorSync);
            model.save();
        }
        addNewUserInfo() {
            this.newUser = true;
            this.$el.find("#client-placeholder")
                .html($("#user-information-edit-template").html());
        }

        showAditional(event) {
            $('.role-aditional-form').show();
        }
        successSync() {
            var self = this;

            UIkit.notify({
                message: 'Забронированно',
                status: 'success',
                timeout: 5000,
                pos: 'top-center'
            });
            self.$("#client-placeholder").html('');
            self.$('.role-user-information-search').val('+375');
            self.$('.role-booking-total-price').html('0');
            self.$('#booking').html('');
            self.validateEventCollection.reset();
            self.eventCollection.reset();
            self.newUser = false;
            self.calendar.reset();

        }
        errorSync() {
            UIkit.notify({
                message: 'Невозможно забронировать студию с такими параметрами, проверьте введенную информацию',
                status: 'danger',
                timeout: 5000,
                pos: 'top-center'
            });
        }
    }
}