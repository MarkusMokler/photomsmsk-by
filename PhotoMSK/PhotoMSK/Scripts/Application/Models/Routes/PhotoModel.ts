/// <reference path="../../../typings/backbone/backbone.d.ts"/>

module App {
    export class PhotoModelModel extends Backbone.Model {
        constructor(model?) {
            this.urlRoot = App.makeUrl("/Api/photomodel") ;
            super(model);
        }

        urlRoot: string;
    }
} 


