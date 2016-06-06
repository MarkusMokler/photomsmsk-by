module App.Views {


    export class WallDayView extends Backbone.View<Backbone.Model>{

        template: (...data: any[]) => string;
        wallCollection: Collections.WallCollection;

        constructor(model?) {
            this.template = Tools.getTemplate("#wall-row-view-template");
            super(model);
        }

        initialize(options?) {
            this.wallCollection = options && typeof options.wallCollection != "undefined" ? options.wallCollection : new Collections.WallCollection();

            this.listenTo(this.wallCollection, "reset", this.addWallPosts);
            this.listenTo(this.wallCollection, "add", this.addWallPost);

        }
        render() {
            this.setElement(this.template());
            return this;
        }

        addWallPosts() {
            this.wallCollection.each(model => this.addWallPost(model));
            return this;

        }
        addWallPost(model) {
            var view = new Views.WallPost({ model: model });
            //this.$el.prepend($("<div class='uk-width-large-1-3 uk-width-medium-1-2 uk-width-small-1-1'></div>").append(view.render().el));
            view.render();

            this.$el.append(view.el);
        }
    }
}