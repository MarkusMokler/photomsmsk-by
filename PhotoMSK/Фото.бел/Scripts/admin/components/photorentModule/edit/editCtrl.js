(function () {
    'use strict';
    angular
		.module('photo.bel.admin.photorent')
        .controller('photorentEditCtrl', photorentEditCtrl);

    photorentEditCtrl.$inject = ['$scope', '$http', '$routeParams', '$location'];

    function photorentEditCtrl($scope, $http, $routeParams, $location) {
        $scope.title = 'photorentEditCtrl';
        $scope.setTab = function (val) {
            $scope.tab = val;
        }
        $scope.setTab('tab1');
        $scope.item = {};
        $scope.phone = {};
        $http.get('/api/v2/photorent/?shortcut=' + $routeParams.id).success(function (response) {
            $scope.item = response[0];
            if ($scope.item.phones.length > 0) {
                $scope.phone.number = $scope.item.phones[0].number;
            };
            $scope.phone.City = $scope.item.city;
            $scope.phone.Adress = $scope.item.adress;
            console.log($scope.item);
        });
        $scope.addName = function () {
            $http({
                method: 'PUT',
                url: '/api/v2/photorent/?shortcut=' + $routeParams.id,
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
            $scope.addName();
            $scope.setTab('tab4');
        }
        $scope.updateData = function () {
            $scope.setTab('tab3');
        }
        $scope.add = function () {
            $location.path('/admin');
        };
        activate();

        function activate() { }
    }
})();