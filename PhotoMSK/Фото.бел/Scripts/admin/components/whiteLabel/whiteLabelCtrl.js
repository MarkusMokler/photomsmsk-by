(function () {
	'use strict';
	angular
		.module('photo.bel.admin.whitelabel', [])
        .controller('whiteLabelAdminCtrl', whiteLabelAdminCtrl);

	whiteLabelAdminCtrl.$inject = ['$scope'];

	function whiteLabelAdminCtrl($scope) {
        $scope.title = 'whiteLabelCtrl';

        activate();

        function activate() { }
    }
})();