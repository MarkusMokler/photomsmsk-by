 /// <reference path="../../../typings/backbone/backbone.d.ts"/>

module App {
    export class PhoneCheckModel extends Backbone.Model {
        constructor(model?) {
            this.urlRoot = App.makeUrl("/api/v2/Route/");
            super(model);
        }

        urlRoot: string;
        id: string;
        EntityID: string;
        Number: string;
        ConfirmCode: string;
    }
}