///<reference path="../../SharedViews/BaseViews/AbstractView.ts"/>
/// <reference path="../../../typings/croppic/Croppic.ts"/>
///<reference path="../../Models/Routes/PhotostudioModel.ts"/>
///<reference path="../../../typings/tagit/tagit.d.ts"/>


module App.Views.Photostudio {
    import EventCollection = App.Collections.EventCollection;
    import PhotomskCalendarOptions = App.Views.PhotomskCalendarOptions;
    import Croppic = CroppicModule.Croppic;

    export class CreateView extends App.BaseViews.BaseCreateView<PhotostudioModel> {

        constructor(model?) {
            this.events = {
                "change input": "changeInput",
              //  "change [data-role-input-optional]": "changeInput",
               // "change [data-role-input-required]": "changeInput",
                "click [data-role-close-modal]": "closeModal",
                "click [data-role-next-step]": "save",
                "click [data-role-prev-step]": "prev",
                "click [data-role-send-to-validate]": "sendToValidate",
                "click [data-role-save-phone]": "savePhone",
                "click [data-role-to-adminpanel]": "redirectToAdminPanel",
                "click [data-role-to-edit]": "redirectToEdit",
                "click [data-role-to-page]": "redirectToMyPage"
            }
            super(model);
        }

        initialize(options) {

            var model = this.model = new PhotostudioModel();
            this.listenTo(this.model, "sync", this.success);
            this.listenTo(this.model, "error", this.error);

            var url = $("#config-settings").attr("data-upload-url") + "Api/Cropper";
            var curl = $("#config-settings").attr("data-upload-url") + "Api/Cropp";

            var cropCover = new Croppic('role-cover', {
                customUploadButtonId: "role-change-cover",
                cropUrl: curl,
                uploadUrl: url,
                onAfterImgCrop: data => {
                    model.set("CoverImage", data.url);
                },
                imgEyecandy: true,
                imgEyecandyOpacity: 0.2,
                cropData: {
                    "zoomFactor": 2,
                }

            });

            var cropAvatar = new Croppic('role-avatar', {
                customUploadButtonId: "role-change-avatar",
                cropUrl: curl,
                modal: true,
                uploadUrl: url,
                width: 400,
                height: 400,
                onAfterImgCrop: data => {
                    model.set("TeaserImage", data.url);
                }

            });

            var progressbar = $("#progressbar");
            var bar = progressbar.find('.uk-progress-bar');
            var settings = {
                action: $('#config-settings').attr('data-upload-url') + "/Api/Files", // upload url
                single: false,
                allow: '*.(jpg|jpeg|gif|png)', // allow only images

                loadstart: () => {
                    bar.css("width", "0%").text("0%");
                    progressbar.removeClass("uk-hidden");
                },

                progress: percent => {
                    percent = Math.ceil(percent);
                    bar.css("width", percent + "%").text(percent + "%");
                },

                allcomplete: response => {
                    $.ajax({
                        url: App.makeUrl("/vjson/RoutePhotos/post"),
                        method: "POST",
                        data: { id: model.get("id"), photos: eval(response) }
                    }).done(function () {
                        $.each(eval(response), function () {
                            $(' <li><div class="uk-thumbnail"><img src="' + this.url + '" alt=""><div class="uk-thumbnail-caption uk-text-center">Главное фото</div></div></li>').appendTo('[data-role-picture-placeholder]');
                        })
                        $(this).addClass("done");
                    });
                }
            };

            var select = new UIkit.uploadSelect($("#upload-select"), settings);
            var drop = new UIkit.uploadDrop($("#upload-drop"), settings);

            ymaps.ready(() => {
                this.myMap = new ymaps.Map("map", {
                    center: [55.76, 37.64],
                    zoom: 9
                });

                this.myMap.events.add('click', function (e) {
                    // To get the geographical coordinates of the point of click,
                    // call .get('coordPosition')
                    var position = e.get('coords');
                    if (typeof (this.placemark) != 'undefined')
                        this.myMap.geoObjects.remove(this.placemark);

                    this.placemark = new ymaps.Placemark(position);
                    this.model.set("latitude", position[0]);
                    this.model.set("longitude", position[1]);
                    this.myMap.geoObjects.add(this.placemark);

                });
            });

            this.$el.find("[data-role-menu]").find("li").not(".uk-active").addClass("uk-disabled");


        }
        render() {
            this.setElement($("body"));
            this.delegateEvents();
            return this;
        }

        redirectToEdit() {
            window.location.href = "/Admin/Photostudio/Edit?shortcut=" + this.model.get("shortcut");
        }
        redirectToMyPage() {
            window.location.href = "/Photostudio/Details?shortcut=" + this.model.get("shortcut");
        }

        checkRequired(e) {
            this.changeInput(e);

            if (this.model.has("name") && this.model.has("shortcut")) {
                this.$el.find("button.uk-button-next-step").removeAttr("disabled");
            }

        }
    }

