/// <reference path="../../../typings/backbone/backbone.d.ts"/>

module App.Models {
    export class PlaceModel extends Backbone.Model
    {
        constructor(model?) {
            this.urlRoot = App.makeUrl("/api/place");
            super(model);
        }

        urlRoot: string;
    };
}
