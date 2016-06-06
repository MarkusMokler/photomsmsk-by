/// <reference path="../../../typings/backbone/backbone.d.ts"/>

module App {

    export class PhotostudioModel extends App.Models.CalendarModel {
        constructor(model?) {
            super(model);
            this.urlRoot = App.makeUrl("/api/photostudio");
        }

        urlRoot: string;
        halls: any[];
    }
} 