    export class EditView extends BaseViews.BaseEditView<PhotostudioModel>
    {
        constructor(model) {
            this.events = {
                "change input": "changeInput",
                //"change [data-role-input-optional]": "changeInput",
                //"change [data-role-input-required]": "changeInput",
                "click [data-role-close-modal]": "closeModal",
                "click [data-role-save]": "save",
                "click [data-role-save-phone]": "savePhone",
                "click [data-role-send-to-validate]": "sendToValidate",
                "click [data-role-to-adminpanel]": "redirectToAdminPanel",
                "click [data-role-phone-delete]": "deletePhone"
            };
            super(model);
        }

        initialize(options) {
            var self = this;
            self.model = new PhotostudioModel({ id: options.routeId });
            this.listenTo(this.model, "sync", this.success);
            this.listenTo(this.model, "error", this.error);

            var url = $("#config-settings").attr("data-upload-url") + "Api/Cropper";
            var curl = $("#config-settings").attr("data-upload-url") + "Api/Cropp";

            var cropCover = new Croppic('role-cover', {
                customUploadButtonId: "role-change-cover",
                cropUrl: curl,
                uploadUrl: url,
                onAfterImgCrop: data => {
                    self.model.set("CoverImage", data.url);
                },
                imgEyecandy: true,
                imgEyecandyOpacity: 0.2,
                cropData: {
                    "zoomFactor": 2,
                }
            });
            var cropAvatar = new Croppic('role-avatar', {
                customUploadButtonId: "role-change-avatar",
                cropUrl: curl,
                modal: true,
                uploadUrl: url,
                width: 400,
                height: 400,
                onAfterImgCrop: data => {
                    self.model.set("TeaserImage", data.url);
                }
            });
        }

        render() {

            this.setElement($("body"));
            this.delegateEvents();
            return this;
        }

        deletePhoto(evt) {
            var id = $(evt.target).attr("data-id");
            $.ajax({
                url: App.makeUrl('/api/RoutePhotos/' + id),
                type: 'DELETE',
                success: result => {
                    // Do something with the result
                }
            });
        }
    }

    export class StudioDetails extends Backbone.View<PhotostudioModel>{
        requestID: string;
        eventCollection: EventCollection;
        validateEventCollection: EventCollection;
        bookingTemplate: (...data: any[]) => string;
        calendar: Views.PhotomskCalendar;
        selectedUser: Models.UserInformationModel;
        wallView: Views.WallView;

        constructor(model) {
            this.el = "body";
            this.events = {
                "click .role-booking-button": "booking",
                "click .role-clean-data": "cleanData",
                "click .role-booking-toturial": "showBookingToturial"
            };
            this.bookingTemplate = Tools.getTemplate("#booking-view-template");
            super(model);
        }

        initialize(options) {
            this.eventCollection = new Collections.EventCollection();
            this.validateEventCollection = new Collections.EventCollection();
            this.listenTo(this.validateEventCollection, "add", this.addValidatedEvent);
            this.requestID = Tools.guid();
        }
        render() {
            this.wallView = new Views.WallView();

            this.calendar = new Views.PhotomskCalendar(
                {
                    model: this.model,
                    calendarOption: { slotDuration: "00:30", snapDuration: "01:00", eventClick: function (calEvent, jsEvent, view) { } }
                });
            $(".role-booking-calendar").html(this.calendar.render().el);
            this.calendar.updateView();
            this.listenTo(this.calendar, 'eventAdded', this.eventAdded);


            this.$("[data-role-wall-placeholder]").html(this.wallView.render().el);
            return this;
        }

