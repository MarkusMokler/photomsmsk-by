(function () {
	'use strict';
	angular
		.module('photo.bel.admin.photomodel', [])
        .controller('photomodelAdminCtrl', photomodelAdminCtrl);

	photomodelAdminCtrl.$inject = ['$scope'];

	function photomodelAdminCtrl($scope) {
        $scope.title = 'photomodelCtrl';

        activate();

        function activate() { }
    }
})();