(function () {
    'use strict';
    angular
		.module('photo.bel.admin.userinformation', [])
        .controller('userInformationAdminCtrl', userInformationAdminCtrl);

    userInformationAdminCtrl.$inject = ['$scope', '$routeParams', '$http', 'Upload', '$timeout'];

    function userInformationAdminCtrl($scope, $routeParams, $http, Upload, $timeout) {
        $scope.title = 'userInformationCtrl';
        $scope.active = 'userInfo';
        $scope.userInfoState = 'view';
        $scope.userInformation = {};
        $scope.card = {};
        $scope.dateOfBirth = {};
        function historyRent() {
            $http.get('/api/v2/userinformation/' + $routeParams.id + "/Events").then(function (data) {
                $scope.events = data.data;
            });
        };

        function penalties() {
            $http.get('/api/v2/userinformation/' + $routeParams.id + "/Penalties").then(function (data) {
                $scope.penalties = data.data;
            });
        };

        function cards() {
            $http.get('/api/v2/userinformation/' + $routeParams.id + "/Cards").then(function (data) {
                $scope.cards = data.data;
            });
        };

        var url = "http://ph1.photomsk.by/" + "Api/Files";

        $scope.$watch('active', function () {
            switch ($scope.active) {
                case 'HistoryRent':
                    historyRent();
                    break;
                case 'Penalties':
                    penalties();
                    break;
                case 'Cards':
                    cards();
                    break;
            }

        });
        $scope.deletePhone = function (value) {
            value.ID = $routeParams.id;
            $http({
                    method: 'DELETE',
                    url: '/api/v2/UserPhone/',
                    data: value,
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
                    $scope.phoneIsValidated = true;
                });
        };
        $scope.edit = function() {
            $scope.userInfoState = 'edit';
            $scope.userProfilesEdit = false;
            $scope.userPersonalInformationEdit = false;
            $scope.userBirthdayEdit = false;
            $scope.userAccommodationEdit = false;
            $scope.userExternalLinkEdit = false;
        };

        $scope.saveUserInfo = function () {
            $scope.userInformation.dateOfBirth = $scope.dateOfBirth.year + "-" + $scope.dateOfBirth.month + "-" + $scope.dateOfBirth.day;
            $http.put('/api/v2/userinformation/' + $scope.userInformation.id, $scope.userInformation).then(function(data) {
                    $scope.userProfilesEdit = false;
                    $scope.userPersonalInformationEdit = false;
                    $scope.userExternalLinkEdit = false;
                    swal({
                        title: 'Отлично',
                        text: 'Данные успешно сохранены',
                        type: "success",
                        timer: 2000,
                        showConfirmButton: false
                    });
                },
                function(data) {
                    swal({
                        title: 'Произошла ошибка',
                        text: 'попробуйте заного или обратитесть к администрации',
                        type: "error",
                        timer: 2000,
                        showConfirmButton: false
                    });
                });
        };

        $scope.newCard = function() {
            $scope.showCreateCard = true;
        };

        $scope.saveCard = function() {
            $http.post('/api/v2/userinformation/' + $routeParams.id + "/Cards", $scope.card).then(function(data) {
                $scope.cards = data.data;
            });

        };

        $scope.upload = function (dataUrl) {
            Upload.upload({
                url: url,
                data: {
                    file: Upload.dataUrltoBlob(dataUrl)
                }
            }).then(function (response) {
                $scope.userInformation.userPhoto = response.data[0].url;
            }, function (response) {
                if (response.status > 0) $scope.errorMsg = response.status
                    + ': ' + response.data;

                $scope.userInformation.userPhoto = response.data;
            }, function (evt) {
                $scope.progress = parseInt(100.0 * evt.loaded / evt.total);
            });
        };

        activate();

        $scope.dataEntityID = $routeParams.id;
        function activate() {
            $http.get('/api/v2/userinformation/' + $routeParams.id).then(function (data) {
                $scope.userInformation = data.data;
                var mom = moment($scope.userInformation.dateOfBirth, 'YYYY-MM-DD');
                $scope.dateOfBirth.year = mom.format("YYYY");
                $scope.dateOfBirth.month = mom.format("M");
                $scope.dateOfBirth.day = mom.format("D");
                console.log(data.data);
            });
            $http.get('/api/v2/userinformation/?userGuid=' + $routeParams.id).then(function (data) {
                $scope.userPhones = data.data;
            });
        }
    }
})();
