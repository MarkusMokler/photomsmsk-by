(function () {
    "use strict";
    angular
		.module("photo.bel.admin.route", [])
        .controller("routeAdminCtrl", routeAdminCtrl);

    routeAdminCtrl.$inject = ["$scope"];

    function routeAdminCtrl($scope) {
        $scope.title = "photomodelCtrl";

        activate();

        function activate() { }
    }
})();