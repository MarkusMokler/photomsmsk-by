(function () {
    "use strict";
    angular
		.module("photo.bel.admin.project")
        .controller("projectAdminCtrl", projectAdminCtrl);

    projectAdminCtrl.$inject = ["$scope", "$http", "$routeParams"];

    function projectAdminCtrl($scope, $http, $routeParams) {
        $scope.title = "projectAdminCtrl";
        $scope.setTab = function (val) {
            $scope.tab = val;
        }

        // GET projects list
        $scope.projects = {};
        $http.get("api/v2/project/?shortcut=" + $routeParams.id).then(function (response) {
            $scope.projects = response.data;
        });

        $scope.setTab("tab1");

        activate();
        function activate() { }
    }
})();