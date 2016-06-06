/// <reference path="../../../typings/backbone/backbone.d.ts"/>

module App {

    export class PageModel extends Backbone.Model {
        constructor(model?) {
            this.urlRoot = App.makeUrl("/api/page");
            super(model);
        }

        urlRoot: string;
    }
} 

