(function () {
    'use strict';

    angular
        .module('photo.bel.model')
        .controller('modelListCtrl', modelListCtrl);

    modelListCtrl.$inject = ['$scope', '$http'];

    function modelListCtrl($scope, $http) {
        $scope.title = 'modelListCtrl';
        var vm = this;
        vm.pagingParams = {};
        $scope.maxSize = 5;
        $scope.currentPage = 1;

        function setScope($scope, response) {
            vm.pagingParams = response;
            $scope.page = response;
            $scope.itemsPerPage = vm.pagingParams.pageSize;
            $scope.numPages = vm.pagingParams.pagesCount;
            $scope.totalItems = vm.pagingParams.itemsCount;
        }

        $http.get('/api/v2/photomodel').success(function(response) {
            setScope($scope, response);
        });

        $scope.pageChanged = function () {
            $http.get('api/v2/photomodel/?from=' + ($scope.currentPage - 1) * vm.pagingParams.pageSize).success(function (response) {
                setScope($scope, response);
            });
        };
        activate();

        function activate() { }
    }
})();
