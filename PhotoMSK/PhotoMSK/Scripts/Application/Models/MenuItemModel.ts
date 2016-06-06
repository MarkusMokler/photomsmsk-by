/// <reference path="../../typings/backbone/backbone.d.ts"/>

module App.Models {
    export class MenuItemModel extends Backbone.Model {
        constructor(model?) {
            this.urlRoot = App.makeUrl("/Api/MenuItem");
            super(model);
        }

        urlRoot: string;
    }
}