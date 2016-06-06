(function () {
	'use strict';
	angular
		.module('photo.bel.admin.blog', [])
        .controller('blogAdminCtrl', blogAdminCtrl);

	blogAdminCtrl.$inject = ['$scope'];

	function blogAdminCtrl($scope) {
        $scope.title = 'blogCtrl';

        activate();

        function activate() { }
    }
})();