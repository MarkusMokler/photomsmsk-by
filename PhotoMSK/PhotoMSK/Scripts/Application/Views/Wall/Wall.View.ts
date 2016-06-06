module App.Views {
    import WallCollection = Collections.WallCollection;
    import WallModel = Models.WallModel;

    export class WallView extends Backbone.View<WallModel>{
        constructor(model?) {
            this.template = Tools.getTemplate("#wall-view-template");
            events: { };
            super(model);
        }

        wallCollection: WallCollection;
        template: (...data: any[]) => string;

        initialize(options) {
            this.wallCollection = new Collections.WallCollection();

            this.listenTo(this.wallCollection, 'reset', this.addWallPosts);
            this.listenTo(this.wallCollection, 'add', this.addWallPost);

        }
        render() {
            this.setElement(this.template());
            try {
                if (typeof App.owner !== "undefined") {
                    var view = new WallEditView({ WallCollection: this.wallCollection });
                    this.$("#wall-edit-placeholder").html(view.render().el);
                }
            } catch (exception) {
                return this;
            }
            return this;
        }

        addWallPosts() {
            this.wallCollection.each(model => this.addWallPost(model));
            return this;

        }
        addWallPost(model) {
            var view = new Views.WallPost({ model: model });
            this.$("#wall").append(view.render().el);
        }
    };
}