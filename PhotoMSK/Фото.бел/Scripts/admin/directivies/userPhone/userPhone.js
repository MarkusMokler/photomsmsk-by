angular.module("photo.bel.admin.directivies")
    .directive('adminUserPhone', function () {
        return {
            controller: ['$scope', '$http', '$routeParams', function ($scope, $http, $routeParams) {
                $scope.validatePhone = function () {
                    $scope.phoneAdd.ID = $routeParams.id;
                    if (!$scope.phoneIsValidated) {
                            $scope.showConfirm = true;
                            $http({
                                    method: 'POST',
                                    url: '/api/v2/UserPhone/',
                                    data: $scope.phoneAdd,
                                    headers: { 'Content-Type': 'application/json' }
                                })
                                .success(function(data) {
                                    if (data.error != 1) {
                                        swal({
                                            title: 'Успех',
                                            text: data.message,
                                            type: "success",
                                            timer: 2000,
                                            showConfirmButton: false
                                        });
                                    } else {
                                        swal({
                                            title: 'Ошибка',
                                            text: data.message,
                                            type: "error",
                                            timer: 2000,
                                            showConfirmButton: false
                                        });
                                    }
                                });
                    }
                }

                

                $scope.confirm = function () {
                    $scope.phoneAdd.ConfirmCode = $scope.Code;
                    $http({
                            method: 'PUT',
                            url: '/api/v2/UserPhone/',
                            data: $scope.phoneAdd,
                            headers: { 'Content-Type': 'application/json' }
                        })
                        .success(function (data) {
                            if (data.error != 1) {
                                swal({
                                    title: 'Успех',
                                    text: data.message,
                                    type: "success",
                                    timer: 2000,
                                    showConfirmButton: false
                                });
                            } else {
                                swal({
                                    title: 'Ошибка',
                                    text: data.message,
                                    type: "error",
                                    timer: 2000,
                                    showConfirmButton: false
                                });
                            }
                            $scope.phoneIsValidated = true;

                        });
                };
            }],
            restrict: 'EA',
            templateUrl: '/Scripts/admin/directivies/userPhone/index.html'
        };
    });