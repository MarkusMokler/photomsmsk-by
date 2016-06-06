"use strict";

angular.module('photo.bel.account', ["ngRoute"])
    .config([
        "$routeProvider", function ($routeProvider) {
            $routeProvider.when("/login", {
                templateUrl: "Scripts/default/components/accountModule/account/login.html",
                controller: "accountCtrl"
            });

            $routeProvider.when("/register", {
                templateUrl: "Scripts/default/components/accountModule/register/index.html",
                controller: "registerCtrl"
            });
        }
    ]);
