///<reference path="../../SharedViews/BaseViews/AbstractView.ts"/>
/// <reference path="../../../typings/croppic/Croppic.ts"/>
///<reference path="../../Models/Routes/PhotoshopModel.ts"/>
module App.Models {
    export class RequestData {
        routeId: string;
        brandid: string;
        page = 0;
        categoryid: string;
        order: string;
    }
}

module App.Photoshop {
    import RequestData = App.Models.RequestData;
    import Croppic = CroppicModule.Croppic;

    export class CreateView extends BaseViews.BaseCreateView<PhotoshopModel> {
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
            var self = this;

            var model = this.model = new PhotoshopModel();

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
                        url: "/vjson/RoutePhotos/post",
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

            ymaps.ready(function () {
                self.myMap = new ymaps.Map("map", {
                    center: [55.76, 37.64],
                    zoom: 9
                });

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

        render() {
            this.setElement($("body"));
            this.delegateEvents();
            return this;
        }

        redirectToEdit() {
            window.location.href = "/Admin/Photoshop/Edit?shortcut=" + this.model.get("shortcut");
        }
        redirectToMyPage() {
            window.location.href = "/Photoshop/Details?shortcut=" + this.model.get("shortcut");
        }

        checkRequired(e) {
            this.changeInput(e);

            if (this.model.has('name') && this.model.has('shortcut')) {
                this.$el.find('button.uk-button-next-step').removeAttr("disabled");
            }

        }
    }

    export class EditView extends BaseViews.BaseEditView<PhotoshopModel>
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
            self.model = new PhotoshopModel({ id: options.routeId });
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

    export class Photoshop extends Backbone.View<PhotoshopModel>{
        constructor(model?) {
            this.el = "body";
            this.vitrineCollection = new App.Collections.PhototechnicsCollection();
            this.searchCollection = new App.Collections.PhototechnicsCollection();
            this.requestData = new RequestData();
            this.events = {
                "click .role-booking-button": "booking",
                "click .role-clean-data": "cleanData",
                "click .role-booking-toturial": "showBookingToturial",
                "click .role-category": "sortCategory",
                "click .role-filter": "sortFilter",
                "click .role-paginator-placeholder a": "sortPage",
                "click [data-role-brand-select]": "sortBrand",
                "keyup [data-role-search-input]": "search",
                "click [data-role-search-input]": "showSearch",
                "click [data-role-send-request]": "sendRequest"
            }
            super(model);
        }
        
        vitrineCollection: App.Collections.PhototechnicsCollection;
        searchCollection: App.Collections.PhototechnicsCollection;
        shoppingCart: any;
        private requestData: RequestData;
        

        initialize(options) {
            var self = this;
            this.searchCollection.url = App.makeUrl("/api/search");
            this.requestData = options.requestData || {};
            this.requestData.routeId = App.owner;

            this.listenTo(this.vitrineCollection, "reset", this.resetVitrine);
            this.listenTo(this.vitrineCollection, "add", this.addVitrineTechnic);

            this.listenTo(this.searchCollection, "reset", this.resetSearch);
            this.listenTo(this.searchCollection, "add", this.addSearchTechnic);

            //this.vitrineCollection.reset(this.model.technics.elements);

            this.shoppingCart = this.shoppingCart || Tools.cookieList("shoppingCart");
            $("[data-role-items-count]").html(this.shoppingCart.items().length);
        }
        render() {


            var view = new App.Views.WallView();
            this.$("#wall-placeholder").html(view.render().el);
            App.WallView = view;
            return this;
        }

        closeAll() {
            this.$(".uk-open").removeClass("uk-open");
        }
        resetVitrine() {
            var self = this;

            this.$el.find('.role-vitrine').html('');
            this.$el.find('.role-horizontal-vitrine').html('');

            this.vitrineCollection.each(function (model) {
                return self.addVitrineTechnic(model);
            });
            return this;
        }
        addVitrineTechnic(model) {
            var view = new App.Views.PhototechnicSummary({ model: model });
            view.render();
            this.$el.find('.role-vitrine').append(view.el);

            view = new App.Views.PhototechnicSummary({ model: model, version: 'horizontal', className: "uk-width-1-1" });
            view.render();
            this.$el.find('.role-horizontal-vitrine').append(view.el);
        }
        sortCategory(event) {
            var self = this;

            this.requestData.brandid = null;
            this.requestData.page = 0;
            this.requestData.categoryid = $(event.target).attr("data-categoryid");
            $.get(App.makeUrl("/api/PricePosition"), this.requestData, function (data) { self.fetchSuccess(data); });

        }
        sortBrand(event) {
            var self = this;
            this.requestData.page = 0;
            this.requestData.brandid = $(event.target).attr("data-brand-id");
            $.get(App.makeUrl("/api/PricePosition"), this.requestData, function (data) { self.fetchSuccess(data); });
        }
        sortFilter(event) {
            var self = this;

            this.requestData.order = $(event.target).attr("data-val");
            $.get(App.makeUrl("/api/PricePosition"), this.requestData, function (data) { self.fetchSuccess(data); });
        }
        sortPage(event) {
            var self = this;

            this.requestData.page = +$(event.target).attr("data-page");
            $.get(App.makeUrl("/api/PricePosition"), this.requestData, function (data) { self.fetchSuccess(data); });
        }
        fetchSuccess(data) {
            $('.role-paginator-placeholder').html('');
            for (var i = 0; i < data.pagesCount; i++) {
                $('.role-paginator-placeholder').append('<li><a href="#page-1" data-page="' + i + '">' + i + '</a></li>');
            }
            this.vitrineCollection.reset(data.elements);
        }
        booking() {
            var self = this;


        }
        successSync(model, resp, options) {

        }
        errorSync(model, xhr, options) {
            UIkit.notify({
                message: 'Невозможно забронировать студию с такими параметрами, проверьте введенную информацию',
                status: 'danger',
                timeout: 5000,
                pos: 'bottom-right'
            });
        }
        showSearch(e) {
            $(e.currentTarget).parent().addClass("uk-open");
            $("body").one("click",() => {
                this.closeAll();
            });
        }
        search(e) {
            var self = this;
            var val = $(e.currentTarget).val();
            if (val.length < 3)
                return;

            this.searchCollection.fetch({ data: { routeid: App.owner, search: val }, reset: true });
        }
        resetSearch() {
            this.$el.find('[data-role-search-results-placeholder]').html('');

            this.searchCollection.each(model => this.addSearchTechnic(model));
            return this;
        }
        addSearchTechnic(model) {

            var view = new App.Views.PhototechnicSummary({ model: model, version: 'search', className: "uk-width-1-1" });
            view.render();
            this.$el.find('[data-role-search-results-placeholder]').append(view.el);
        }
        sendRequest() {
            //var client = new $.RestClient(App.makeUrl("/api/"));

            //client.add("callback");

            //client.callback.create({ routeID: App.owner, phone: $("[data-role-pohone-number]").val() });
            //var modal = new UIkit.modal("[data-role-dialog]");
            //modal.hide();
        }
    }
}  