"use strict";

angular.module("photo.bel.admin.book", [
        "ngRoute",
        "photo.bel.admin.directivies"
])
    .config([
        "$routeProvider", function ($routeProvider) {
            $routeProvider.when("/admin/photographer/:id/book/add", {
                label: "Создание книги",
                templateUrl: "Scripts/admin/components/photographerModule/bookModule/add/index.html",
                controller: "bookAddAdminCtrl"
            });
            $routeProvider.when("/admin/photographer/:id/book/:bookId/edit", {
                label: "Редактирование книги",
                templateUrl: "Scripts/admin/components/photographerModule/bookModule/edit/index.html",
                controller: "bookEditAdminCtrl"
            });
            $routeProvider.when("/admin/photographer/:id/book/:bookId/delete", {
                label: "Удаление книги",
                templateUrl: "Scripts/admin/components/photographerModule/bookModule/delete/index.html",
                controller: "bookDeleteAdminCtrl"
            });
        }
    ]);