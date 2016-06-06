(function () {
    'use strict';
    angular
        .module('photo.bel.photostudio')
        .controller('settingsCtrl', settingsCtrl);

    settingsCtrl.$inject = ['$scope'];

    function settingsCtrl($scope) {
        $scope.title = 'settingsCtrl';

        activate();

        function activate() { }
    }
})();