        eventAdded(event) {
            var self = this;
            $.get(App.makeUrl("/api/Events"),

                { validate: true, start: event.start.format(), end: event.end.format(), calendarIDs: [event.calendarID] }, function (data) {

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
        removeEvent(event) { }
        addValidatedEvent(event) {
            this.$el.find("#booking").append(this.bookingTemplate(event.toJSON()));
            var price = +0;
            this.validateEventCollection.each(function (eevent) {
                price += eevent.get('summ');
            });

            this.$('.role-booking-total-price').html(Tools.formatMoney(price));
        }
        booking() {
            var self = this;

            if (this.validateEventCollection.length == 0) {
                UIkit.notify({
                    message: 'Не выбрано время, бронируемое за человеком =)',
                    status: 'danger',
                    timeout: 5000,
                    pos: 'bottom-right'
                });
                return;
            }

            var model = new App.Models.BookingModel({ UserInformation: self.selectedUser, events: self.validateEventCollection.toJSON(), requestID: self.requestID });
            this.listenTo(model, "sync", this.successSync);
            this.listenTo(model, "error", this.errorSync);
            model.save();
        }
        successSync(model, resp, options) {

            UIkit.notify({
                message: 'студия успешно забронирована',
                status: 'info',
                timeout: 5000,
                pos: 'bottom-right'
            });
            this.validateEventCollection.reset();
            this.$el.find("#booking-placeholder").html('');
        }
        errorSync(model, xhr, options) {
            UIkit.notify({
                message: 'Невозможно забронировать студию с такими параметрами, проверьте введенную информацию',
                status: 'danger',
                timeout: 5000,
                pos: 'bottom-right'
            });
        }
        cleanData() {
            this.validateEventCollection.reset();
            this.eventCollection.reset();
            this.$el.find("#booking-placeholder").html('');
            //this.calendar.fullCalendar('removeEvents', function (event) {
            //    return event.className == "role-add-booking";
            //});
        }
        showBookingToturial() {
            (new App.Views.ToturialModal({ src: "//www.youtube.com/embed/Xog1_IqR5Jo" })).render().$el.appendTo("body");
        }
    }

    export class StudioBooking extends Backbone.View<PhotostudioModel> {

        eventCollection: Collections.EventCollection;
        validateEventCollection: Collections.EventCollection;
        calendar: Views.PhotomskCalendar;
        newUser: boolean;
        selectedUser: Models.UserInformationModel;
        autoItem: (...data: any[]) => string;
        newitem: (...data: any[]) => string;
        bookingTemplate: (...data: any[]) => string;
        popover: any;

        constructor(model) {
            this.events = {
                "focus .role-user-information-search": "autofocus",
                "blur .role-user-information-search": "autoblur",
                "click .booking-button": "booking",
                "click .role-hall-tabs button": "changeCalendar",
                "click .role-aditional-show": "showAditional"
            }
            this.el = "body";
            this.autoItem = Tools.getTemplate("#autocomplete-userinfo-view-template");
            this.newitem = Tools.getTemplate("#autocomplete-new-view-template");
            this.bookingTemplate = Tools.getTemplate("#booking-view-template");
            super(model);
        }

        initialize(options) {
            this.eventCollection = new App.Collections.EventCollection();
            this.validateEventCollection = new App.Collections.EventCollection();
            // this.listenTo(this.eventCollection, "add", this.addEvent);
            this.listenTo(this.validateEventCollection, "add", this.addValidatedEvent);
            var msko = new PhotomskCalendarOptions();
            msko.model = this.model;
            this.calendar = new App.Views.PhotomskCalendar(msko);
        }
        render() {

            var self = this;

            var auto = this.$el.find(".role-user-information-search").autocomplete({
                appendTo: this.$el.find(".uk-dropdown-search"),
                source: (request, respond) => {
                    $.post("/vjson/Search/FindByPhone", { search: request.term },
                        response => {
                            if (response.length === 0) {
                                self.addNewUserInfo();
                            }
                            respond(response);
                        });
                },
                minLength: 2,
                select(event, ui) {
                    self.selectItem(ui.item);
                }
            });

            auto.data("ui-autocomplete")._renderMenu = function (ul, item) {
                return self.renderMenu(ul, item, this);
            };

            auto.data("ui-autocomplete")._renderItem = (ul, item) => this.renderAutocompleteItem(ul, item);

            $(".ui-autocomplete").attr("class", "uk-nav uk-nav-autocomplete uk-autocomplete-results");


            $('.role-booking-calendar').html(this.calendar.render().el);
            this.calendar.updateView();
            this.listenTo(this.calendar, 'eventAdded', this.eventAdded);

            //  $(".role-aditional-tags").tagit({});

            return this;
        }
        renderAutocompleteItem(ul, item) {
            return $(this.autoItem(item)).appendTo(ul);
        }
        renderMenu(ul, items, auto) {
            var self = this;
            $.each(items, function (index, item) {
                auto._renderItemData(ul, item);
            });
            $(ul).append(self.newitem());
        }
        autofocus() {
            $(".uk-dropdown-search").show();
        }
        autoblur() {
            $(".uk-dropdown-search").hide();
        }
        selectItem(item) {
            this.newUser = false;
            this.selectedUser = new App.Models.UserInformationModel({
                userFirstName: item.firstName,
                userlastName: item.lastName,
                userPhone: item.userPhone,
                admin: true
            });

            this.$el.find("#client-placeholder")
                .html(new App.Views.UserInformation({ model: item }).render().el);
        }

        eventAdded(event) {
            this.eventCollection.add(event);
            var self = this;
            $.get(App.makeUrl("/api/Events"),

                { validate: false, start: event.start.format(), end: event.end.format(), calendarIDs: [event.calendarID] }, data => {

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
            this.validateEventCollection.each(function (eevent) {
                price += eevent.get('summ');
            });

            this.$('.role-booking-total-price').html(Tools.formatMoney(price));
        }
        booking() {
            var self = this;
            if (this.newUser) {
                this.selectedUser = new App.Models.UserInformationModel(
                    {
                        firstName: $("#firstName").val(),
                        lastName: $("#lastName").val(),
                        userPhone: $(".role-user-information-search").val(),
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
            self.$(".role-user-information-search").val('+375');
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