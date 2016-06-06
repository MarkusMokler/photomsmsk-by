(function () {
	'use strict';
	angular
		.module('photo.bel.admin.photostudio')
        .controller('photostudioAdminCtrl', photostudioAdminCtrl);

	photostudioAdminCtrl.$inject = ['$scope', '$http', '$location'];

	function photostudioAdminCtrl($scope, $http, $location) {
	    $scope.title = 'photostudioAdminCtrl';
	    $scope.setTab = function(val) {
	        $scope.tab = val;
	    }

	    $scope.photosURL = '/api/v2/routephotos/?shortcut=' + $scope.shortcut;
	    $scope.tab = 'tab1';
	    $scope.title = 'photostudioAdminCtrl';
	    $scope.item = {};
	    $scope.phone = {};
	    $scope.addPhotostudio = function () {
	        $http({
	            method: 'POST',
	            url: '/api/v2/photostudio',
	            data: $scope.item,
	            headers: { 'Content-Type': 'application/json' }
	        })
	            .success(function (data) {
	                $scope.dataEntityID = data.id;
	            });
	        $scope.setTab('tab2');
	    };
	    $scope.loadPhoto = function () {
	        $scope.setTab('tab4');
	    }
	    $scope.updateData = function () {
	        $scope.setTab('tab3');
	    }
	    $scope.add = function () {
	        $location.path('/admin');
	    };
	    activate();

	    function activate() { }
    }
})();