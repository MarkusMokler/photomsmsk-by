"use strict";

angular.module("photo.bel.photomonth", ["ngRoute"])
    .config([
        "$routeProvider", function ($routeProvider) {
            $routeProvider.when("/photomonth", {
                label: "Фото месяца",
                templateUrl: "Scripts/default/components/photomonthModule/photomonthList/index.html",
                controller: "photomonthListCtrl"
            });
        }
    ]);
