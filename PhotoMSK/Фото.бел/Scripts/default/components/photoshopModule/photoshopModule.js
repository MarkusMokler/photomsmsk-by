"use strict";

angular.module('photo.bel.photoshop', ["ngRoute"])
    .config([
        "$routeProvider", function ($routeProvider) {
            $routeProvider.when("/photoshop", {
                label: '��������',
                templateUrl: "Scripts/default/components/photoshopModule/photoshopList/index.html",
                controller: "photoshopListCtrl"
            });

            $routeProvider.when("/photoshop/details", {
                label: '���������� � ��������',
                templateUrl: "Scripts/default/components/photoshopModule/details/details.html",
                controller: "photoshopCtrl"
            });

            $routeProvider.when("/photoshop/about", {
                label: '���������� � ��������',
                templateUrl: "Scripts/default/components/photoshopModule/details/about.html",
                controller: "photoshopCtrl"
            });

            $routeProvider.when("/photoshop/contact", {
                label: '��������',
                templateUrl: "Scripts/default/components/photoshopModule/details/contact.html",
                controller: "photoshopCtrl"
            });
}
    ]);
