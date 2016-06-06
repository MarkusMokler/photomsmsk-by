(function () {
	'use strict';
	angular
		.module('photo.bel.admin.dialog', [])
        .controller('dialogAdminCtrl', dialogAdminCtrl);

	dialogAdminCtrl.$inject = ['$scope'];

	function dialogAdminCtrl($scope) {
        $scope.title = 'dialogCtrl';

        activate();

        function activate() { }
    }
})();