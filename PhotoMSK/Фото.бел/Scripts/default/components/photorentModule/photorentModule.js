"use strict";

angular.module('photo.bel.photorent', ["ngRoute"])
    .config([
        "$routeProvider", function ($routeProvider) {
            $routeProvider.when("/photorent", {
                label: 'Техника',
                templateUrl: "Scripts/default/components/photorentModule/photorentList/index.html",
                controller: "photorentListCtrl"
            });

            $routeProvider.when("/photorent/details", {
                label: '',
                templateUrl: "Scripts/default/components/photorentModule/Details/details.html",
                controller: "photorentCtrl"
            });
}
    ]);
