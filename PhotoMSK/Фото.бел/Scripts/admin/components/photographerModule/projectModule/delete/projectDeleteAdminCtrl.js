(function () {
    "use strict";
    angular
		.module("photo.bel.admin.project")
        .controller("projectDeleteAdminCtrl", projectDeleteAdminCtrl);

    projectDeleteAdminCtrl.$inject = ["$scope", "$http", "$routeParams"];

    function projectDeleteAdminCtrl($scope, $http, $routeParams) {
        $scope.title = "projectDeleteAdminCtrl";

        // Remove the project
        $http.delete("api/v2/project/?shortcut=" + $routeParams.id + "&id=" + $routeParams.projectId).success(function () {
            // Back to the projects list

        });
        
    }
})();