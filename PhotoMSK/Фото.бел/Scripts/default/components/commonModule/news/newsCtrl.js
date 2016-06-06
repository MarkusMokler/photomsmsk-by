(function () {
	'use strict';
	angular
		.module('photo.bel.common')
        .controller('newsCtrl', newsCtrl);

    newsCtrl.$inject = ['$scope'];

    function newsCtrl($scope) {
        $scope.title = 'newsCtrl';

        activate();

        function activate() { }
    }
})();