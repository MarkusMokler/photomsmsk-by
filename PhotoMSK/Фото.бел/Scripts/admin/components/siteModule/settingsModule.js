"use strict";

angular.module("photo.bel.admin.site", ["ngRoute", "photo.bel.admin.directivies", "photo.bel.admin"])
    .config([
        "$routeProvider", function ($routeProvider) {
            $routeProvider.when("/admin/photostudio/:id/layouts", {
                label: 'Шаблоны',
                templateUrl: "Scripts/admin/components/siteModule/layouts/index.html",
                controller: "layoutsCtrl"
            })
            .when("/admin/photostudio/:id/layouts/:lid/edit", {
                label: 'Редактирование',
                templateUrl: "Scripts/admin/components/siteModule/layouts/edit/index.html",
                controller: "layoutsEditCtrl"
            })
            .when("/admin/photostudio/:id/layouts/add", {
                label: 'Создание',
                templateUrl: "Scripts/admin/components/siteModule/layouts/add/index.html",
                controller: "layoutsAddCtrl"
            })
            .when("/admin/photostudio/:id/routeLayouts", {
                label: 'Шаблоны',
                templateUrl: "Scripts/admin/components/siteModule/routeLayouts/index.html",
                controller: "routeLayoutsCtrl"
            })
            .when("/admin/photostudio/:id/routeLayouts/:lid/edit", {
                label: 'Редактирование',
                templateUrl: "Scripts/admin/components/siteModule/routeLayouts/edit/index.html",
                controller: "routeLayoutsEditCtrl"
            })
            .when("/admin/photostudio/:id/routeLayouts/add", {
                label: 'Создание',
                templateUrl: "Scripts/admin/components/siteModule/routeLayouts/add/index.html",
                controller: "routeLayoutsAddCtrl"
            }).when("/admin/photostudio/:id/menu", {
                label: 'Меню',
                templateUrl: "Scripts/admin/components/siteModule/menu/index",
                controller: "menuWLCtrl"
            })
            .when("/admin/photostudio/:id/menu/create", {
                label: 'Редактирование',
                templateUrl: "Scripts/admin/components/siteModule/menu/details/create",
                controller: "menuWLEditCtrl"
            })
            .when("/admin/photostudio/:id/themes", {
                label: 'Темы',
                templateUrl: "Scripts/admin/components/siteModule/themes/index.html",
                controller: "themesWLCtrl"
            })
            .when("/admin/photostudio/:id/pages", {
                label: 'Страницы',
                templateUrl: "Scripts/admin/components/siteModule/page/index.html",
                controller: "pageWLCtrl"
            })
            .when("/admin/photostudio/:id/pages/create", {
                label: 'Создание',
                templateUrl: "Scripts/admin/components/siteModule/page/details/create.html",
                controller: "pageCreateWLCtrl"
            })
            .when("/admin/photostudio/:id/routeLayouts/:lid/", {
                label: 'Шаблон',
                templateUrl: "Scripts/admin/components/siteModule/routeLayouts/edit/index.html",
                controller: "routeLayoutsEditCtrl"
            });

        }
    ]);
