(function () {
	'use strict';
	angular
		.module('photo.bel.photostudio')
        .controller('calendarCtrl', calendarCtrl);

    calendarCtrl.$inject = ['$scope'];

    function calendarCtrl($scope) {
        $scope.title = 'calendarCtrl';
        var date = new Date();
        var d = date.getDate();
        var m = date.getMonth();
        var y = date.getFullYear();

        //$http.get('/api/v2/!get api!')
        activate();

        function activate() { }
    }
})();