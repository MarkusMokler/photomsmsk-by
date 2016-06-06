"use strict";

angular.module('photo.bel.admin.statistic', ["ngRoute", "photo.bel.admin.directivies"])
    .config([
        "$routeProvider", function ($routeProvider) {

            $routeProvider.when('/admin/photostudio/:id/clients', {
                label: "Клиенты",
                templateUrl: "/Scripts/admin/components/statisticsModule/clients/index.html",
                controller: 'clientsCtrl'
            });

            $routeProvider.when('/admin/photostudio/:id/clients/:tag', {
                label: "Клиенты",
                templateUrl: "/Scripts/admin/components/statisticsModule/clients/index.html",
                controller: 'clientsCtrl'
            });

            $routeProvider.when('/admin/photostudio/:id/reviews', {
                label: "Отзывы",
                templateUrl: "/Scripts/admin/components/statisticsModule/reviews/index.html",
                controller: 'reviewsCtrl'
            });

            $routeProvider.when('/admin/photostudio/:id/month', {
                label: "Месячный отчет",
                templateUrl: "/Scripts/admin/components/statisticsModule/monthStatistic/index.html",
                controller: 'monthStatisticCtrl'
            });

            $routeProvider.when('/admin/photorent/:id/month', {
                label: "Месячный отчет",
                templateUrl: "/Scripts/admin/components/statisticsModule/monthStatistic/index.html",
                controller: 'monthStatisticCtrl'
            });


            $routeProvider.when('/admin/photostudio/:id/balance', {
                label: "Месячный отчет",
                templateUrl: "/Scripts/admin/components/statisticsModule/balance/index.html",
                controller: 'balanceCtrl'
            });

            $routeProvider.when('/admin/photorent/:id/balance', {
                label: "Месячный отчет",
                templateUrl: "/Scripts/admin/components/statisticsModule/balance/index.html",
                controller: 'balanceCtrl'
            });

            $routeProvider.when('/admin/photostudio/:id', {
                label: "{{id}}",
                templateUrl: "/Scripts/admin/components/statisticsModule/activityStream/index.html",
                controller: 'activityStreamCtrl'
            });
            
            $routeProvider.when('/admin/photorent/:id/activity', {
                label: "{{id}}",
                templateUrl: "Scripts/admin/components/statisticsModule/activityStream/index.html",
                controller: 'activityStreamCtrl'
            });
        }
    ]);
