(function () {
    'use strict';

    angular
        .module('photo.bel.photostudio')
        .controller('photostudioListCtrl', photostudioListCtrl);

    photostudioListCtrl.$inject = ['$scope','$http'];

    function photostudioListCtrl($scope,$http) {
        $scope.title = 'hallListCtrl';
        var vm = this;
        vm.pagingParams = {};
        $scope.maxSize = 5;
        $scope.currentPage = 1;
        $scope.modeViewPhotostudios = true;
        function setScope($scope, response) {
            vm.pagingParams = response;
            $scope.page = response;
            $scope.itemsPerPage = vm.pagingParams.pageSize;
            $scope.numPages = vm.pagingParams.pagesCount;
            $scope.totalItems = vm.pagingParams.itemsCount;

        }

        $http.get('/api/v2/photostudio').success(function(response) {
            setScope($scope, response);
        });

        $scope.pageChanged = function () {
            $http.get('api/v2/photostudio/?from=' + ($scope.currentPage - 1) * vm.pagingParams.pageSize).success(function (response) {
                setScope($scope, response);
            });
        };

        var _map;
        $scope.afterMapInit = function (map) {
            _map = map;
        };
        $scope.mapObj = [];
        $scope.mapObj.longitude = 27.5;
        $scope.mapObj.latitude = 53.9;
        $scope.getStudioMap = function (id) {
            $.each($scope.page.elements, function() {
                if (this.id == id) {
                    $scope.mapObj = this;
                    return;
                }
            });
            $scope.geoObject = {
                geometry: {
                    type: 'Point',
                    coordinates: [$scope.mapObj.longitude, $scope.mapObj.latitude]
                },
                properties: {
                    balloonContent: $scope.mapObj.adress,
                    iconContent: $scope.mapObj.name,
                    hintContent: $scope.mapObj.adress
                }
            }
        }
        activate();

        function activate() { }
    }
})();
