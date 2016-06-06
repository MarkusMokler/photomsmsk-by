angular.module("photo.bel.admin.directivies")
    .directive('adminParticipantsPage', function () {
        return {
            controller: ['$scope', "$routeParams", function ($scope, $routeParams) {
                $scope.isModalShow = false;
                $scope.shortcut = $routeParams.id;
                $scope.showModal = function() {
                    $scope.isModalShow = !$scope.isModalShow;
                }
            }],
            restrict: 'EA',
            templateUrl: '/Scripts/admin/directivies/participants/index.html'
        };
    });
