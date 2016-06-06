(function () {
    "use strict";
    angular
		.module("photo.bel.admin.creative")
        .controller("creativeAdminCtrl", creativeAdminCtrl);

    creativeAdminCtrl.$inject = ["$scope", "$http", "$routeParams", "$location"];

    function creativeAdminCtrl($scope, $http, $routeParams, $location) {
        $scope.title = "creativeAdminCtrl";
        $scope.setTab = function (val) {
            $scope.tab = val;
        }

        // GET projects list
        $scope.creative = {};
        $http.get("api/v2/creative/?shortcut=" + $routeParams.id).then(function (response) {
            $scope.creative = response.data;
        });

        $scope.setTab("tab1");

        activate();
        function activate() { }
    }
})();