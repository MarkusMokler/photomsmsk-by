(function () {
    'use strict';
    angular
		.module('iframecalendar.bel')
        .controller('calendarIFrameCtrl', calendarIFrameCtrl);
    calendarIFrameCtrl.$inject = ['$scope', '$routeParams', "$http"];
    function calendarIFrameCtrl($scope, $routeParams, $http) {
        $scope.dataUrl = '/api/v2/photostudio/' + $routeParams.id;
        $scope.isAdmin = false;
        activate();
        function activate() { }

        $scope.shortcut = $routeParams.id;

        $http.get($scope.dataUrl).then(function (response) {
            $scope.item = response.data;
            $scope.calendars = [];
            angular.forEach($scope.item.halls, function (item) {
                $scope.calendars.push(item.calendarID);
            });
        });
    }
})();