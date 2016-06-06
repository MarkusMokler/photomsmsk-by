(function () {
	'use strict';
	angular
		.module('photo.bel.model')
        .controller('photomodelsCtrl', photomodelsCtrl);

    photomodelsCtrl.$inject = ['$scope', '$http', '$routeParams'];

    function photomodelsCtrl($scope, $http, $routeParams) {
        $scope.title = 'photomodelsCtrl';
        $http.get('api/v2/photomodel?id=' + $routeParams.id).success(function(response) {
            $scope.item = response.elements[0];
        });
        activate();
        function activate() { }
    }
})();