/// <reference path="../../typings/backbone/backbone.d.ts"/>

module App.Models {
    export class AttachmentModel extends Backbone.Model {
        constructor(model?) {
            this.url = App.makeUrl("/Api/Attachments");
            super(model);
        }

        url: string;

        parse(data) {
            var response = {};
            return response;
        }
    };
}


