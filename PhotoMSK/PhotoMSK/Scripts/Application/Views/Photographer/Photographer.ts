///<reference path="../../SharedViews/BaseViews/AbstractView.ts"/>
/// <reference path="../../../typings/croppic/Croppic.ts"/>
/// <reference path="../../../typings/photosetGrid/photosetGrid.d.ts"/>
///<reference path="../../Models/Routes/PhotographerModel.ts"/>
///<reference path="../../Models/Routes/PhoneCheckModel.ts"/>
///<reference path="../../Models/Routes/ConfirmModel.ts"/>

module App.Photographer {
    import Croppic = CroppicModule.Croppic;

    export class CreateView extends BaseViews.BaseCreateView<PhotographerModel> {
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

            var model = this.model = new PhotographerModel();

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

        redirectToEdit() {
            window.location.href = App.makeUrl(`/Admin/Photographer/Edit?shortcut=${this.model.get("shortcut") }`);
        }

        redirectToMyPage() {
            window.location.href = App.makeUrl("/Photographer/Details?shortcut=" + this.model.get("shortcut"));
        }

        checkRequired(e) {
            this.changeInput(e);

            if (this.model.has('firstname') && this.model.has('lastname') && this.model.has('shortcut')) {
                this.$el.find('button.uk-button-next-step').removeAttr("disabled");
            }

        }
    }

    export class EditView extends BaseViews.BaseEditView<PhotographerModel>
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
            self.model = new PhotographerModel({ id: options.id });

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

    export class PhotographerDetails extends Backbone.View<PhotographerModel>{
        constructor(model?) {
            this.el = "body";
            this.bookingTemplate = App.Tools.getTemplate("#booking-view-template");
            this.events = {
                "click .role-switch-tabs a": "rebuildGrid"
            }
            super(model);
        }

        bookingTemplate: (...data: any[]) => string;

        initialize(options) { }
        render() {
            var view = new App.Views.WallView();

            this.$("#wall-placeholder").html(view.render().el);
            App.WallView = view;
            return this;
        }
        rebuildGrid() {
            $('.photoset-grid-basic').each(function () {
                $(this).photosetGrid({
                    highresLinks: true,
                    rel: App.Tools.guid(),
                    gutter: '5px',
                    onComplete: function () {
                        $('.photoset-grid-basic a').colorbox({
                            photo: true,
                            scalePhotos: true,
                            maxHeight: '90%',
                            maxWidth: '90%'
                        });
                    }
                });
            });
        }
    }

    export class PhotographerIndex extends Backbone.View<PhotographerModel>{
        constructor(model?) {
            this.el = 'body';
            this.events = {

            };
            super(model);
        }

        element: string;
        Photographers: App.Collections.PhotographersCollection;

        initialize(options) {
            this.Photographers = new App.Collections.PhotographersCollection();

            this.listenTo(this.Photographers, "reset", this.addPhotographers);
            this.listenTo(this.Photographers, "add", this.addPhotographer);
            $('[data-uk-pagination]').on('uk-select-page', (e, pageIndex) =>
                this.loadPage(e, pageIndex));
        }
        render() {
            return this;
        }

        addPhotographers() {
            $(".role-photographers").html("");
            this.Photographers.each(this.addPhotographer);
            return this;
        }
        addPhotographer(model) {
            var view = new PhotographerView({ model: model });
            $(".role-photographers").append(view.render().el);

        }
        loadPage(e, pageIndex) {
            this.Photographers.fetch({
                data: {
                    from: pageIndex * 20,
                    pageSize: 20
                },
                reset: true
            });
        }

    }

    export class PhotographerView extends Backbone.View<PhotographerModel>{
        constructor(model?) {
            this.template = App.Tools.getTemplate("#photographer-summary-template");
            this.events = {
                "click .clicl": "click"
            }
            super(model);
        }

        template: (any) => string;

        initialize(options) { }
        render() {
            this.setElement(this.template(this.model.toJSON()));
            return this;
        }

        click(event) { }
    }
} 