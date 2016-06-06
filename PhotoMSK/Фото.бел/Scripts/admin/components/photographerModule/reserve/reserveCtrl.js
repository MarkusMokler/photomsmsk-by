/// <reference path="reserveCtrl.js" />
(function () {
    'use strict';
    angular
		.module('photo.bel.admin.photographer')
        .controller('ReserveCtrl', ReserveCtrl);

    ReserveCtrl.$inject = ['$scope', '$http', '$routeParams', '$location'];

    function ReserveCtrl($scope, $http, $routeParams) {
        $scope.title = $routeParams.id;
        $scope.setData = function (item) {
            $scope.isAdmin = true;
            $scope.selectedItem = item;
            $scope.rentBookingItem = item;
            $scope.calendarID = $scope.selectedItem.rents[0].calendarID;
            $scope.addCalendar($scope.calendarID);
            $scope.changeCalendar($scope.calendarID);
        }
        $scope.addCalendar = function (calendarID) {
            $scope.calendarsID = [];
            $scope.calendarsID.push(calendarID);
        }

        $scope.rentBookingFunc = function () {
            $http.get("/api/v2/Calendar?routeid=" + $scope.selectedItem.photographerID +
                "&start=" + moment($scope.currentSpan.start).format('YYYY-MM-DD hh:mm') +
                "&end=" + moment($scope.currentSpan.end).format('YYYY-MM-DD hh:mm'))
                        .then(function () {
                            $scope.shoppingCart();
                        });
        };
        $scope.dataUrl = 'api/v2/RentCalendar/?shortcut=' + $routeParams.id;
        activate();

        function activate() { }
    }
})();