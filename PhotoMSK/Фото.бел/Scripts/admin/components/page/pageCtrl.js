(function () {
	'use strict';
	angular
		.module('photo.bel.admin.page', [])
        .controller('pageAdminCtrl', pageAdminCtrl);

	pageAdminCtrl.$inject = ['$scope'];

	function pageAdminCtrl($scope) {
        $scope.title = 'pageCtrl';

        activate();

        function activate() { }
    }
})();
