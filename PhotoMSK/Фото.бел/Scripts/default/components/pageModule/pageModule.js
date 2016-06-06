"use strict";
angular.module('photo.bel.page', ["ngRoute"])
    .config([
        "$routeProvider", function ($routeProvider) {
            $routeProvider.when("/page", {
                label: 'Паблики',
                templateUrl: "Scripts/default/components/pageModule/page/index.html",
                controller: "pageCtrl"
            });
        }
    ]);
