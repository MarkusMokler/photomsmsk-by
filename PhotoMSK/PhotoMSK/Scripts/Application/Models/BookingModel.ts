/// <reference path="../../typings/backbone/backbone.d.ts"/>

module App.Models {
    export class BookingModel extends Backbone.Model {
        constructor(model?) {
            this.urlRoot = App.makeUrl("/api/Booking");
            super(model);
        }

        urlRoot: string;
    }
}
