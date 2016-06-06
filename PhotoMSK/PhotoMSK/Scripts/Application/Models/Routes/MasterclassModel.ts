/// <reference path="../../../typings/backbone/backbone.d.ts"/>

module App {

    export class MasterclassModel extends Backbone.Model {
        constructor(model?) {
            this.urlRoot = App.makeUrl("/api/masterclass");
            super(model);
        }

        urlRoot: string;
    }
} 

