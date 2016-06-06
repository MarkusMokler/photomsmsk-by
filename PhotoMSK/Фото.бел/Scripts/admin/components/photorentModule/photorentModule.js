"use strict";
angular.module('photo.bel.admin.photorent', ["ngRoute", "photo.bel.admin.directivies"])
    .config([
        "$routeProvider", function ($routeProvider) {
            $routeProvider.when('/admin/photorent', {
                label: "Фотопрокат",
                templateUrl: "Scripts/admin/components/photorentModule/photorent/index.html",
                controller: 'photorentAdminCtrl'
            });
            $routeProvider.when('/admin/photorent/:id', {
                label: "{{id}}"
            });
            $routeProvider.when('/admin/photorent/:id/edit', {
                label: "Редактирование",
                templateUrl: "Scripts/admin/components/photorentModule/edit/index.html",
                controller: 'photorentEditCtrl'
            })
            $routeProvider.when('/admin/photorent/:id/booking', {
                label: "Бронирование",
                templateUrl: "Scripts/admin/components/photorentModule/reserve/index.html",
                controller: 'photorentReserveCtrl'
            });
            $routeProvider.when('/admin/photorent/:id/prices', {
                label: "Прайсы",
                templateUrl: "Scripts/admin/components/photorentModule/prices/index.html",
                controller: 'photorentPricesCtrl'
            });
            $routeProvider.when('/admin/photorent/:id/add', {
                label: "Добавление техники",
                templateUrl: "Scripts/admin/components/photorentModule/techadd/index.html",
                controller: 'addTechCtrl'
            });
            $routeProvider.when('/admin/photorent/:id/participants', {
                    label: "Сотрудники",
                    templateUrl: "Scripts/admin/components/shared/RouteEdit/_Participants.html",
                    controller: 'photorentParticipantsCtrl'
});
        }
    ]);