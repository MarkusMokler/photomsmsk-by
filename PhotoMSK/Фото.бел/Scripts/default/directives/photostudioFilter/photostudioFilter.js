angular.module("photo.bel.directives")
    .directive('photostudioFilter', function () {
        return {
            controller: ['$scope', '$routeParams', '$http', function ($scope, $routeParams, $http) {
                $scope.showListStudios = false;
            }],
            restrict: 'AE',
            replace: true,
            templateUrl: '/Scripts/default/directives/photostudioFilter/tmpl.html'
        };
    });
