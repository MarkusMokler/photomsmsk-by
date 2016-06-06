module App.Views{
    import Template = Tools.getTemplate;
    import WallModel = Models.WallModel;

    export class WallModal extends Backbone.View<WallModel>{
        constructor(model?) {
            this.template = Template("#wall-popup-view-template");
            this.src = "";
            this.events = {
                "click .uk-modal": "close",
                "click .uk-modal-close": "close",
                "click .uk-modal-dialog": "none",
                "click .role-close-dialog": "close",

                "click .clicl": "click",
                "click .role-like": "like",
                "click .role-dislike": "dislike",
                "click .role-delete-wall-post": "deleteWall",
                "click .role-comment-cancel": "cancelComment",
                "click .role-comment-send": "sendComment",
                "blur .role-comment-area": "hideControlls",
                "focus .role-comment-area": "showControlls",
                "click .uk-comment-body": "showFullViewPost",
            }
            super(model);
        }
        
        template: (...data: any[]) => string;
        src: string;

        initialize (options) {
            this.src = options.src;
        }
        render () {
            this.$el.html(this.template(this.model.toJSON()));
            return this;
        }

        close () {
            this.remove();
        }
        none () {
            return false;
        }
    }
};