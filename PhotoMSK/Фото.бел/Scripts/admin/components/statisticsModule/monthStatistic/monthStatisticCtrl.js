(function () {
    'use strict';
    angular
		.module('photo.bel.admin.photostudio')
        .controller('monthStatisticCtrl', photostudioDashboardCtrl);

    photostudioDashboardCtrl.$inject = ['$scope', '$http', '$routeParams', '@authService'];

    function photostudioDashboardCtrl($scope, $http, $routeParams, authService) {
        $scope.auth = authService;
        $scope.title = 'reserveCtrl';


        var fetch = function () {
            $http.get("/api/v2/Reports/" + $routeParams.id + "/CalendarMonth?date=" + $scope.date.format('YYYY-MM-DD HH:mm:ss')).then(function (data) {
                $scope.item = data.data;
            });
        }

        $scope.date = moment();

        $scope.before = function () {
            $scope.date.add(-1, "month");
            fetch();
        }

        $scope.after = function () {
            $scope.date.add(1, "month");
            fetch();
        }

        $scope.getCount = function (values, state) {
            var count = 0;
            $.each(values, function () {
                if (this.state == state)
                    count++;
            });
            return count;
        }


        fetch();

        $scope.isAdmin = true;
        activate();

        function activate() { }
    }
})();