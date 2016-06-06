"use strict";
angular.module('photoshop.bel.module', [
"ngRoute",
'photoshop.bel.module.home'
]).config([
        "$routeProvider", function ($routeProvider) {
            $routeProvider.when("/", {
                templateUrl: "Scripts/default/whitelabel/shop/home/index.html",
                controller: "shopCtrl"
            });
        }
    ]);
