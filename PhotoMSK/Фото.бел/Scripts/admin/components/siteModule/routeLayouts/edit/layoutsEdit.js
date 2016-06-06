(function () {
    'use strict';
    angular
        .module('photo.bel.admin.site')
        .controller('routeLayoutsEditCtrl', routeLayoutsEditCtrl);

    routeLayoutsEditCtrl.$inject = ['$scope', '$http', '$routeParams'];

    function routeLayoutsEditCtrl($scope, $http, $routeParams) {
        $scope.title = 'layoutsCtrl';
        $http.get('/api/v2/RoutePageLayouts/?shortcut=' + $routeParams.id + '&rpId=' + $routeParams.lid).then(function (response) {
            $scope.rpLayout = response.data;
        });
        activate();
        $scope.currentPage = 1;
        $scope.state = {};
        $scope.state.currentFolderID = null;

        $scope.updateRoutePageLayout = function () {
            $http({
                method: 'PUT',
                url: '/api/v2/RoutePageLayouts/?id=' + $routeParams.lid,
                data: $scope.rpLayout,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        function activate() { }
    }
})();