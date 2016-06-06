(function () {
	'use strict';
	angular
		.module('photo.bel.admin.statistics', [])
        .controller('statisticsAdminCtrl', statisticsAdminCtrl);

	statisticsAdminCtrl.$inject = ['$scope'];

	function statisticsAdminCtrl($scope) {
        $scope.title = 'statisticsCtrl';

        activate();

        function activate() { }
    }
})();