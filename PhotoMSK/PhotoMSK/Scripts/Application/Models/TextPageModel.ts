/// <reference path="../../typings/backbone/backbone.d.ts"/>

module App.Models{
    export class TextPageModel extends Backbone.Model{
        constructor(model?) {
            this.urlRoot = App.makeUrl("/Api/TextPage");
            super(model);
        }

        urlRoot: string;
    };
}
