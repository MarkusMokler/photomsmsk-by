"use strict";

angular.module('photo.bel.masterclass', ["ngRoute"])
    .config([
     "$routeProvider", function($routeProvider) {
         $routeProvider.when("/masterclass", {
                label: 'Мастерклассы',
                templateUrl: "Scripts/default/components/masterclassModule/masterclass/index.html",
                controller: "masterclassCtrl"
            });

         $routeProvider.when("/masterclassDetails", {
                label: 'Информация о мастерклассе',
                templateUrl: "Scripts/default/components/masterclassModule/masterclassDetails/details.html",
                controller: "masterclassDetailsCtrl"
            });
        }
    ]);