"use strict";

angular.module('photo.bel.admin.hall', ["ngRoute", "photo.bel.admin.directivies", "ngFileUpload"])
    .config([
        "$routeProvider", function ($routeProvider) {
            $routeProvider.when('/admin/photostudio/:id/hall/add', {
                label : "Создание зала",
                templateUrl: "Scripts/admin/components/photostudioModule/hallModule/add/index.html",
                controller: 'hallAddAdminCtrl'
            });
            $routeProvider.when('/admin/photostudio/:id/hall/:hallId/edit', {
                label : "Редактирование зала",
                templateUrl: "Scripts/admin/components/photostudioModule/hallModule/edit/index.html",
                controller: 'hallEditAdminCtrl'
            });
            $routeProvider.when('/admin/photostudio/:id/hall/:hallId/delete', {
                label : "Удаление зала",
                templateUrl: "Scripts/admin/components/photostudioModule/hallModule/delete/index.html",
                controller: 'hallDeleteAdminCtrl'
            });
        }
    ]);
