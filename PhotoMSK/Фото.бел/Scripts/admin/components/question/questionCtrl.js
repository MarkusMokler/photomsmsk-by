(function () {
	'use strict';
	angular
		.module('photo.bel.admin.question', [])
        .controller('questionAdminCtrl', questionAdminCtrl);

	questionAdminCtrl.$inject = ['$scope'];

	function questionAdminCtrl($scope) {
        $scope.title = 'questionCtrl';

        activate();

        function activate() { }
    }
})();
