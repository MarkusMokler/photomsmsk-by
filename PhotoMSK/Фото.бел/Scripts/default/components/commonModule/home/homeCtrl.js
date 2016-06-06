(function () {
	'use strict';
	angular
		.module('photo.bel.common')
        .controller('homeCtrl', homeCtrl);

    homeCtrl.$inject = ['$scope'];

    function homeCtrl($scope) {
        $scope.title = 'homeCtrl';

        activate();

        function activate() { }
    }
})();