angular.module("photostudio.bel.directives")
    .directive('wlMenuTop', function () {
        return {
            controller: ['$scope', '$http', function($scope, $http) {
                    if ($scope.WhiteLabel != "undefined" && $scope.WhiteLabel != null && $scope.WhiteLabel != " ") {
                        $http.get('api/v2/MenuItem/?shortcut=' + $scope.WhiteLabel).success(function(data) {
                            $scope.menuItemWL = data;
                        });
                    }
                }
            ],
            restrict: 'EA',
            replace: true,
            templateUrl: 'Scripts/default/whitelabel/directives/wlmenu/index.html'
        };
    });
