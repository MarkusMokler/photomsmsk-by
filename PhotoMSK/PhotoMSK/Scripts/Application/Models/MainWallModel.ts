/// <reference path="../../typings/backbone/backbone.d.ts"/>

module App.Models {
    export class MainWallModel extends Backbone.Model {
        defaults(): any {
            return {
                title: ""
            };
        }
        parse(data) {
            var response = {};
            return response;
        }
    }
}