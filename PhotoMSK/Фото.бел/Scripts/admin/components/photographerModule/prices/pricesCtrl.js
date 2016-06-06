(function () {
    'use strict';
    angular
		.module('photo.bel.admin.photographer')
        .controller('PricesCtrl', PricesCtrl);

    PricesCtrl.$inject = ['$scope', '$http', '$routeParams'];
    function PricesCtrl($scope, $http, $routeParams) {
        $scope.title = 'PricesCtrl';
        $scope.setData = function (item) {
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
             .success(function (data) {
                 $scope.message = data;
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