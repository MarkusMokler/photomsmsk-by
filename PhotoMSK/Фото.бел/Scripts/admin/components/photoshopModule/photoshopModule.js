"use strict";

angular.module('photo.bel.admin.photoshop', ["ngRoute", "photo.bel.admin.directivies"])
    .config([
        "$routeProvider", function ($routeProvider) {
            $routeProvider.when('/admin/photoshop/:id/edit', {
                    label: "Редактирование магазина",
                    templateUrl: "Scripts/admin/components/photoshopModule/edit/index.html",
                    controller: 'photoshopEditCtrl'
            })
            .when('/admin/photoshop/:id/prices', {
                label: "Прайсы",
                templateUrl: "Scripts/admin/components/photoshopModule/prices/index.html",
                controller: 'pricesCtrl'
            })
            .when('/admin/photoshop/:id', {
                label: "{{id}}"
            })
            .when('/admin/photoshop/', {
                label: "Магазин"
            });
        }
    ]);
