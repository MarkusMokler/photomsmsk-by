"use strict";

angular.module("photo.bel.admin.photographer", ["ngRoute", "photo.bel.admin.directivies"])
    .config([
        "$routeProvider", function ($routeProvider) {
            $routeProvider.when("/admin/photographer", {
                label: "Фотограф",
                templateUrl: "Scripts/admin/components/photographerModule/photographer/index.html",
                controller: "photographerAdminCtrl"
            });
            $routeProvider.when("/admin/photographer/:id", {
                label: "{{id}}"
            });
            $routeProvider.when("/admin/photographer/:id/settings", {
                label: "Настройки",
                templateUrl: "Scripts/admin/components/photographerModule/settings/index.html",
                controller: "photographerSettingsCtrl"
            });
            $routeProvider.when("/admin/photographer/:id/booking", {
                label: "Бронирование",
                templateUrl: "Scripts/admin/components/photographerModule/reserve/index.html",
                controller: "photographerReserveCtrl"
            });
            $routeProvider.when("/admin/photographer/:id/prices", {
                label: "Прайсы",
                templateUrl: "Scripts/admin/components/photographerModule/prices/index.html",
                controller: "photographerPricesCtrl"
            });
            // Photos
            $routeProvider.when("/admin/photographer/:id/photos", {
                label: "Фотографии",
                templateUrl: "Scripts/admin/components/shared/RouteEdit/_Photos.html",
                controller: "photoAdminCtrl"
            });
            // Creative
            $routeProvider.when("/admin/photographer/:id/creative", {
                label: "Креативы",
                templateUrl: "Scripts/admin/components/shared/RouteEdit/_Creative.html",
                controller: "creativeAdminCtrl"
            });
            // Books
            $routeProvider.when("/admin/photographer/:id/books", {
                label: "Книги",
                templateUrl: "Scripts/admin/components/photographerModule/bookModule/index.html",
                controller: "photographerBooksCtrl"
            });
            // Projects
            $routeProvider.when("/admin/photographer/:id/projects", {
                label: "Проекты",
                templateUrl: "Scripts/admin/components/shared/RouteEdit/_Projects.html",
                controller: "projectAdminCtrl"
            });
            $routeProvider.when("/admin/photographer/:id/participants", {
                label: "Сотрудники",
                templateUrl: "Scripts/admin/components/shared/RouteEdit/_Participants.html",
                controller: "photographerParticipantsCtrl"
            });
        }
    ]);
