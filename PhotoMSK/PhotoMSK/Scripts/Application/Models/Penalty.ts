/// <reference path="../../typings/backbone/backbone.d.ts"/>

module App.Models {
    export class PenaltyModel extends Backbone.Model {
        constructor(model?) {
            this.url = App.makeUrl("/Api/Penalties");
            super(model);
        }

        url: string;

        defaults() {
            return {
                description: ""
            };
        }

        parse(data) {
            var response = {};
            return response;
        }
    }
}