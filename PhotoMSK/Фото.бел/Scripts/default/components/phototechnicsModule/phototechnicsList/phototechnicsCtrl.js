(function () {
    'use strict';
    angular
		.module('photo.bel.phototechnic')
        .controller('phototechnicCtrl', phototechnicsCtrl);

    phototechnicsCtrl.$inject = ['$scope', '$http'];

    function phototechnicsCtrl($scope, $http) {
        $scope.title = 'phototechnicCtrl';
     
        $scope.loadPage = function (page, pageSize) {

            $http.get("/api/v2/phototechnics?page=" + (page || 0) + "&pageSize=" + (pageSize || 20)).then(function (data) {
                $scope.itemsPage = data.data;
            });
        };

        activate();

        function activate() {
            $scope.loadPage();
        }
    }
})();
