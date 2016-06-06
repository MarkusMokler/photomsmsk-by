(function () {
	'use strict';
	angular
		.module('photo.bel.admin.cardchecker', [])
        .controller('cardCheckerAdminCtrl', cardCheckerAdminCtrl);

	cardCheckerAdminCtrl.$inject = ['$scope'];

	function cardCheckerAdminCtrl($scope) {
        $scope.title = 'cardCheckerCtrl';

        activate();

        function activate() { }
    }
})();