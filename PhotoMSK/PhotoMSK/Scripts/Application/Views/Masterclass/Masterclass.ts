///<reference path="../../SharedViews/BaseViews/AbstractView.ts"/>
/// <reference path="../../../typings/croppic/Croppic.ts"/>
///<reference path="../../Models/Routes/MasterclassModel.ts"/>

module App.Masterclass {
    import Croppic = CroppicModule.Croppic;

    export class CreateView extends BaseViews.BaseCreateView<MasterclassModel> {
        constructor(model?) {
            this.events = {
                "change input": "changeInput",
                //"change [data-role-input-optional]": "changeInput",
                //"change [data-role-input-required]": "checkRequired",
                "click [data-role-close-modal]": "closeModal",
                "click [data-role-next-step]": "save",
                "click [data-role-prev-step]": "prev",
                "click [data-role-save-phone]": "savePhone",
                "click [data-role-send-to-validate]": "sendToValidate",
                "click [data-role-to-adminpanel]": "redirectToAdminPanel",
                "click [data-role-to-edit]": "redirectToEdit",
                "click [data-role-to-page]": "redirectToMyPage"
            };
            super(model);
        }    

        initialize(options) {

            var model = this.model = new MasterclassModel();
            var self = this;
            this.listenTo(model, "sync", this.success);
            this.listenTo(model, "error", this.error);

            var url = $("#config-settings").attr("data-upload-url") + "Api/Cropper";
            var curl = $("#config-settings").attr("data-upload-url") + "Api/Cropp";

            var cropAvatar = new Croppic('role-avatar',{
                customUploadButtonId: "role-change-avatar",
                cropUrl: curl,
                modal: true,
                width: 400,
                height: 400,
                uploadUrl: url,
                onAfterImgCrop: (data) => {
                    model.set("TeaserImage", data.url);
                }
            });

            var cropCover = new Croppic('role-cover', {
                customUploadButtonId: "role-change-cover",
                cropUrl: curl,
                uploadUrl: url,
                onAfterImgCrop: (data) => {
                    model.set("CoverImage", data.url);
                },
                imgEyecandy: true,
                imgEyecandyOpacity: 0.2,
                /* cropData: {
                     "zoomFactor": 2,
                 }*/

            });


            ymaps.ready(() => {
                var myMap: any;

                myMap = new ymaps.Map("map", {
                    center: [55.76, 37.64],
                    zoom: 9
                });

                myMap.events.add('click', function (e) {
                    // To get the geographical coordinates of the point of click,
                    // call .get('coordPosition')
                    var position = e.get('coords');
                    if (typeof (self.placemark) != 'undefined')
                        self.myMap.geoObjects.remove(self.placemark);

                    self.placemark = new ymaps.Placemark(position);
                    self.model.set("latitude", position[0]);
                    self.model.set("longitude", position[1]);
                    self.myMap.geoObjects.add(self.placemark);

                });
            });

            this.$el.find("[data-role-menu]").find("li").not('.uk-active').addClass("uk-disabled");
        }

        render() {
            this.setElement($("body"));
            this.delegateEvents();
            return this;
        }

        checkRequired(e) {
            this.changeInput(e);

            if (this.model.has('title') && this.model.has('shortcut')) {
                this.$el.find('button.uk-button-next-step').removeAttr("disabled");
            }

        }
    }

    export class EditView extends BaseViews.BaseEditView<MasterclassModel> {
        constructor(model?) {
            this.events = {
                "change [data-role-input-optional]": "changeInput",
                "change [data-role-input-required]": "changeInput",
                "click [data-role-close-modal]": "closeModal",
                "click [data-role-save]": "save",
                "click [data-role-save-phone]": "savePhone",
                "click [data-role-send-to-validate]": "sendToValidate",
                "click [data-role-to-adminpanel]": "redirectToAdminPanel",
                "click [data-role-phone-delete]": "deletePhone",
            };
            super(model);
        }    

        initialize(options) {
            var self = this;
            self.model = new MasterclassModel({ id: options.routeId });
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

        checkRequired(e) {
            if (this.model.has('firstname') && this.model.has('lastname') && this.model.has('shortcut')) {
                this.$el.find('button.uk-button-next-step').removeAttr("disabled");
            }
        }
    }

    export class MasterclassDetails extends Backbone.View<MasterclassModel> {
        constructor(model?) {
            this.el = "body";
            this.bookingTemplate = App.Tools.getTemplate("#booking-view-template");
            this.events = {
                "click .role-hall-tabs button": "changeCalendar",
                "click .booking-button": "booking",
                "click .role-clean-data": "cleanData",
                "click .role-booking-toturial": "showBookingToturial"
            };
            super(model);
        } 

        bookingTemplate: (...data: any[]) => string;

        initialize(options) {
            var self = this;
        }
        render() {
            var view = new App.Views.WallView();
            this.$("#wall-placeholder").html(view.render().el);
            return this;
        }
    }
}  