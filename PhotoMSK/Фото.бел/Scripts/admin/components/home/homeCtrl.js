(function () {
    'use strict';
    angular
		.module('photo.bel.admin.home', ['ngAnimate', 'ui.bootstrap'])
        .controller('homeAdminCtrl', homeAdminCtrl);

    homeAdminCtrl.$inject = ['$scope', '$http', '@authService'];

    function homeAdminCtrl($scope, $http, authService) {
        $scope.title = 'homeCtrl';
        $scope.setTab = function(val) {
            $scope.tab = val;
        }
        $scope.setTab('tab1');
        $scope.delete = function(id) {
            $http({
                method: 'DELETE',
                url: '/api/v2/photostudio/' + id
            });
        };
        $scope.auth = authService;
 
        activate();

        function activate() { }
    }
})();