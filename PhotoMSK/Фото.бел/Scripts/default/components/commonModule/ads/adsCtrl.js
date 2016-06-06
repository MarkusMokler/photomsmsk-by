(function () {
	'use strict';
	angular
		.module('photo.bel.common')
        .controller('adsCtrl', adsCtrl);

    adsCtrl.$inject = ['$scope'];

    function adsCtrl($scope) {
        $scope.title = 'adsCtrl';

        activate();

        function activate() { }
    }
})();