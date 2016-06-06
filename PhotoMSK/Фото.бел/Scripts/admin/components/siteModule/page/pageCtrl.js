(function () {
    'use strict';
    angular
        .module('photo.bel.admin.site')
        .controller('pageWLCtrl', pageWLCtrl);

    pageWLCtrl.$inject = ['$scope', '$http', '$routeParams'];

    function pageWLCtrl($scope, $http, $routeParams) {
        $scope.title = 'pageWLCtrl';
        $scope.shortcut = $routeParams.id;

        $http.get('api/v2/TextPage/?shortcut=' + $routeParams.id).success(function (data) {
            $scope.routePages = data;
        });

        $scope.setAsDefault = function (page) {
            $http({
                method: 'PUT',
                url: '/api/v2/route/' + $scope.shortcut + '/page',
                data: page,
                headers: { 'Content-Type': 'application/json' }
            });
        }

        activate();
        function activate() { }
    }
})();