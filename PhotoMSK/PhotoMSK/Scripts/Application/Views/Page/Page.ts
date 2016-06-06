///<reference path="../../SharedViews/BaseViews/AbstractView.ts"/>
/// <reference path="../../../typings/croppic/Croppic.ts"/>
///<reference path="../../Models/Routes/PageModel.ts"/>

module App.Page {
    import Croppic = CroppicModule.Croppic;

    export class CreateView extends BaseViews.BaseCreateView<PageModel> {
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
            }
            super(model);
        }

        initialize(options) {

            var model = this.model = new PageModel();

            this.listenTo(model, "sync", this.success);
            this.listenTo(model, "error", this.error);

            var url = $("#config-settings").attr("data-upload-url") + "Api/Cropper";
            var curl = $("#config-settings").attr("data-upload-url") + "Api/Cropp";

            var cropAvatar = new Croppic('role-avatar', {
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

            this.$el.find("[data-role-menu]").find("li").not('.uk-active').addClass("uk-disabled");
        }

        render() {
            this.setElement($("body"));
            this.delegateEvents();
            return this;
        }

        checkRequired(e) {
            this.changeInput(e);

            if (this.model.has('name') && this.model.has('shortcut')) {
                this.$el.find('button.uk-button-next-step').removeAttr("disabled");
            }

        }
    }

    export class EditView extends BaseViews.BaseEditView<PageModel>
    {
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
            }
            super(model);
        }
        

        initialize(options) {
            var self = this;
            self.model = new PageModel({ id: options.routeId });
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


    }

    export class PublicDetails extends Backbone.View<PageModel>{
        constructor(model?) {
            this.el = "body";
            this.bookingTemplate = App.Tools.getTemplate("#booking-view-template");
            this.events = {
                "click .role-booking-toturial": "showBookingToturial",
                "click [data-role-show-callback]": "showclaback",
                "click [data-role-send-request]": "sendRequest"
            }
            super(model);
        }
        
        eventCollection: App.Collections.EventCollection;
        validateEventCollection: App.Collections.EventCollection;
        bookingTemplate: (any) => string;

        initialize(options) {
            var self = this;
            //this.eventSources = this.getCalendars();

            this.eventCollection = new App.Collections.EventCollection(),
            this.validateEventCollection = new App.Collections.EventCollection();
            //  this.listenTo(this.eventCollection, "add", this.addEvent);
        }
        render() {
            var view = new App.Views.WallView();

            this.$("#wall-placeholder").html(view.render().el);
            // app.WallView = view;
            return this;
        }

        sendRequest() {
            //var client = new $.RestClient("/api/");

            //client.add("callback");

            //client.callback.create({ routeID: App.owner, phone: $("[data-role-pohone-number]").val() });
            //var modal = UIkit.modal("[data-role-dialog]");
            //modal.hide();
        }
    }
} 