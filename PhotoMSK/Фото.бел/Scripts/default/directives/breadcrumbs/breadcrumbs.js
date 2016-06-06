angular.module("photo.bel.directives")
    .directive('breadCrumbs', function () {
        return {
            controller: ['$scope', '$routeParams', '$http', 'crumble', '$rootScope', function ($scope, $routeParams, $http, crumble, $rootScope) {
                var thing = $routeParams.id != null ? $routeParams.id : $scope.title;
                //
                crumble.update({ id: thing });
                $scope.crumble = crumble;
                
            }],
            restrict: 'EA',
            replace: true,
            templateUrl: '/Scripts/default/directives/breadcrumbs/tmpl.html'
        };
    });
