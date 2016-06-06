(function () {
	'use strict';
	angular
		.module('photo.bel.common')
        .controller('helpCtrl', helpCtrl);

    helpCtrl.$inject = ['$scope'];

    function helpCtrl($scope) {
        $scope.title = 'helpCtrl';

        activate();

        function activate() { }
    }
})();