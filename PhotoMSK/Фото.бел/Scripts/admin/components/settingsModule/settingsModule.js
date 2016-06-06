"use strict";

angular.module("photo.bel.admin.settings", ["ngRoute", "photo.bel.admin.directivies"])
    .config([
        "$routeProvider", function ($routeProvider) {

            $routeProvider.when("/admin/photostudio/:id/settings", {
                label: "Настройки",
                templateUrl: "/Scripts/admin/components/settingsModule/settings/index.html",
                controller: "settingsCtrl"
            });


            $routeProvider.when("/admin/photostudio/:id/settings/friendship", {
                label: "Друзья",
                templateUrl: "/Scripts/admin/components/settingsModule/friendship/index.html",
                controller: "friendshipCtrl"
            });


            $routeProvider.when("/admin/photostudio/:id/settings/legalInformation", {
                label: "Юр. Данные",
                templateUrl: "/Scripts/admin/components/settingsModule/legalInformation/index.html",
                controller: "legalInformationCtrl"
            });

            $routeProvider.when("/admin/photostudio/:id/settings/sms", {
                label: "СМС",
                templateUrl: "/Scripts/admin/components/settingsModule/sms/index.html",
                controller: "smsModuleCtrl"
            });
        }
    ]);
