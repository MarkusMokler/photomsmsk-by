(function () {
    'use strict';
    angular
		.module('photo.bel.admin.photostudio')
        .controller('clientsCtrl', photostudioActivityCtrl);

    photostudioActivityCtrl.$inject = ['$scope', '$http', '$routeParams', '@authService'];

    function photostudioActivityCtrl($scope, $http, $routeParams, authService) {
        $scope.auth = authService;
        $scope.currentClient = null;
        $scope.currentPage = 1;
        $scope.review = {};
        $scope.review.title = "";
        $scope.review.description = "";
        $scope.review.badComment = "";
        $scope.review.goodComment = "";
        $scope.date = moment().subtract(3, 'month');
        $scope.status = "3 месяца";

        $scope.setCurrent = function (data) {
            $scope.currentClient = data;
        }

        $scope.edit = function (value) {
            value.copy = angular.copy(value);
            value.isEdit = true;
        }
        $scope.cancel = function (value) {
            $.each(value.copy, function (key, ovalue) {
                value[key] = ovalue;
            });
            value.isEdit = false;
            value.copy = null;

        }
        $scope.approive = function (value) {
            value.copy = null;
            value.isEdit = false;
            $http.post("/api/v2/route/" + $routeParams.id + "/clients", value).then(function (data) {
                swal({
                    title: 'Сохранено!',
                    text: 'Клиенту добавлены категории',
                    type: "success",
                    timer: 2000,
                    showConfirmButton: false
                });
            });
        }


        $scope.showReviewModal = false;
        function fetch() {
            if ($routeParams.tag == null)
                $http.get("/api/v2/route/" + $routeParams.id + "/clients?from=" + $scope.date.format('YYYY-MM-DD') + "&size=100&page=" + ($scope.currentPage - 1)).then(function (data) {
                    $scope.page = data.data;
                    $scope.clients = data.data.elements;
                });
            else {
                $http.get("/api/v2/route/" + $routeParams.id + "/clients?from=" + $scope.date.format('YYYY-MM-DD') + "&size=100&tag=" + $routeParams.tag + "&page=" + ($scope.currentPage - 1)).then(function (data) {
                    $scope.page = data.data;
                    $scope.clients = data.data.elements;
                });

            }
        }

        $scope.showReviewAdd = function (item) {
            $scope.userID = item.id;
            $scope.selectedUser = item;
            $scope.showReviewModal = true;
        };
        $scope.addReview = function () {
            $scope.review.routeShortcut = $routeParams.id;
            if ($scope.review.title == "" || $scope.review.description == "" || $scope.review.badComment == "" || $scope.review.goodComment == "") {
                swal({
                    title: 'Ошибка',
                    text: "Заполните поля",
                    type: "error",
                    timer: 2000,
                    showConfirmButton: false
                });
            } else {
                $http({
                    method: 'POST',
                    url: "/api/v2/Reviews/?userID=" + $scope.userID,
                    data: $scope.review
                })
                    .success(function (data) {
                        swal({
                            title: 'Успех',
                            text: '',
                            type: "success",
                            timer: 2000,
                            showConfirmButton: false
                        });
                    }).error(function (data) {
                        swal({
                            title: 'Ошибка',
                            text: data.message,
                            type: "error",
                            timer: 2000,
                            showConfirmButton: false
                        });
                    });
                $scope.showReviewModal = false;
            }
        };
        $scope.load = function (data) {
            $scope.date = moment().subtract(data, 'month');
            switch (data) {
                case 3:
                    $scope.status = "3 месяца";
                    break;
                case 6:
                    $scope.status = "полгода";
                    break;
                case 12:
                    $scope.status = "год";
                    break;
            }
            fetch();
        }


        $scope.$watch('currentPage', function () {
            fetch();
        });
        fetch();
        $scope.isAdmin = true;
        activate();

        function activate() { }
    }
})();
