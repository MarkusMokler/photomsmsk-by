(function () {
    'use strict';
    angular
		.module('photo.bel.account')
        .controller('registerCtrl', registerCtrl);

    registerCtrl.$inject = ['$scope', '$http'];

    function registerCtrl($scope, $http) {
        $scope.title = 'registerCtrl';

        $scope.sendPhone = function () {
            $http.post("/api/Services/Account/Create?id=" + $scope.item.phone).success(function (data) {
                $scope.formState = data.action;
                $scope.item.userId = data.userID;
            }).error(function (data) {
                console.log(data);
                $scope.message = data.message;
            });
        }
        $scope.registerUser = function () {
            $http.post("/api/Services/Account/Register", $scope.item).success(function () {
                location.href = '/login';
                swal({
                    text: "Поздравляем Вас. Вы успешно зарегистрировались!",
                    type: "success",
                    showCancelButton: false,
                    closeOnConfirm: false
                }, function () {
                    location.href = '/login';
                });

            }).error(function (data) {
                console.log(data);
                $scope.userModelErrors = data.modelState;
            });
        }
        $scope.restoreUser = function () {
            $http.post("/api/Services/Account/Reset", $scope.item).then(function (data) {
                swal({
                    text: "Ваш пароль был успешно изменён на новый.",
                    type: "success",
                    showCancelButton: false,
                    closeOnConfirm: false
                }, function () {
                    location.href = '/login';

                });
            });
        }

        $scope.day = moment().format("DD");
        $scope.month = moment().format("MMMM");
        $scope.formState = "phone";
        $scope.item = {};
        $scope.item.phone = "+375296000000";

        var hh = moment().format("HH");

        if (hh > 6 && hh <= 12) {
            $scope.partOfDay = 1;
        }
        if (hh > 12 && hh <= 18) {
            $scope.partOfDay = 2;
        }
        if (hh > 18 & hh <= 24) {
            $scope.partOfDay = 3;
        }
        if (hh > 0 & hh <= 6) {
            $scope.partOfDay = 4;
        }

        activate();

        function activate() { }
    }
})();
