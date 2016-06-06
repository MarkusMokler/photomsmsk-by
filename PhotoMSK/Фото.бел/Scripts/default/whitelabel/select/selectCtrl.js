(function () {
    'use strict';
    angular
        .module('photo.bel.photostudio')
        .controller('selectCtrl', selectCtrl);

    selectCtrl.$inject = ['$scope'];

    function selectCtrl($scope) {
        $scope.title = 'selectCtrl';

        activate();

        function activate() { }
    }
})();