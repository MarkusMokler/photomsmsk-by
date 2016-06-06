(function () {
    'use strict';
    angular
		.module('photo.bel.admin.photostudio')
        .controller('photostudioReserveCtrl', photostudioReserveCtrl);

    photostudioReserveCtrl.$inject = ['$scope', '$http', '$routeParams', '@authService', '$location', "$permission"];

    function photostudioReserveCtrl($scope, $http, $routeParams, authService, $location, $permission) {
        $scope.authService = authService;
        $scope.userInformation = {};
        $scope.isAdmin = $permission.isAdmin();
        $http.get("/api/Services/Account/GetUserInformation").then(function (data) {
            authService.authentication.isAuth = data.data.isAuthenticated;
            $scope.userInformation = data.data.userInformation;
            if (!data.data.isAuthenticated) {
                swal({
                    title: '',
                    text: data.message,
                    type: "error",
                    timer: 2000,
                    showConfirmButton: false
                });
            } else {
                $http.get("/api/Services/Account/CheckUserAccess?id=" + $scope.userInformation.id + "&shortcut=" + $routeParams.id).error(function (data) {
                    $location.url("/admin");
                    swal({
                        title: '',
                        text: data.message,
                        type: "error",
                        timer: 2000,
                        showConfirmButton: false
                    });
                });
            }
        });

        $scope.title = 'reserveCtrl';
        $scope.dataUrl = "/api/v2/photostudio/" + $routeParams.id;
        $scope.shortcut = $routeParams.id;
        $scope.isAdmin = true;

        $http.get($scope.dataUrl).then(function (response) {
            $scope.item = response.data;
            $scope.calendars = [];
            angular.forEach($scope.item.halls, function (item) {
                $scope.calendars.push(item.calendarID);
            });
        });

        activate();

        function activate() { }
    }
})();