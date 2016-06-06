(function () {
    'use strict';
    angular
		.module('photo.bel.phototechnic')
        .controller('phototechnicsDetailsCtrl', phototechnicsCtrl);

    phototechnicsCtrl.$inject = ['$scope', '$http', '$routeParams'];

    function phototechnicsCtrl($scope, $http, $routeParams) {
        $scope.title = 'phototechnicCtrl';

        $scope.loadPage = function () {

            $http.get("/api/v2/phototechnics/" + $routeParams.id).then(function (data) {
                $scope.item = data.data;
            });
        };

        activate();

        function activate() {
            $scope.loadPage();
        }
    }
})();
