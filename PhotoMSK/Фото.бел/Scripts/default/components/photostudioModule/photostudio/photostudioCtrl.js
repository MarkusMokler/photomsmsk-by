
(function() {
    'use strict';

    angular
        .module('photo.bel.photostudio')
        .controller('photostudioCtrl', photostudioCtrl);

    photostudioCtrl.$inject = ['$scope', '$routeParams', '$http', '$compile', "$location", "$permission"];

    function photostudioCtrl($scope, $routeParams, $http,$location, $permission) {
        if (typeof($scope.WhiteLabel) == "undefined" || $scope.WhiteLabel == null || $scope.WhiteLabel == " ") {
            $scope.dataUrl = '/api/v2/photostudio/' + $routeParams.id;
            $scope.shortcut = $routeParams.id;
        } else {
            $scope.dataUrl = '/api/v2/photostudio/' + $scope.WhiteLabel;
            $scope.shortcut = $scope.WhiteLabel;
        }

        $scope.isAdmin = $permission.isAdmin;
        activate();
        
        $http.get($scope.dataUrl).then(function (response) {
            $scope.item = response.data;
            $scope.calendars = [];
            angular.forEach($scope.item.halls, function (item) {
                $scope.calendars.push(item.calendarID);
            });
        });

        function activate() {}

        // $scope.getTotalSquare = function() {
        //     var total = 0;
        //     for (var i = 0; i < $scope.item.halls.length; i++) {
        //         var total += $scope.item.halls[i].square;
        //     }
        //     return total;
        // }

        // for(var i = 0; i < $scope.item.halls.length; i++){
        //     $scope.square += $scope.item.halls[i].square;
        // }

        // var $scope.total = 123456;
        // for (var i = 0; i < 10; i++) {
        //     // $scope.total += i;
        // }
    }

})();
