///<reference path="../../SharedViews/BaseViews/AbstractView.ts"/>
/// <reference path="../../../typings/croppic/Croppic.ts"/>
///<reference path="../../Models/Routes/PhotorentModel.ts"/>

module App.Photorent {
    import PhotorentModel = App.Models.PhotorentModel;
    import Models = App.Models;
    import Croppic = CroppicModule.Croppic;

    export class CreateView extends BaseViews.BaseCreateView<PhotorentModel> {
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

            var model = this.model = new PhotorentModel();

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

            this.$el.find("[data-role-menu]").find("li").not('.uk-active').addClass("uk-disabled");
        }

        render() {
            this.setElement($("body"));
            this.delegateEvents();
            return this;
        }

        redirectToEdit() {
            window.location.href = App.makeUrl("/Admin/Photorent/Edit?shortcut=" + this.model.get("shortcut"));
        }

        redirectToMyPage() {
            window.location.href = App.makeUrl("/Photorent/Details?shortcut=" + this.model.get("shortcut"));
        }

        checkRequired(e) {
            this.changeInput(e);

            if (this.model.has('name') && this.model.has('shortcut')) {
                this.$el.find('[data-role-next-step]').removeAttr("disabled");
            }

        }
    }

    export class EditView extends BaseViews.BaseEditView<PhotorentModel>
    {
        constructor(model) {
            this.events = {
                "change [data-role-input-optional]": "changeInput",
                "change [data-role-input-required]": "changeInput",
                "click [data-role-close-modal]": "closeModal",
                "click [data-role-save]": "next",
                "click [data-role-save-phone]": "savePhone",
                "click [data-role-send-to-validate]": "sendToValidate",
                "click [data-role-to-adminpanel]": "redirectToAdminPanel",
                "click [data-role-phone-delete]": "deletePhone"
            };
            super(model);
        }


        initialize(options) {
            var self = this;
            self.model = new PhotorentModel({ id: options.routeId });
            this.listenTo(this.model, "sync", this.success);
            this.listenTo(this.model, "error", this.error);

            var url = $("#config-settings").attr("data-upload-url") + "Api/Cropper";
            var curl = $("#config-settings").attr("data-upload-url") + "Api/Cropp";

            var cropCover = new Croppic('role-cover', {
                customUploadButtonId: "role-change-cover",
                cropUrl: curl,
                uploadUrl: url,
                onAfterImgCrop(data) {
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
                onAfterImgCrop(data) {
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

    export class PhotorentDetails extends Backbone.View<PhotorentModel>{
        constructor(model?) {
            this.el = "body";
            this.technics = new App.Collections.PhototechnicsCollection();
            this.searchCollection = new App.Collections.PhototechnicsCollection();
            this.requestData = new App.Models.RequestData();
            this.bookingTemplate = App.Tools.getTemplate("#booking-view-template");
            this.events = {
                "click .role-hall-tabs button": "changeCalendar",
                "click .booking-button": "booking",
                "click .role-clean-data": "cleanData",
                "click .role-booking-toturial": "showBookingToturial",
                "click .role-show-calendar": "showModalCalendar",
                "click .role-category": "sortCategory",
                "click .role-filter": "sortFilter",
                "click .role-paginator-placeholder a": "sortPage",
                "click [data-role-brand-select]": "sortBrand",
                "keyup [data-role-search-input]": "search",
                "click [data-role-search-input]": "showSearch"
            };
            super(model);
        }

        el: string;
        private requestData: App.Models.RequestData;
        bookingTemplate: (...data: any[]) => string;
        technics: App.Collections.PhototechnicsCollection;
        searchCollection: App.Collections.PhototechnicsCollection;

        initialize(options) {
            var self = this;
            this.searchCollection.url = App.makeUrl("/api/search");
            this.requestData = options.requestData || {};
            this.requestData.routeId = App.owner;

            this.listenTo(this.technics, 'reset', this.resetTechnics);
            this.listenTo(this.technics, "add", this.addTechnic);

            this.listenTo(this.searchCollection, "reset", this.resetSearch);
            this.listenTo(this.searchCollection, "add", this.addSearchTechnic);
        }
        render() {
            var view = new App.Views.WallView();
            this.technics.reset(this.model.technics.elements);

            this.$("#wall-placeholder").html(view.render().el);
            App.WallView = view;
            return this;
        }

        selectCategory(event) {
            var categoryID = $(event.target).attr('data-categoryid');

            $.get(App.makeUrl("/api/rentcalendar/"), { categoryID: categoryID }, data => this.technics.reset(data.elements));
        }
        fetchSuccess(data) {
            $('.role-paginator-placeholder').html('');
            for (var i = 0; i < data.pagesCount; i++) {
                $('.role-paginator-placeholder').append('<li><a href="#page-1" data-page="' + i + '">' + i + '</a></li>');
            }
            this.technics.reset(data.elements);
        }
        sortCategory(event) {
            this.requestData.brandid = null;
            this.requestData.page = 0;
            this.requestData.categoryid = $(event.target).attr("data-categoryid");
            $.get(App.makeUrl("/api/RentPosition"), this.requestData, data => { this.fetchSuccess(data); });

        }
        sortBrand(event) {
            this.requestData.page = 0;
            this.requestData.brandid = $(event.target).attr("data-brand-id");
            $.get(App.makeUrl("/api/RentPosition"), this.requestData, data => { this.fetchSuccess(data); });
        }
        sortFilter(event) {
            this.requestData.order = $(event.target).attr("data-val");
            $.get(App.makeUrl("/api/RentPosition"), this.requestData, data => { this.fetchSuccess(data); });
        }
        sortPage(event) {
            this.requestData.page = +$(event.target).attr("data-page");
            $.get(App.makeUrl("/api/RentPosition"), this.requestData, data => { this.fetchSuccess(data); });
        }
        addTechnic(item) {
            var placeholder = this.$('[data-role-PhototechnicsViewModel-placeholder]');
            (new App.Views.PhototechnicSummary({ model: item })).render().$el.appendTo(placeholder);
        }
        resetTechnics() {
            this.$('[data-role-PhototechnicsViewModel-placeholder]').html("");
            this.technics.each(this.addTechnic);
            return this;
        }

        closeAll() {
            this.$(".uk-open").removeClass("uk-open");
        }

        showSearch(e) {
            $(e.currentTarget).parent().addClass("uk-open");
            $("body").one("click", () => this.closeAll());
        }
        search(e) {
            var self = this;
            var val = $(e.currentTarget).val();
            if (val.length < 3)
                return;

            this.searchCollection.fetch({ data: { routeId: App.owner, search: val }, reset: true });
        }
        resetSearch() {
            this.$el.find('[data-role-search-results-placeholder]').html('');

            this.searchCollection.each(model => this.addSearchTechnic(model));
            return this;
        }
        addSearchTechnic(model) {

            var view = new App.Views.PhototechnicSummary({ model: model, version: "search", className: "uk-width-1-1" });
            view.render();
            this.$el.find('[data-role-search-results-placeholder]').append(view.el);
        }

    }
    export class PhotorentView extends Backbone.View<Backbone.Model>{
        constructor(model?) {
            this.events = {
                "click #clicl": "click"
            }
            super(model);
        }

        initialize(options) { }
        render() {
            return this;
        }


        click(event) { }
    }
}  