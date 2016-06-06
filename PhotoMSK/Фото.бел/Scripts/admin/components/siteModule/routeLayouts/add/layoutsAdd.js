(function () {
    'use strict';
    angular
        .module('photo.bel.admin.site')
        .controller('routeLayoutsAddCtrl', routeLayoutsAddCtrl);

    routeLayoutsAddCtrl.$inject = ['$scope', '$http', '$routeParams', '$sce'];

    function routeLayoutsAddCtrl($scope, $http, $routeParams) {
        
        $http.get('/api/v2/Layouts/?shortcut=' + $routeParams.id).then(function (response) {
            $scope.layouts = response.data;
        });

        $scope.selectedLayout = {};
        $scope.selectedLayout.content = "";

        activate();

        $scope.saveRPLayout = function () {
            $scope.rpLayout.LayoutID = $scope.selectedLayout.id;
            $scope.rpLayout.zones = $scope.selectedLayout.zones;
            $http({
                method: 'POST',
                url: '/api/v2/RoutePageLayouts/?shortcut=' + $routeParams.id,
                data: $scope.rpLayout,
                headers: { 'Content-Type': 'application/json' }
            })
              .then(function (data) {
              });
        };
        activate();

        

        $scope.currentPage = 1;
        $scope.state = {};
        $scope.state.currentFolderID = null;

        function activate() { }
    }
})();