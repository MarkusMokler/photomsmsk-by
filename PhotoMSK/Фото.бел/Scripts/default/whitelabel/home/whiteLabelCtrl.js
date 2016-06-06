(function () {
    'use strict';
    angular
        .module('photostudio.bel')
        .controller('homeWLCtrl', homeWLCtrl);

    homeWLCtrl.$inject = ['$scope'];

    function homeWLCtrl($scope) {
        $scope.title = 'homeCtrl';

        activate();

        function activate() { }
    }
})();