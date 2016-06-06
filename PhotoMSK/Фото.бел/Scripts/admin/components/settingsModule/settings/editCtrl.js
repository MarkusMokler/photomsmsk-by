(function () {
    'use strict';
    angular
		.module('photo.bel.admin.settings')
        .controller('settingsCtrl', photostudioEditCtrl);

    photostudioEditCtrl.$inject = ['$scope', '$http', '$routeParams', '$location'];

    function photostudioEditCtrl($scope, $http, $routeParams, $location) {
        $scope.title = $routeParams.id;
        $scope.studioShortcut = $routeParams.id;
        $scope.findByphone = function () {
            $http.get('/api/v2/route/' + $scope.studioShortcut + '/Participant?page=0&size=10&search=' + $scope.newAdminPhone).then(function (data) {
                $scope.newParticipants = data.data.elements;
            });
        }
        $scope.loadParticipants = function () {
            $http.get('/api/v2/route/' + $scope.studioShortcut + '/Participant').then(function (data) {
                $scope.participants = data.data.elements;
            });
        }

        $scope.selectNewAdmin = function (admin) {
            $.each($scope.participants, function () {
                this.isActive = false;
            });
            admin.isActive = !admin.isActive;
            if (admin.isActive)
                $scope.selectedAdmin = admin;
            else {
                $scope.selectedAdmin = null;
            }
        }
        $scope.saveNewAdmin = function () {
            $scope.selectedAdmin.AccessStatus = 2;

            $http.post('/api/v2/route/' + $scope.studioShortcut + '/Participant', $scope.selectedAdmin).then(
                function (data) {
                    swal({
                        title: 'Выполнено!',
                        text: 'Новый админ добавлен',
                        type: "success",
                        timer: 2000,
                        showConfirmButton: false
                    });
                    $scope.showModal();
                    $scope.loadParticipants();
                }, function (error) {
                    swal({
                        title: "Ошибка",
                        text: error.data.exceptionMessage,
                        type: "error",
                        showConfirmButton: data.code & 1
                    });
                });
        }

        $scope.removeAdmin = function (id) {
            $http.delete('/api/v2/route/' + $scope.studioShortcut + '/Participant/' + id).then(function (data) {
                swal({
                    title: 'Выполнено!',
                    text: 'Новый админ добавлен',
                    type: "success",
                    timer: 2000,
                    showConfirmButton: false
                });
                $scope.loadParticipants();
            })


        }

        $scope.setTab = function (val) {
            $scope.tab = val;
            if (val === "tab6") {
                $http.get("/api/v2/route/" + $scope.studioShortcut + "/Setting").then(function (response) {
                    $scope.settings = response.data;
                });
            }
        }
        $scope.item = {};
        $scope.item.name = "";
        $scope.item.phones = {};
        $scope.phone = {};
        $scope.phone.number = "";
        $scope.phone.City = "";
        $scope.phone.Adress = "";

        $scope.delRoute = function() {
            $http.delete('/api/v2/photostudio/?id=' + $scope.item.id);
        }

        $http.get('/api/v2/photostudio/' + $routeParams.id).then(function (response) {
            $scope.item = response.data;
            if ($scope.item.phones.length > 0) {
                $scope.phone.number = $scope.item.phones[0].number;
            };
            $scope.phone.City = $scope.item.city;
            $scope.phone.Adress = $scope.item.adress;
            $scope.loadParticipants();
        });
        $scope.photosURL = '/api/v2/routephotos/?shortcut=' + $routeParams.id;
        $scope.updateCharacteristics = function () {
            $scope.setTab('tab5');
        };
        $scope.phonePage = function () {
                if ($scope.item.phones.length != 0 && $scope.phone.number == $scope.item.phones[0].number) {
                    $scope.setTab('tab4');
                } else {
                    $scope.validatePhone();
                }
        };
        $scope.updateName = function () {
            $http({
                method: 'PUT',
                url: '/api/v2/photostudio/?id=' + $scope.item.id,
                data: $scope.item
            })
	            .success(function (data) {
	                $scope.setTab('tab2');
	                $scope.message = data.message;
	            });
        };
        $scope.validatePhone = function () {
            $scope.phone.EntityId = $scope.item.id;

            $scope.showConfirm = true;
            $http({
                method: 'POST',
                url: '/api/v2/route/' + $scope.studioShortcut + '/phonecheck',
                data: $scope.phone,
                headers: { 'Content-Type': 'application/json' }
            })
	            .success(function (data) {
	                $scope.message = data.message;
	            });
        }
        $scope.confirm = function () {
            $scope.phone.ConfirmCode = $scope.Code;
            $http({
                method: 'PUT',
                url: '/api/v2/route/' + $scope.studioShortcut + '/phonecheck',
                data: $scope.phone,
                headers: { 'Content-Type': 'application/json' }
            })
	            .success(function (data) {
	                swal({
	                    title: 'Успех!',
	                    text: data.message,
	                    type: "success",
	                    timer: 2000,
	                    showConfirmButton: false
	                })
	            })
	            .error(function (data) {
	                swal({
	                    title: 'Ошибка!',
	                    text: data.message,
	                    type: "error",
	                    timer: 2000,
	                    showConfirmButton: false
	                })
	            });
        };

        $scope.add = function () {
            $location.path('/admin');
        };
        $scope.setTab('tab1');
        activate();

        function activate() { }
    }
})();