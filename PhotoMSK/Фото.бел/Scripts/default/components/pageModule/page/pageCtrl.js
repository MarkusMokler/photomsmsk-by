(function () {
	'use strict';
	angular
		.module('photo.bel.page')
        .controller('pageCtrl', pageCtrl);
    pageCtrl.$inject = ['$scope', '$http'];
    function pageCtrl($scope, $http) {
        $scope.title = 'pageCtrl';
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

        $http.get('/api/v2/page').success(function(response) {
            setScope($scope, response);
        });

        $scope.pageChanged = function () {
            $http.get('api/v2/page/?from=' + ($scope.currentPage - 1) * vm.pagingParams.pageSize).success(function (response) {
                setScope($scope, response);
            });
        };
        activate();
        function activate() { }
    }
})();