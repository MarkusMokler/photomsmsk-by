(function () {
	'use strict';
	angular
		.module('photo.bel.admin.plasticcard', [])
        .controller('plasticCardAdminCtrl', plasticCardAdminCtrl);

	plasticCardAdminCtrl.$inject = ['$scope'];

	function plasticCardAdminCtrl($scope) {
        $scope.title = 'plasticCardCtrl';

        activate();

        function activate() { }
    }
})();