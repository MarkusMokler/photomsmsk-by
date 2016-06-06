
module App.Collections {
    import MainWallModel = Models.MainWallModel;

    export class MainWallCollection extends Backbone.Collection<MainWallModel>{
        constructor(model?) {
            this.url = App.makeUrl("/Api/MainWall");
            super(model);
        }
        
        url: string;

        initialize(options) {

        }
    };
}