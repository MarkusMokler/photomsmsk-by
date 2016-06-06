module App.Views {
    import WallModel = App.Models.WallModel;

    export class WallPost extends Backbone.View<WallModel>{
        constructor(model?) {
            this.template = App.Tools.getTemplate("#wall-post-view-template");
            this.tagName = "li";
            this.events = {
                "click .role-like": "like",
                "click .role-dislike": "dislike",
                "click .role-delete-wall-post": "deleteWall",
                "click .role-comment-cancel": "cancelComment",
                "click .role-comment-send": "sendComment",
                "blur .role-comment-area": "hideControlls",
                "focus .role-comment-area": "showControlls",
                "click .uk-comment-body": "showFullViewPost",
            };
            super(model);
        }
        
        template: (...data:any[]) => string;

        initialize(options) { }
        render() {
            var json = this.model.toJSON();
            this.$el.html(this.template(json));
            this.$(".role-comment-buttons").hide();
            return this;
        }

     
        like  (event) {
            var self = this;
            $.post("/vjson/like/post", {
                postID: self.model.id,
                likeType: 1
            }, function (data) {
                self.reviewLike(data);
            });
        }
        dislike (event) {
            var self = this;
            $.post("/vjson/like/post", {
                PostID: self.model.id,
                likeType: -1
            }, function (data) {
                self.reviewLike(data);
            });
        }
        reviewLike (data) {

            this.$el.find('.is-active').removeClass("is-active");
            this.$el.find('.role-like').find("b").html(data.likes);
            this.$el.find('.role-dislike').find("b").html(data.dislikes);

            if (data.opinion == 1) {
                this.$el.find('.role-like').addClass('is-active');
            } else {
                this.$el.find('.role-dislike').addClass('is-active');
            }
        }
        deleteWall (event) {
            var self = this;
            $.post("/vjson/wallpost/delete", {
                ID: self.model.id,
            }, function (data) {
                self.remove();
            });
        }
        cancelComment (event) {

        }
        sendComment (event) {
            var self = this;
            var text = self.$el.find(".role-comment-area").val();
            $.post("/vjson/WallComment/post", {
                ID: self.model.id,
                text: text
            }, function (data) {
                var view = _.template($('#comment-view-template').html());
                var html = view(data);
                var $html = $(html);
                $html.hide();
                self.$(".uk-comment-list").append($html);
                self.$(".role-comment-area").val("");
                $html.slideDown();
                self.$(".role-comment-buttons").hide();
            });
        }
        hideControlls (event) {
            this.$(".role-comment-buttons").slideToggle();
        }
        showControlls (event) {
            this.$(".role-comment-buttons").slideDown();
        }
        showFullViewPost (event) {
            new App.Views.WallModal({ model: this.model }).render().$el.appendTo("body");
        }
    }
}