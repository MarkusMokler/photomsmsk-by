(function () {
	'use strict';
	angular
		.module('photo.bel.photoshop')
        .controller('photoshopCtrl', photoshopCtrl);

    photoshopCtrl.$inject = ['$scope'];

    function photoshopCtrl($scope) {
        $scope.title = 'photoshopCtrl';
        console.log("Photoshop");
        activate();

        function activate() { }
    }
})();