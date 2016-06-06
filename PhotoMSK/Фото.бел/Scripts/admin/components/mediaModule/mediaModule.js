"use strict";

angular.module('photo.bel.admin.media', ["ngRoute", "photo.bel.admin.directivies"])
    .config([
        "$routeProvider", function ($routeProvider) {

            $routeProvider.when('/admin/photostudio/:id/media/manager', {
                label: "Файлы",
                templateUrl: "/Scripts/admin/components/mediaModule/mediamanager/index.html",
                controller: 'mediaManagerCtrl'
            });

        }
    ]);
