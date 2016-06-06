(function () {
	'use strict';
	angular
		.module('photo.bel.admin.developer', [])
        .controller('developerAdminCtrl', developerAdminCtrl);

	developerAdminCtrl.$inject = ['$scope'];

	function developerAdminCtrl($scope) {
        $scope.title = 'developerCtrl';

        activate();

        function activate() { }
    }
})();