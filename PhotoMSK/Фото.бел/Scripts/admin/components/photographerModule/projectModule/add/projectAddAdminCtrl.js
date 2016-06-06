(function () {
    "use strict";
    angular
		.module("photo.bel.admin.project")
        .controller("projectAddAdminCtrl", projectAddAdminCtrl);

    projectAddAdminCtrl.$inject = ["$scope", "$http", "$routeParams", "$location"];

    function projectAddAdminCtrl($scope, $http, $routeParams, $location) {
        $scope.title = "projectAddAdminCtrl";
        $scope.item = {};
        $scope.routeShortcut = $routeParams.id;
        $scope.item.Shortcut = $routeParams.id;

        $scope.createProject = function () {
            $http({
                method: "POST",
                url: "/api/v2/project/" + $scope.routeShortcut,
                data: $scope.item
            }).then(function (data) {
                swal({
                    title: "Успех!",
                    text: "Проект успешно создан",
                    type: "success",
                    timer: 2000,
                    showConfirmButton: false
                });

                $location.url("admin/photographer/" + $scope.routeShortcut + "/projects");
            }, function () {
                swal({
                    title: "Ошибка",
                    text: "Что то пошло не так :(",
                    type: "error",
                    timer: 2000,
                    showConfirmButton: false
                });
            });
        };

        activate();

        function activate() { }
    }
})();