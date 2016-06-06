(function () {
    'use strict';
    angular
		.module('photo.bel.admin.photorent')
        .controller('photorentPricesCtrl', photorentPricesCtrl);

    photorentPricesCtrl.$inject = ['$scope', '$http', '$routeParams'];
    function photorentPricesCtrl($scope, $http, $routeParams) {
        $scope.title = 'photorentPricesCtrl';
        $scope.setData = function(item) {
            $scope.view = item;
        };
        $scope.edit = function () {
            $scope.isEdit = true;
        }
        $scope.approive = function () {
            $http({
                method: 'PUT',
                url: '/api/v2/RentCalendar/' + $scope.view.id,
                data: $scope.view,
                headers: { 'Content-Type': 'application/json' }
            })
             .success(function (dadta) {
                 $scope.message = dadta;
             });
            $scope.isEdit = false;
        }
        activate();
        $scope.routeShortcut = $routeParams.id;
        $http.get("/api/v2/RentCalendar/?shortcut=" + $routeParams.id)
                    .success(function (data) {
                        $scope.item = data.elements;
                        $scope.setData($scope.item[0]);
                    });
        function activate() { }
    }
})();