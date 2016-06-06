(function () {
    'use strict';
    angular
        .module('photo.bel.photostudio')
        .controller('sharedCtrl', sharedCtrl);

    sharedCtrl.$inject = ['$scope'];

    function sharedCtrl($scope) {
        $scope.title = 'sharedCtrl';

        activate();

        function activate() { }
    }
})();