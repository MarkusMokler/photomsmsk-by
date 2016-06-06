"use strict";

angular.module("photo.bel.home", ["ngRoute"])
    .config([
     "$routeProvider", function ($routeProvider) {
         $routeProvider.when("/", {
             label: "Главная",
             templateUrl: "Scripts/default/components/homeModule/Home/index.html",
             controller: "homeCtrl"
         }).when("/:id", {
             label: "",
             templateUrl: "Scripts/default/components/homeModule/Shortcut/index.html",
             controller: "shortcutCtrl"
         });
     }
    ])

;
