(function () {
    'use strict';
    angular
        .module('photo.bel.photostudio')
        .controller('usersCtrl', usersCtrl);

    usersCtrl.$inject = ['$scope'];

    function usersCtrl($scope) {
        $scope.title = 'usersCtrl';

        activate();

        function activate() { }
    }
})();