"use strict";

angular.module('photo.bel.photographer', ["ngRoute"])
    .config([
        "$routeProvider", function ($routeProvider) {
            $routeProvider.when("/photographer", {
                label: 'Фотографы',
                templateUrl: "Scripts/default/components/photographerModule/photographerList/index.html",
                controller: "photographerListCtrl"
            });

            $routeProvider.when("/photographerDetails/:id", {
                label: 'Информация о фотографе',
                templateUrl: "Scripts/default/components/photographerModule/Details/details.html",
                controller: "photographerCtrl"
            });
        }
    ]);