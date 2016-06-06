(function () {
	'use strict';
	angular
		.module('photo.bel.admin.photorent')
        .controller('photorentAdminCtrl', photorentAdminCtrl);

	photorentAdminCtrl.$inject = ['$scope', '$http', '$location', '$routeParams'];

	function photorentAdminCtrl($scope, $http, $location, $routeParams) {
	    $scope.title = 'photorentCtrl';
        $scope.setTab = function(val) {
            $scope.tab = val;
        }
        $scope.setTab('tab1');
        $scope.item = {};
        $scope.phone = {};
        $scope.addName = function () {
            $http({
                method: 'POST',
                url: '/api/v2/photorent',
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
        $scope.loadPhoto = function() {
            $scope.setTab('tab4');
        }
        $scope.updateData = function () {
            $scope.item.city = $scope.phone.city;
            $scope.item.adress = $scope.phone.adress;
            $http({
                method: 'PUT',
                url: '/api/v2/photorent?shortcut='+$scope.item.shortcut,
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
