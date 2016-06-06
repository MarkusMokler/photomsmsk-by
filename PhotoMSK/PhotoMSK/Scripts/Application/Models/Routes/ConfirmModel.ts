/// <reference path="../../../typings/backbone/backbone.d.ts"/>

module App {
    export class ConfirmModel extends Backbone.Model {
        constructor(model?) {
            this.urlRoot = App.makeUrl("/api/v2/Route/");
            super(model);
        }

        urlRoot: string;
        reason: string;
    }
} 