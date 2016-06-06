(function () {
    "use strict";
    angular
		.module("photo.bel.home")
        .controller("homeCtrl", homeCtrl);

    homeCtrl.$inject = ["$scope", "$http", "$location"];

    function homeCtrl($scope, $http, $location) {
        $scope.title = "homeCtrl";
        $scope.search = "api/v2/search/?searchRoute=";
        $scope.selected = null;
        var handler = function () {
            if ($scope.selected != null) {
                console.log($scope.selected);
                if ($scope.selected.originalObject.etype === "Photostudio") {
                    $location.url("/photostudio/" + $scope.selected.originalObject.shortcut);
                }
                if ($scope.selected.originalObject.etype === "Photorent") {
                    $location.url("/photostudio/" + $scope.selected.originalObject.shortcut);
                }
                if ($scope.selected.originalObject.etype === "Photoshop") {
                    $location.url("/photostudio/" + $scope.selected.originalObject.shortcut);
                }

                if ($scope.selected.originalObject.etype === "Photoshop") {
                    $location.url("/photostudio/" + $scope.selected.originalObject.shortcut);
                }
            }
        };

        // Top projects list
        $http.get("api/v2/project/").success(function (response) {
            $scope.projects = response;
        });

        $scope.projectFilter = function (item) {
            var result = (!$scope.projectName || item.name.toLowerCase().includes($scope.projectName.toLowerCase())) &&
                   (!$scope.priceFrom || item.price >= $scope.priceFrom) &&
                   (!$scope.priceTo || item.price <= $scope.priceTo);
            return result;
        };

        // Last month winner
        $http.get("api/v2/winner/").success(function (response) {
            $scope.winner = response;
        });

        // Photographer list
        $http.get("api/v2/photographer/").success(function (response) {
            $scope.photographer = response;
        });

        // Get last month
        function getLastMonth() {
            var month = [
                "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль",
                "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
            ];

            var d = new Date();
            var n = month[d.getMonth() - 1] + " " + d.getFullYear();
            return n;
        }
        $scope.lastMonth = getLastMonth();

        $scope.$watch("selected", handler);
        activate();
        function activate() { }
    }
})();
