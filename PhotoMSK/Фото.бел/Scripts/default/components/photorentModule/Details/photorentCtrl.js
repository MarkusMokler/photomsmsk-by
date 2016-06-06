(function () {
	'use strict';
	angular
		.module('photo.bel.photorent')
        .controller('photorentCtrl', photorentCtrl);

    photorentCtrl.$inject = ['$scope'];

    function photorentCtrl($scope) {
        $scope.title = 'photorentCtrl';
        console.log("Photorent");
        activate();

        function activate() { }
    }
})();