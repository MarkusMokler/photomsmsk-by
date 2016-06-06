(function () {
    'use strict';
    angular
		.module('photo.bel.account')
        .controller('accountCtrl', accountCtrl);

    accountCtrl.$inject = ['$scope', '$http', '@authService'];

    function accountCtrl($scope, $http, authService) {
        $scope.title = "accountCtrl";
        $scope.item = {};
        $scope.item.userName = "";
        $scope.item.password = "";
        $scope.day = moment().format("DD");
        $scope.month = moment().format("MMMM");

        $scope.login = function () {
            authService.login($scope.item).then(function () {
                swal({
                    title: 'Добро пожаловать!',
                    text: 'Вы авторизировались на портале www.ФОТО.бел!',
                    type: "success",
                    timer: 2000,
                    showConfirmButton: false
                });
                window.location.href = '/';
                
            }, function () {
                $scope.loginError = "Пользователя с таким логином или паролем не существует";
                $scope.loginErrorEmpty = false;
            });
        }

        activate();

        function activate() { }
    }
})();
