(function () {
	'use strict';
	angular
		.module('photo.bel.photostudio')
        .controller('templatesCtrl', templatesCtrl);

    templatesCtrl.$inject = ['$scope'];

    function templatesCtrl($scope) {
        $scope.title = 'templatesCtrl';

        activate();

        function activate() { }
    }
})();