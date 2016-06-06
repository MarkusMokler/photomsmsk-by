/// <reference path="../../../typings/backbone/backbone.d.ts"/>

module App {

    export class PhotographerModel extends Backbone.Model {
        constructor(model?) {
            this.urlRoot = App.makeUrl("/api/Photographer");
            super(model);
        }

        urlRoot: string;
    }
} 