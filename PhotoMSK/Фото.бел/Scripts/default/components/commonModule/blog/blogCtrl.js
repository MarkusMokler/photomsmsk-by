(function () {
	'use strict';
	angular
		.module('photo.bel.common')
        .controller('blogCtrl', blogCtrl);

    blogCtrl.$inject = ['$scope'];

    function blogCtrl($scope) {
        $scope.title = 'blogCtrl';

        activate();

        function activate() { }
    }
})();