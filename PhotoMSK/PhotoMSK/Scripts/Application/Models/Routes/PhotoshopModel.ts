/// <reference path="../../../typings/backbone/backbone.d.ts"/>

module App {
    import PageView = App.Models.PageView;

    export class PhotoshopModel extends Backbone.Model {
        constructor(model?) {
            this.urlRoot = App.makeUrl("/api/photoshop");
            super(model);
        }

        urlRoot: string;
        technics: PageView;
    }
} 

