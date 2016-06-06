(function () {
    'use strict';

    angular
        .module('photo.bel.photostudio')
        .controller('hallListCtrl', hallListCtrl);

    hallListCtrl.$inject = ['$scope', '$http']; 

    function hallListCtrl($scope, $http) {
        $scope.title = 'hallListCtrl';
        var vm = this;
        vm.pagingParams = {};
        $scope.maxSize = 5;
        $scope.currentPage = 1;
        $scope.modeViewHalls = true;
        function setScope($scope, response) {
            vm.pagingParams = response;
            $scope.page = response;
            $scope.itemsPerPage = vm.pagingParams.pageSize;
            $scope.numPages = vm.pagingParams.pagesCount;
            $scope.totalItems = vm.pagingParams.itemsCount;
        }

        $http.get('/api/v2/hall').success(function(response) {
            setScope($scope, response);
        });

        $scope.pageChanged = function () {
            $http.get('api/v2/hall/?from=' + ($scope.currentPage - 1) * vm.pagingParams.pageSize).success(function (response) {
                setScope($scope, response);
            });
        };

        activate();

        function activate() { }
    }
})();
