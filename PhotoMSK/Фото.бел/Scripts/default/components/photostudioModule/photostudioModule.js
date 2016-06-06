"use strict";

angular.module('photo.bel.photostudio', ["ngRoute"])
    .config([
        "$routeProvider", function ($routeProvider) {
            $routeProvider.when("/photostudio", {
                label: 'Фотостудии',
                templateUrl: "Scripts/default/components/photostudioModule/photostudioList/index",
                controller: "photostudioListCtrl"
            });

            $routeProvider.when("/photostudio/halls", {
                label: 'Залы',
                templateUrl: "Scripts/default/components/photostudioModule/hallList/index",
                controller: "hallListCtrl"
            });

            $routeProvider.when("/photostudio/halls/:id", {
                label: 'Информация о зале',
                templateUrl: "Scripts/default/components/photostudioModule/hall/Details/details",
                controller: "hallCtrl"
            });

            $routeProvider.when("/photostudio/:id", {
                label: ':id',
                templateUrl: "Scripts/default/components/photostudioModule/photostudio/index",
                controller: "photostudioCtrl"
            });

            $routeProvider.when("/map", {
                label: 'Карта',
                templateUrl: "Scripts/default/components/photostudioModule/yandexmap/index",
                controller: "mapCtrl"
            });

            $routeProvider.when("/map/:id", {
                label: '',
                templateUrl: "Scripts/default/components/photostudioModule/yandexmap/index",
                controller: "mapCtrl"
            });
        }
    ]);
