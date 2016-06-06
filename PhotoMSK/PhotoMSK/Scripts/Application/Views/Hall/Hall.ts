///<reference path="../../SharedViews/BaseViews/AbstractView.ts"/>
/// <reference path="../../../typings/croppic/Croppic.ts"/>


module App.Halls {
    import Croppic = CroppicModule.Croppic;

    export class HallModel extends Backbone.Model {
        constructor(model?) {
            super(model);
            this.url = () => {
                return App.makeUrl(`/api/v2/photostudio/${this.get("photostudioShortcut")}/hall`);
            };
        }
        photostudioID: string;
    }


    export class HallListView extends Marionette.CollectionView<HallModel> {
        childView: HallEditSummaryView;
    }

    export class HallCreate extends App.BaseViews.BaseCreateView<HallModel>{
        constructor(model?) {
            this.el = "body";
            this.routeId = null;
            this.events = {
                "change .input-ms": "changeInput",
                "click [data-role-save-button]": "save"
            }
            super(model);
        }

        routeId: string;

        initialize(options) {
            this.model = new HallModel({ photostudioID: options.photostudioID, photostudioShortcut: options.photostudioShortcut });

            this.listenTo(this.model, "sync", this.success);
            this.listenTo(this.model, "error", this.error);
        }
        render() {
            return this;
        }
        success(data: any) {
            this.routeId = data.id;

            $('.error').removeClass('error');

            this.$el.find(".role-step-1").hide();
            this.$el.find(".role-step-2").show();
        }
    };

    export class HallEdit extends BaseViews.BaseEditView<HallModel>{
        constructor(model?) {
            this.el = "body";
            this.events = {
                "change .input-ms": "changeInput",
                "click .role-save-button": "save"
            }
            super(model);
        }

        initialize(options: any) {
            var self = this;

            var url = $("#config-settings").attr("data-upload-url") + "Api/Cropper";
            var curl = $("#config-settings").attr("data-upload-url") + "Api/Cropp";

            var cropCover = new Croppic('role-cover', {
                customUploadButtonId: "role-change-cover",
                cropUrl: curl,
                uploadUrl: url,
                onAfterImgCrop: function (data) {
                    self.updateCover(data.url);
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
                onAfterImgCrop: function (data) {
                    self.updateAvatar(data.url);
                }
            });
        }
        render() {
            return this;
        }
    };

    export class HallEditSummaryView extends Marionette.ItemView<HallModel> {

    }
}
 