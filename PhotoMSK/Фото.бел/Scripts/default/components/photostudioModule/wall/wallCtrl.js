(function () {
	'use strict';
	angular
		.module('photo.bel.photostudio')
        .controller('wallCtrl', wallCtrl);

    wallCtrl.$inject = ['$scope'];

    function wallCtrl($scope) {
        $scope.title = 'wallCtrl';

        activate();

        function activate() { }
    }
})();