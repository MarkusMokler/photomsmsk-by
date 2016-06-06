(function () {
    'use strict';
    angular
        .module('photorent.bel')
        .controller('rentAppCtrl', rentAppCtrl);

    rentAppCtrl.$inject = ['$scope'];

    function rentAppCtrl($scope) {
        $scope.title = 'rentAppCtrl';

        activate();

        function activate() { }
    }
})();