(function () {
    'use strict';
    angular
        .module("photo.bel.admin.site")
        .controller("menuWLEditCtrl", menuWLEditCtrl);

    menuWLEditCtrl.$inject = ['$scope', '$http', '$routeParams'];

    function menuWLEditCtrl($scope, $http, $routeParams) {
        $scope.title = 'menuCtrl';
        $scope.itemMenu = {};
        $scope.WhiteLabel = $routeParams.id;
        $scope.pagesByNameUrl = 'api/v2/TextPage/?search=';
        $scope.saveItemMenu = function () {
            console.log($scope.selectedMenuPage);
            $scope.itemMenu.page = {};
            $scope.itemMenu.page = $scope.selectedMenuPage==null?null:$scope.selectedMenuPage.originalObject;
            $http({
                method: 'POST',
                url: '/api/v2/MenuItem/?shortcut=' + $scope.WhiteLabel,
                data: $scope.itemMenu
            });
        }
        $scope.clean = function () {
            $scope.itemMenu = null;
        }
     
        activate();

        function activate() { }
    }
})();