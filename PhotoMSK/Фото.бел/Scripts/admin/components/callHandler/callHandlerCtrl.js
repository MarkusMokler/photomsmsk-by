(function () {
	'use strict';
	angular
		.module('photo.bel.admin.callhandler', [])
        .controller('callHandlerAdminCtrl', callHandlerAdminCtrl);

	callHandlerAdminCtrl.$inject = ['$scope'];

	function callHandlerAdminCtrl($scope) {
        $scope.title = 'callHandlerCtrl';

        activate();

        function activate() { }
    }
})();