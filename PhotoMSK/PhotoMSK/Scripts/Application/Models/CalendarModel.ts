/// <reference path="../../typings/backbone/backbone.d.ts"/>

module App.Models {
    export class CalendarModel extends Backbone.Model {
        constructor(model?) {
            this.urlRoot = App.makeUrl("/api/Calendar");
            super(model);
        }

        urlRoot: string;
        halls:any[];
    }
}