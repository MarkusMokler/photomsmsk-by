angular.module("photo.bel.admin.directivies")
    .directive('adminHallsPage', function () {
        return {
            controller: ['$scope', '$http', function ($scope, $http) {
                $scope.deleteHall = function(id) {
                    $http({
                        method: 'DELETE',
                        url: '/api/v2/photostudio/' + $scope.item.shortcut + "/hall?shortcut=" + $scope.item.shortcut + "&id=" + id
                    });
                };
            }],
            restrict: 'EA',
            templateUrl: '/Scripts/admin/directivies/halls/index.html'
        };
    });