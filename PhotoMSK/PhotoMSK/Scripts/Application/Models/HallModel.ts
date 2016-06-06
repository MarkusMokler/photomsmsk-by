/// <reference path="../../typings/backbone/backbone.d.ts"/>

module App.Models {
    export class HallModel extends Backbone.Model {
        constructor(model?) {
            this.urlRoot = App.makeUrl("/api/hall");
            super(model);
        }

        urlRoot: string;
    }
}