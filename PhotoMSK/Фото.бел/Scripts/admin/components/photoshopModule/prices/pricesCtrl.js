(function () {
    'use strict';
    angular
		.module('photo.bel.admin.photoshop')
        .controller('pricesCtrl', pricesCtrl);

    pricesCtrl.$inject = ['$scope', '$http', '$routeParams'];
    function pricesCtrl($scope, $http, $routeParams) {
        $scope.title = 'photoshopEditCtrl';
        $http.get("/api/v2/Photoshop/" + $routeParams.id + "/PricePosition")
                    .success(function (data) {
                        $scope.item = data.elements;
                    });
        activate();
        function activate() { }
    }
})();