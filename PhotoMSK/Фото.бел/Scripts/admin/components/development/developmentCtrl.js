(function () {
	'use strict';
	angular
		.module('photo.bel.admin.development', [])
        .controller('developmentAdminCtrl', developmentAdminCtrl);

	developmentAdminCtrl.$inject = ['$scope'];

	function developmentAdminCtrl($scope) {
        $scope.title = 'developmentCtrl';

        activate();

        function activate() { }
    }
})();
