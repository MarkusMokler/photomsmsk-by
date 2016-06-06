module App.Views {
    export class MainWallView extends Backbone.View<Backbone.Model> {
        constructor(model?) {
            this.template = Tools.getTemplate("#main-wall-view-template");
            this.events = {};
            super(model);
        }

        template: any;
        wallCollection: Collections.MainWallCollection;

        initialize(options) {
            this.wallCollection = new Collections.MainWallCollection();

            this.listenTo(this.wallCollection, "reset", this.addWallPosts);
            this.listenTo(this.wallCollection, "add", this.addWallPost);

        }
        render() {
            var self = this;
            this.template().done(data => {
                self.setElement(data);
                self.addWallPosts();
            });
            //   this.setElement(this.template());
            return this;
        }
        reset(data: any) {
            this.wallCollection.reset(data);
        }
        addWallPosts() {
            this.wallCollection.each(model => this.addWallPost(model));
            return this;

        }
        addWallPost(model) {
            var view = new Views.WallDayView();
            var el = view.render().el;
            this.$el.prepend(el);
            view.wallCollection.reset(model.get("items"));
        }
    }
}