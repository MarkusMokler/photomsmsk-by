(function () {
	'use strict';
	angular
		.module('photo.bel.admin.deleteroute', [])
        .controller('deleteRouteAdminCtrl', deleteRouteAdminCtrl);

	deleteRouteAdminCtrl.$inject = ['$scope'];

	function deleteRouteAdminCtrl($scope) {
	    $scope.title = 'deleteRouteAdminCtrl';

        activate();

        function activate() { }
    }
})();