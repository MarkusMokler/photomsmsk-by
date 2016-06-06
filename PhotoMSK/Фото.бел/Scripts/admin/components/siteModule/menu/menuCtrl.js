(function () {
    'use strict';
    angular
        .module('photo.bel.admin.site')
        .controller('menuWLCtrl', menuWLCtrl);

    menuWLCtrl.$inject = ['$scope', '$http', '$routeParams'];

    function menuWLCtrl($scope, $http, $routeParams) {
        $scope.title = 'menuCtrl';
        $scope.itemMenu = null;
        $scope.WhiteLabel = $routeParams.id;
        $scope.allMenues = {};
        $scope.allMenues.menu = {
            nodes: []
        };

        $scope.sortableOptionsPublish = {
            placeholder: "placeholder--menu",
            connectWith: ".ng-menus",
            handle: '.sortable__handle',
            receive: function (e, ui) {
                ui.item.sortable.model.publish = true;
                $scope.saveChanges();
            }
        };

        $scope.sortableOptionsNonPublish = {
            placeholder: "placeholder--menu",
            connectWith: ".ng-menus",
            handle: '.sortable__handle',
            receive: function (e, ui) {
                ui.item.sortable.model.publish = false;
                $scope.saveChanges();
            }
        };

        $http.get("api/v2/MenuItem/?shortcut=" + $scope.WhiteLabel).then(function (response) {
            $scope.rootNodes = response.data;
        });


        $scope.setCurrentMenu = function (item) {
            $scope.currentMenuID = item.id;
        }

        $scope.$watch("currentMenuID", function () {
            $http.get("api/v2/MenuItem/?shortcut=" + $scope.WhiteLabel + "&routeMenuId=" + $scope.currentMenuID).then(function (response) {
                $scope.allMenues = response.data;
            });
        });

        $scope.saveChanges = function () {
            $http({
                method: "PUT",
                url: "/api/v2/MenuItem/?shortcut=" + $scope.WhiteLabel,
                data: $scope.allMenues.menu
            });
        };


        activate();

        function activate() { }
    }
})();
