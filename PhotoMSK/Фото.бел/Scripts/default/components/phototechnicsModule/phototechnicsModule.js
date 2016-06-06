"use strict";

angular.module('photo.bel.phototechnic', ["ngRoute"])
    .config([
        "$routeProvider", function ($routeProvider) {
            $routeProvider.when("/phototechnics", {
                templateUrl: "Scripts/default/components/phototechnicsModule/phototechnicsList/index.html",
                controller: "phototechnicCtrl"
            });
        }
    ]);