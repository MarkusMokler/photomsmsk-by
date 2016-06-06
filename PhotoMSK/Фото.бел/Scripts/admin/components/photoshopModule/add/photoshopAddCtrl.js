(function () {
    'use strict';
    angular
		.module('photo.bel.admin.photoshop')
        .controller('photoshopAddCtrl', photoshopAddCtrl);

    photoshopAddCtrl.$inject = ['$scope', '$http', '$routeParams'];
    function photoshopAddCtrl($scope, $http, $routeParams) {
        $scope.title = 'photoshopAddCtrl';
        $scope.setTab = function (val) {
            $scope.tab = val;
        }
        $scope.setTab('tab1');
        $scope.addName = function () {
            $http({
                method: 'POST',
                url: '/api/v2/photoshop',
                data: $scope.item,
                headers: { 'Content-Type': 'application/json' }
            })
	            .success(function (data) {
	                $scope.dataEntityID = data.id;
	            });
            $scope.lockTab = true;
            if ($scope.lockTab) {
                $scope.setTab('tab2');
                $scope.lockTab = false;
            }
        };
        $scope.loadPhoto = function () {
            $scope.setTab('tab4');
        }
        $scope.updateData = function () {
            $http({
                method: 'PUT',
                url: '/api/v2/photoshop?shortcut=' + $scope.item.shortcut,
                data: $scope.item,
                headers: { 'Content-Type': 'application/json' }
            })
	            .success(function (data) {

	            });

            $scope.setTab('tab3');
        }
        $scope.add = function () {
            $location.path('/admin');
        };

        activate();
        function activate() { }
    }
})();