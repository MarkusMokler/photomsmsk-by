/// <reference path="reserveCtrl.js" />
(function () {
    'use strict';
    angular
		.module('photo.bel.admin.photorent')
        .controller('photorentReserveCtrl', photorentReserveCtrl);

    photorentReserveCtrl.$inject = ['$scope', '$http', '$routeParams', '$location'];

    function photorentReserveCtrl($scope, $http, $routeParams) {
        $scope.shortcut = $routeParams.id;

        $scope.dataUrl = 'api/v2/RentCalendar/?shortcut=' + $routeParams.id;
        $http.get($scope.dataUrl).then(function (response) {
            $scope.item = response.data;
            $scope.calendars = [];
            $.each($scope.item.elements, function () {
                $scope.calendars.push(this.rents[0].calendarID);
            });
        });

        $scope.rentBookingFunc = function () {
            $http.get("/api/v2/Calendar?routeid=" + $scope.selectedItem.photorentID +
                "&start=" + moment($scope.currentSpan.start).format('YYYY-MM-DD hh:mm') +
                "&end=" + moment($scope.currentSpan.end).format('YYYY-MM-DD hh:mm'))
                        .then(function () {
                            $scope.shoppingCart();
                });
        };
        
        activate();

        function activate() { }
    }
})();