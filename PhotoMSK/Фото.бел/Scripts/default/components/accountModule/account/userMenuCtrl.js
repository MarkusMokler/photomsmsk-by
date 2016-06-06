(function () {
    'use strict';
    angular
		.module('photo.bel.account')
        .controller('userMenuCtrl', accountCtrl);

    accountCtrl.$inject = ['$scope', '$http', '@authService'];

    function accountCtrl($scope, $http, authService) {
        $scope.showFuture = false;
        $scope.authService = authService;
        $scope.userInformation = {};
        $scope.showFutureChange = function($event) {
            $scope.showFuture = !$scope.showFuture;
            $scope.futurePopupX = $event.clientX - 120;
            $scope.futurePopupY = $event.clientY + 30;
            
        }
        $http.get("/api/Services/Account/GetUserInformation").then(function (data) {
            authService.authentication.isAuth = data.data.isAuthenticated;
            $scope.userInformation = data.data.userInformation;
            if (authService.authentication.isAuth) {
                $http.get("/api/v2/userinformation/").success(function (data) {
                    $scope.userEventsData = data.data;
                });
            }
            
        });

        $scope.$watch('authService.authentication.isAuth ', function () {
            $http.get("/api/Services/Account/GetUserInformation").then(function (data) {
                $scope.userInformation = data.data.userInformation;
            });
        });

        activate();

        function activate() { }
    }
})();