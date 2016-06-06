"use strict";

angular.module('photo.bel.photoshop', ["ngRoute"])
    .config([
        "$routeProvider", function ($routeProvider) {
            $routeProvider.when("/photoshop", {
                label: 'Магазины',
                templateUrl: "Scripts/default/components/photoshopModule/photoshopList/index.html",
                controller: "photoshopListCtrl"
            });

            $routeProvider.when("/photoshop/details", {
                label: 'Информация о магазине',
                templateUrl: "Scripts/default/components/photoshopModule/details/details.html",
                controller: "photoshopCtrl"
            });

            $routeProvider.when("/photoshop/about", {
                label: 'Информация о магазине',
                templateUrl: "Scripts/default/components/photoshopModule/details/about.html",
                controller: "photoshopCtrl"
            });

            $routeProvider.when("/photoshop/contact", {
                label: 'Контакты',
                templateUrl: "Scripts/default/components/photoshopModule/details/contact.html",
                controller: "photoshopCtrl"
            });
}
    ]);
