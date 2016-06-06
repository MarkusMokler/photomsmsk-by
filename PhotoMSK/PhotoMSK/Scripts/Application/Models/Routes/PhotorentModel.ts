/// <reference path="../../../typings/backbone/backbone.d.ts"/>

module App.Models {

    export class PageView extends Backbone.Model {
        elements: any[];
    }

    export class PhotorentModel extends Backbone.Model {
        constructor(model?) {
            this.urlRoot = App.makeUrl("/Api/Photorent");
            super(model);
        }

        urlRoot: string;
        technics: PageView;
    }
} 


