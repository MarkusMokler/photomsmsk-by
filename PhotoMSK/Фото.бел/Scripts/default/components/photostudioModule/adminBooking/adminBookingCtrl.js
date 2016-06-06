(function () {
	'use strict';
	angular
		.module('photo.bel.photostudio')
        .controller('adminBookingCtrl', adminBookingCtrl);

    adminBookingCtrl.$inject = ['$scope'];

    function adminBookingCtrl($scope) {
        $scope.title = 'adminBookingCtrl';

        activate();

        function activate() { }
    }
})();