(function () {
    'use strict';

    angular
        .module('photo.bel.photorent')
        .controller('photorentListCtrl', photorentListCtrl);

    photorentListCtrl.$inject = ['$scope', '$http'];

    function photorentListCtrl($scope, $http) {
        $scope.title = 'photorentListCtrl';
        $http.get('/api/v2/photorent').success(function (response) {
            $scope.page = response;
        })
        activate();

        function activate() { }
    }
})();
