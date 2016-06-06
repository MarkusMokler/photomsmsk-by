(function () {
    'use strict';
    angular
		.module('photo.bel.admin.photorent')
        .controller('photorentParticipantsCtrl', photorentParticipantsCtrl);

    photorentParticipantsCtrl.$inject = ['$scope', '$http', '$routeParams', '$location'];

    function photorentParticipantsCtrl($scope, $http, $routeParams) {

        $scope.loadParticipants = function () {
            $http.get('/api/v2/route/' + $routeParams.id + '/Participant').then(function (data) {
                $scope.participants = data.data.elements;
                console.log($scope.participants);
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
        };
        $scope.findByphone = function () {
            $http.get('/api/v2/route/' + $routeParams.id + '/Participant?page=0&size=10&search=' + $scope.newAdminPhone).then(function (data) {
                $scope.newParticipants = data.data.elements;
            });
        }
        $scope.saveNewAdmin = function() {
            $scope.selectedAdmin.AccessStatus = 2;

            $http.post('/api/v2/route/' + $routeParams.id + '/Participant', $scope.selectedAdmin).then(function(data) {
                swal({
                    title: 'Успешно!',
                    text: 'Данные успешно сохранены',
                    type: "success",
                    timer: 2000,
                    showConfirmButton: false
                });
                $scope.loadParticipants();
            });
        };
        $scope.removeAdmin = function(id) {
            $http.delete('/api/v2/route/' + $routeParams.id + '/Participant/' + id).then(function(data) {
                swal({
                    title: 'Поздравляем!',
                    text: 'Данные успешно сохранены',
                    type: "success",
                    timer: 2000,
                    showConfirmButton: false
                })
                $scope.loadParticipants();
            });
        };
        $scope.loadParticipants();
        activate();

        function activate() { }
    }
})();
