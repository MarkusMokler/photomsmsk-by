(function () {
    'use strict';

   angular
		.module('photo.bel.masterclass')
        .controller('masterclassCtrl', masterclassCtrl);

    masterclassCtrl.$inject = ['$scope', '$http'];

    function masterclassCtrl($scope, $http) {
        $scope.title = 'masterclassCtrl';
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

        $http.get('/api/v2/masterclass').success(function(response) {
            setScope($scope, response);
        });
        $scope.pageChanged = function () {
            $http.get('api/v2/masterclass/?from=' + ($scope.currentPage - 1) * vm.pagingParams.pageSize).success(function (response) {
                setScope($scope, response);
            });
        };
        activate();

        function activate() { }
    }
})();