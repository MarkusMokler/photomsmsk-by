(function () {
    'use strict';
    angular
		.module('photo.bel.photostudio')
        .controller('hallCtrl', hallCtrl);

    hallCtrl.$inject = ['$scope', '$routeParams', '$http'];

    function hallCtrl($scope, $routeParams, $http) {
        $scope.title = 'hallCtrl';


        var loadLanding = function () {
            if ($scope.item.landingPageID != null)
                $http.get("api/v2/Landing/" + $scope.item.landingPageID).then(function (response) {
                    $scope.widgets = response.data.widgets;
                });
        }


        $scope.getHall = function (id) {
            if (typeof id == "undefined" || id == null) {
                id = $routeParams.id;
            }
            $http.get('/api/v2/hall?id=' + id).success(function (data) {
                $scope.item = data.elements[0];
                loadLanding();
                $http.get('api/v2/photostudio?id=' + $scope.item.photostudioName).success(function (data) {
                    var key = $scope.getThisHallKey(data.halls);
                    if (key > 0 && key < data.halls.length - 1) {
                        $scope.prevHall = data.halls[key - 1];
                        $scope.nextHall = data.halls[key + 1];
                    } else {
                        if (key === 0) {
                            $scope.prevHall = data.halls[data.halls.length - 1];
                            $scope.nextHall = data.halls[key + 1];
                        } else {
                            $scope.prevHall = data.halls[key - 1];
                            $scope.nextHall = data.halls[0];
                        }
                    }
                });
            });
        }
        $scope.getHall();
        $scope.getThisHallKey = function (halls) {
            var hallKey;
            $.each(halls, function (key) {
                if (halls[key].id === $scope.item.id) {
                    hallKey = key;
                    return;
                }
            });
            return hallKey;
        }
        $scope.dayOfWeekStr = ['Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб', 'Вс'];

        activate();

        function activate() { }
    }
})();