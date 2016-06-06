/// <reference path="../../../typings/backbone/backbone.d.ts"/>

module App.Models {
 
       export class PhototechnicsModel extends Backbone.Model {
           constructor(model?) {
               this.url = App.makeUrl("/Api/PhototechnicsViewModel");
               super(model);
           }

           url: string;

           defaults() {
               return {
                   title: ""
               };
           }
       }
}