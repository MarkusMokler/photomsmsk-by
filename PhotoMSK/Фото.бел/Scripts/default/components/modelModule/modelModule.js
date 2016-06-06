"use strict";

angular.module('photo.bel.model', ["ngRoute"])
    .config([
        "$routeProvider", function ($routeProvider) {
            $routeProvider.when("/model", {
                label: '����������',
                templateUrl: "Scripts/default/components/modelModule/modelList/index.html",
                controller: "modelListCtrl"
            });
            $routeProvider.when("/model/:id", {
                label: '���������� � ����������',
                templateUrl: "Scripts/default/components/modelModule/photomodels/details.html",
                controller: "photomodelsCtrl"
            });
}
    ]);
