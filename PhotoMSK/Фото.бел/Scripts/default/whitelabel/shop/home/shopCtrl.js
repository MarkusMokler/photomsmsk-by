(function () {
    'use strict';
    angular
        .module('photoshop.bel.module.home', [])
        .controller('shopCtrl', shopCtrl);

    shopCtrl.$inject = ['$scope'];

    function shopCtrl($scope) {
        $scope.title = 'shopCtrl';

        activate();

        function activate() { }
    }
})();