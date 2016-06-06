(function () {
	'use strict';
	angular
		.module('photo.bel.admin.masterclass', [])
        .controller('masterclassAdminCtrl', masterclassAdminCtrl);

	masterclassAdminCtrl.$inject = ['$scope'];

	function masterclassAdminCtrl($scope) {
        $scope.title = 'masterclassCtrl';

        activate();

        function activate() { }
    }
})();