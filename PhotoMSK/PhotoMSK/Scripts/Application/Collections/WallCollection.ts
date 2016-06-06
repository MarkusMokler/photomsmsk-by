/// <reference path="../../typings/backbone/backbone.d.ts"/>
/// <reference path="../Models/WallModel.ts"/>

module App.Collections {
    import WallModel = App.Models.WallModel;

    export class WallCollection extends Backbone.Collection<WallModel>{
        constructor(model?) {
            this.url = App.makeUrl("/Api/Wall");
            super(model);
        }

        url: string;

        initialize(options) {
        }
    }
}