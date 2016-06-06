(function () {
	'use strict';
	angular
		.module('photo.bel.photostudio')
        .controller('catalogCtrl', catalogCtrl);

    catalogCtrl.$inject = ['$scope'];

    function catalogCtrl($scope) {
        $scope.title = 'catalogCtrl';

        activate();

        function activate() { }
    }
})();