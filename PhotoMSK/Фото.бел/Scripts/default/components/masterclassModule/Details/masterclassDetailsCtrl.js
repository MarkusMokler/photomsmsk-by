(function () {
    'use strict';

    angular
         .module('photo.bel.masterclass')
         .controller('masterclassCtrl', masterclassCtrl);

    masterclassCtrl.$inject = ['$scope', '$http'];

    function masterclassCtrl($scope, $http) {
        $scope.title = 'masterclassDetailsCtrl';

        activate();

        function activate() { }
    }
})();