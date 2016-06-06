"use strict";
angular.module('photo.bel.admin.photostudio', ["ngRoute", "photo.bel.admin.directivies"])
    .config([
        "$routeProvider", function ($routeProvider) {
            $routeProvider.when('/admin/photostudio/create', {
                label: "Создание",
                templateUrl: "Scripts/admin/components/photostudioModule/photostudio/index.html",
                controller: 'photostudioAdminCtrl'
            });
            $routeProvider.when('/admin/photostudio', {
                label: "Фотостудия",
                templateUrl: "Scripts/admin/components/photostudioModule/reserve/index.html",
                controller: 'photostudioReserveCtrl'
            });
            $routeProvider.when('/admin/photostudio/:id/dashboard', {
                label: "Дашборд",
                templateUrl: "Scripts/admin/components/photostudioModule/dashBoard/index.html",
                controller: 'photostudioDashboardCtrl'
            });
            $routeProvider.when('/admin/photostudio/:id/halls', {
                label: "Залы",
                templateUrl: "Scripts/admin/components/shared/RouteEdit/_Halls.html",
                controller: 'photostudioEditCtrl'
            });
            $routeProvider.when('/admin/photostudio/:id/participants', {
                label: "Сотрудники",
                templateUrl: "Scripts/admin/components/shared/RouteEdit/_Participants.html",
                controller: 'photostudioEditCtrl'
            });
            $routeProvider.when('/admin/photostudio/:id/booking', {
                label: "Бронирование",
                templateUrl: "Scripts/admin/components/photostudioModule/reserve/index.html",
                controller: 'photostudioReserveCtrl'
            });
            $routeProvider.when('/admin/photostudio/:id/profit', {
                label: "Выручка",
                templateUrl: "Scripts/admin/components/photostudioModule/photostudio/index.html",
                controller: 'photostudioProfitCtrl'
            });
            $routeProvider.when('/admin/photostudio/:id/edit', {
                label: "Редактирование",
                templateUrl: "Scripts/admin/components/photostudioModule/edit/index.html",
                controller: 'photostudioEditCtrl'
            });
        }
    ]);
