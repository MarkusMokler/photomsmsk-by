angular.module("photo.bel.admin.directivies")
    .directive('topMenu', function () {
        return {
            controller: ['$scope', '$http', '$location', '$routeParams', '$rootScope', function ($scope, $http, $location, $routeParams, $rootScope) {
            }],
            restrict: 'EA',
            templateUrl: '/Scripts/admin/components/shared/_TopAdminMenu.html'
        };
    });
