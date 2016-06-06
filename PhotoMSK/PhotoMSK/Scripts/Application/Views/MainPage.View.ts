module App.Views {

    export class MainPage extends Backbone.View<Backbone.Model>{
        mainWall: MainWallView;

        constructor(model?) {
            this.el = "body";
            super(model);
        }

        initialize(options) { }
        render() {
            this.mainWall = new App.Views.MainWallView();
            this.$("#wall-placeholder").html(this.mainWall.render().el);
            return this;
        }

        reset(data: any) {
            this.mainWall.reset(data);
        }
    }
}