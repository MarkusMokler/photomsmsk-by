(function () {
	'use strict';
	angular
		.module('photo.bel.photostudio')
        .controller('landingCtrl', landingCtrl);

    landingCtrl.$inject = ['$scope'];

    function landingCtrl($scope) {
        $scope.title = 'landingCtrl';

        activate();

        function activate() { }
    }
})();