"use strict";

angular.module("photo.bel.admin.creative", [
        "ngRoute",
        "photo.bel.admin.directivies"
])
    .config([
        "$routeProvider", function ($routeProvider) {
            $routeProvider.when("/admin/photographer/:id/addcreative", {
                label: "Создание креатива",
                templateUrl: "Scripts/admin/components/photographerModule/creativeModule/add/index.html",
                controller: "creativeAddAdminCtrl"
            });
            $routeProvider.when("/admin/photographer/:id/creative/:creativeId/edit", {
                label: "Редактирование креатива",
                templateUrl: "Scripts/admin/components/photographerModule/creativeModule/edit/index.html",
                controller: "creativeEditAdminCtrl"
            });
            $routeProvider.when("/admin/photographer/:id/creative/:creativeId/delete", {
                label: "Удаление креатива",
                templateUrl: "Scripts/admin/components/photographerModule/creativeModule/delete/index.html",
                controller: "creativeDeleteAdminCtrl"
            });
        }
    ]);