(function () {
    'use strict';
    angular
		.module('photo.bel.admin.photostudio')
        .controller('activityStreamCtrl', photostudioActivityCtrl);

    photostudioActivityCtrl.$inject = ['$scope', '$http', '$routeParams', '@authService'];

    function photostudioActivityCtrl($scope, $http, $routeParams, authService) {
        $scope.auth = authService;
        $scope.title = 'reserveCtrl';

        $scope.getCount = function (values, state) {
            var count = 0;
            $.each(values, function () {
                if (this.state == state)
                    count++;
            });
            return count;
        }

        $scope.convertTo = function (arr, key, dayWise) {
            var groups = {};
            for (var i = 0; i < arr.length; i++) {
                if (dayWise) {
                    arr[i]['_' + key] = moment(arr[i][key]).format("DD MMMM YYYY");
                }
                else {
                    arr[i]['_' + key] = arr[i][key].toTimeString();
                }
                groups[arr[i]['_' + key]] = groups[arr[i]['_' + key]] || [];
                groups[arr[i]['_' + key]].push(arr[i]);
            }
            return groups;
        };
        $scope.groups = {}

        $http.get("/api/v2/route/" + $routeParams.id + "/Activity?size=100").then(function (data) {
            $scope.activies = data.data.elements;
            $scope.groups = $scope.convertTo(angular.copy(data.data.elements), "activityTime", true);

        });
        $scope.isAdmin = true;
        activate();

        function activate() { }
    }
})();
