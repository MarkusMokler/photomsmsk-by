(function () {
    "use strict";
    angular
		.module("photo.bel.admin.hall")
        .controller("hallAddAdminCtrl", hallAddAdminCtrl);

    hallAddAdminCtrl.$inject = ["$scope", "$routeParams", "$http", "$location"];

    function hallAddAdminCtrl($scope, $routeParams, $http, $location) {
        $scope.title = "hallAddAdminCtrl";
        $scope.item = {};

        $scope.createHall = function () {
            $http({
                method: "POST",
                url: "/api/v2/photostudio/" + $routeParams.id + "/hall",
                data: $scope.item
            }).then(function (data) {
                swal({
                    title: "Успех!",
                    text: "Зал успешно добавлен",
                    type: "success",
                    timer: 2000,
                    showConfirmButton: false
                });

                $location.url("admin/photostudio/" + $routeParams.id + "/hall/" + data.data.id + "/edit");
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