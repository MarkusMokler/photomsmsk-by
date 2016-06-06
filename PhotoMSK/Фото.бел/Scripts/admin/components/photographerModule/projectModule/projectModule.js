"use strict";

angular.module("photo.bel.admin.project", [
        "ngRoute",
        "photo.bel.admin.directivies"
])
    .config([
        "$routeProvider", function ($routeProvider) {
            $routeProvider.when("/admin/photographer/:id/addproject", {
                label: "Создание проекта",
                templateUrl: "Scripts/admin/components/photographerModule/projectModule/add/index.html",
                controller: "projectAddAdminCtrl"
            });
            $routeProvider.when("/admin/photographer/:id/project/:projectId/edit", {
                label: "Редактирование проекта",
                templateUrl: "Scripts/admin/components/photographerModule/projectModule/edit/index.html",
                controller: "projectEditAdminCtrl"
            });
            $routeProvider.when("/admin/photographer/:id/project/:projectId/delete", {
                label: "Удаление проекта",
                templateUrl: "Scripts/admin/components/photographerModule/projectModule/delete/index.html",
                controller: "projectDeleteAdminCtrl"
            });
        }
    ]);