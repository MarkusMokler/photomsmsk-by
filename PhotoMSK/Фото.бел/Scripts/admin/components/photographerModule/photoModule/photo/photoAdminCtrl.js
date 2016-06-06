(function () {
    "use strict";
    angular
		.module("photo.bel.admin.photo")
        .controller("photoAdminCtrl", photoAdminCtrl);

    photoAdminCtrl.$inject = ["$scope", "$http", "$routeParams"];

    function photoAdminCtrl($scope, $http, $routeParams) {
        $scope.title = "photoAdminCtrl";
        $scope.setTab = function (val) {
            $scope.tab = val;
        }

        // GET photos list
        $scope.photos = {};
        $http.get("api/v2/photo/?shortcut=" + $routeParams.id).then(function (response) {
            $scope.photos = response.data;
        });

        $scope.setTab("tab1");

        activate();
        function activate() { }
    }
})();