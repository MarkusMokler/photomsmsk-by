(function () {
	'use strict';
	angular
		.module('photo.bel.photographer')
        .controller('photographerCtrl', photographerCtrl);

    photographerCtrl.$inject = ['$scope', '$routeParams', '$http'];

    function photographerCtrl($scope, $routeParams, $http) {
        $scope.title = 'photgrapherCtrl';
        $http.get('/api/v2/photographer?shortcut=' + $routeParams.id).success(function (response) {
            $scope.item = response;
        });
        activate();

        function activate() { }
    }
})();