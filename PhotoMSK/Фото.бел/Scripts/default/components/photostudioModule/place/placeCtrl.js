(function () {
	'use strict';
	angular
		.module('photo.bel.photostudio')
        .controller('placeCtrl', placeCtrl);

    placeCtrl.$inject = ['$scope'];

    function placeCtrl($scope) {
        $scope.title = 'placeCtrl';

        activate();

        function activate() { }
    }
})();