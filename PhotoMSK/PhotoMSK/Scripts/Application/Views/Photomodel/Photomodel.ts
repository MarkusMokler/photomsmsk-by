///<reference path="../../SharedViews/BaseViews/AbstractView.ts"/>

module App.Views {
    import ModelsCollection = App.Collections.ModelsCollection;
    import Croppic = CroppicModule.Croppic;

    export class CreateView extends BaseViews.BaseCreateView<PhotoModelModel> {
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

        myMap: any;
        placemark: any;

        render() {
            this.setElement($("body"));
            this.delegateEvents();
            return this;
        }

        initialize(options) {

            var model = this.model = new PhotoModelModel();

            this.listenTo(model, "sync", this.success);
            this.listenTo(model, "error", this.error);
            var self = this;

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

            ymaps.ready(function () {
                self.myMap = new ymaps.Map("map", { center: [55.76, 37.64], zoom: 9 });

                self.myMap.events.add('click', function (e) {
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

        redirectToEdit() {
            window.location.href = App.makeUrl("/Admin/Photomodel/Edit?shortcut=" + this.model.get("shortcut"));
        }

        redirectToMyPage() {
            window.location.href = App.makeUrl("/Photomodel/Details?shortcut=" + this.model.get("shortcut"));
        }

        checkRequired(e) {
            this.changeInput(e);

            if (this.model.has('firstname') && this.model.has('lastname') && this.model.has('shortcut')) {
                this.$el.find('[data-role-next-step]').removeAttr("disabled");
            }

        }

    }
    export class EditView extends BaseViews.BaseEditView<PhotoModelModel>
    {
        constructor(model) {
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
            self.model = new PhotoModelModel({ id: options.routeId });
            this.listenTo(this.model, "sync", this.success);
            this.listenTo(this.model, "error", this.error);

            var url = $("#config-settings").attr("data-upload-url") + "Api/Cropper";
            var curl = $("#config-settings").attr("data-upload-url") + "Api/Cropp";

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


            $.each($(".ionRangeSlider"),() => {
                var selector = this;

                var ionoptions: IonRangeSliderOptions =
                    {
                        min: parseInt($(selector).attr('data-min')),
                        max: parseInt($(selector).attr('value')),
                        from: parseInt($(selector).attr('value')),
                        type: 'single',
                        step: 1,
                        postfix: 'см',
                        prettify_enabled: false,
                        onChange: data => {
                            var $this = $(data.input);
                            var id = $this.attr("id");
                            self.model.set(self.toCamelCase(id), data.from_value);
                        }
                    }

                $(selector).ionRangeSlider(ionoptions);

            });
        }

        render() {

            this.setElement($("body"));
            this.delegateEvents();
            return this;
        }

    }

    export class ModelDetails extends Backbone.View<PhotoModelModel>{
        constructor(model?) {
            this.el = "body";
            this.events = {
                "click .clicl": "click"
            }
            super(model);
        }    

        initialize(options) { }
        render() {
            var view = new App.Views.WallView();
            this.$("#wall-placeholder").html(view.render().el);
            // app.WallView = view;
            return this;
        }

        click(event) { }
    }

    export class ModelView extends Backbone.View<PhotoModelModel>{
        constructor(model?) {
            this.template = App.Tools.getTemplate("#model-summary-template");
            this.events = {
                "click .clicl": "click"
            }
            super(model);
        }
        
        template: (...data: any[]) => string;

        initialize(options) { }
        render() {
            this.setElement(this.template(this.model.toJSON()));
            return this;
        }

        click(event) { }
    }

    export class ModelsView extends Backbone.View<PhotoModelModel>{
        constructor(model?) {
            this.events = {

            };
            super(model);
        }
        
        topModelsCollection: App.Collections.ModelsCollection;
        modelsCollection: App.Collections.ModelsCollection;

        initialize(options) {
            this.topModelsCollection = new App.Collections.ModelsCollection();
            this.modelsCollection = new App.Collections.ModelsCollection();

            this.listenTo(this.modelsCollection, "reset", this.addModels);
            this.listenTo(this.modelsCollection, "add", this.addModel);

            this.listenTo(this.topModelsCollection, "reset", this.addTopModels);
            this.listenTo(this.topModelsCollection, "add", this.addTopModel);
        }
        render() {
            return this;
        }

        addModels() {
            this.modelsCollection.each(this.addModel);
            return this;
        }
        addTopModels() {
            this.topModelsCollection.each(this.addTopModel);
            return this;
        }

        addTopModel(model) {

            var view = new ModelView({ model: model });
            $("#top-models").append(view.render().el);

        }
        addModel(model) {
            var view = new ModelView({ model: model });
            $("#models").append(view.render().el);
        }
    }
} 