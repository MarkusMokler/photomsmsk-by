(function () {
	'use strict';
	angular
		.module('photo.bel.common')
        .controller('battleCtrl', battleCtrl);

    battleCtrl.$inject = ['$scope'];

    function battleCtrl($scope) {
        $scope.title = 'battleCtrl';

        activate();

        function activate() { }
    }
})();