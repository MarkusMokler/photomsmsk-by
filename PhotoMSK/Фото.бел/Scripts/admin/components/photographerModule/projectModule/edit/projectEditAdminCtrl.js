(function () {
    "use strict";
    angular
		.module("photo.bel.admin.project")
        .controller("projectEditAdminCtrl", projectEditAdminCtrl);

    projectEditAdminCtrl.$inject = ["$scope", "$http", "$routeParams", "$location"];

    function projectEditAdminCtrl($scope, $http, $routeParams, $location) {
        $scope.title = "projectEditAdminCtrl";
        $scope.photographerShortcut = $routeParams.id;
        $scope.item = {};

        $scope.tab = "tab1";

        // Change tab
        $scope.setTab = function (tab) {
            $scope.tab = tab;
        };

        // GET the project
        $http.get("api/v2/project/?id=" + $routeParams.projectId).then(function (response) {
            $scope.project = response.data;
        });

        $scope.routeTypeSelected = "Photographer";
        $scope.updateProject = function () {
            $http({
                method: "PUT",
                url: "/api/v2/project/?id=" + $scope.project.id,
                data: $scope.project
            });
        };

        activate();
        function activate() { }
    }
})();