(function () {
	'use strict';
	angular
		.module('photo.bel.admin.usersmanagement', [])
        .controller('usersManagementAdminCtrl', usersManagementAdminCtrl);

	usersManagementAdminCtrl.$inject = ['$scope'];

	function usersManagementAdminCtrl($scope) {
        $scope.title = 'usersManagementCtrl';

        activate();

        function activate() { }
    }
})();
