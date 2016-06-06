"use strict";
// Declare app level module which depends on views, and components
var app = angular.module("photo.bel.admin", [
  "ngRoute",
  "ui.sortable",
  "photo.bel.service",
  "photo.bel.directives",
  "photo.bel.admin.directivies",
  "photo.bel.admin.blog",
  "photo.bel.admin.callhandler",
  "photo.bel.admin.cardchecker",
  "photo.bel.admin.deleteroute",
  "photo.bel.admin.developer",
  "photo.bel.admin.dialog",
  "photo.bel.admin.hall",
  "photo.bel.admin.home",
  "photo.bel.admin.masterclass",
  "photo.bel.admin.page",
  "photo.bel.admin.photographer",
  "photo.bel.admin.project",
  "photo.bel.admin.creative",
  "photo.bel.admin.photo",
  "photo.bel.admin.book",
  "photo.bel.admin.media",
  "photo.bel.admin.photomodel",
  "photo.bel.admin.photorent",
  "photo.bel.admin.photoshop",
  "photo.bel.admin.photostudio",
  "photo.bel.admin.plasticcard",
  "photo.bel.admin.statistics",
  "photo.bel.admin.statistic",
  "photo.bel.admin.userinformation",
  "photo.bel.admin.settings",
  "photo.bel.admin.usersmanagement",
  "photo.bel.admin.whitelabel",
  "photo.bel.admin.route",
  "photo.bel.admin.question",
  "photo.bel.admin.development",
  "photo.bel.admin.site",
  "ui.codemirror",
  'photo.bel.admin.services',
  "textAngular"
]).config(["$routeProvider", function ($routeProvider) {
    $routeProvider.
        when("/admin", {
            label: "Личный кабинет",
            templateUrl: "Scripts/admin/components/home/index.html",
            controller: "homeAdminCtrl"
        }).
        when("/admin/blog", {
            label: "Блог",
            templateUrl: "Scripts/admin/components/blog/index.html",
            controller: "blogAdminCtrl"
        }).
        when("/admin/callHandler", {
            templateUrl: "Scripts/admin/components/callHandler/index.html",
            controller: "callHandlerAdminCtrl"
        }).
        when("/admin/deleteRoute", {
            templateUrl: "Scripts/admin/components/deleteRoute/index.html",
            controller: "deleteRouteAdminCtrl"
        }).
        when("/admin/developer", {
            templateUrl: "Scripts/admin/components/developer/index.html",
            controller: "developerAdminCtrl"
        }).
        when("/admin/dialog", {
            templateUrl: "Scripts/admin/components/dialog/index.html",
            controller: "dialogAdminCtrl"
        }).
        when("/admin/masterclass", {
            templateUrl: "Scripts/admin/components/masterclass/index.html",
            controller: "masterclassAdminCtrl"
        }).
        when("/admin/page", {
            templateUrl: "Scripts/admin/components/page/index.html",
            controller: "pageAdminCtrl"
        }).
        when("/admin/plasticcard", {
            templateUrl: "Scripts/admin/components/plasticcard/index.html",
            controller: "plasticcardAdminCtrl"
        }).
        when("/admin/statistics", {
            templateUrl: "Scripts/admin/components/statistics/index.html",
            controller: "statisticsAdminCtrl"
        }).
        when("/admin/userinformation/:id", {
            label: "Информация о пользователе",
            templateUrl: "Scripts/admin/components/userinformation/index.html",
            controller: "userInformationAdminCtrl"
        }).
        when("/admin/usermanagement", {
            templateUrl: "Scripts/admin/components/usermanagement/index.html",
            controller: "usermanagementAdminCtrl"
        }).
        when("/admin/whitelabel", {
            templateUrl: "Scripts/admin/components/whitelabel/index.html",
            controller: "whitelabelAdminCtrl"
        }).
        when("/admin/photostudio/:id/question", {
            label: "База знаний",
            templateUrl: "Scripts/admin/components/question/index.html",
            controller: "questionAdminCtrl"
        }).
        when("/admin/photostudio/:id/development", {
            label: "Для разработчиков",
            templateUrl: "Scripts/admin/components/development/index.html",
            controller: "developmentAdminCtrl"
        }).
        when("/admin/route/add", {
            label: "Создание",
            templateUrl: "Scripts/admin/components/route/index.html",
            controller: "routeAdminCtrl"
        });
}]);

app.config(["$routeProvider", function ($routeProvider) {
    $routeProvider.otherwise({ redirectTo: "/admin" });
}]);
