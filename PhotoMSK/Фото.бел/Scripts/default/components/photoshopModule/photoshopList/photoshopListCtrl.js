(function () {
    'use strict';

    angular
        .module('photo.bel.photoshop')
        .controller('photoshopListCtrl', photoshopListCtrl);

    photoshopListCtrl.$inject = ['$scope', '$http'];

    function photoshopListCtrl($scope, $http) {
        $scope.title = 'photoshopListCtrl';
        $http.get('/api/v2/photoshop').success(function (response) {
            $scope.page = response;
        })
        activate();

        function activate() { }
    }
})();
