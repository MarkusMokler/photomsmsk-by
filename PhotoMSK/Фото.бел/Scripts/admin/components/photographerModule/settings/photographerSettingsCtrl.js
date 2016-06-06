(function () {
    "use strict";
    angular
		.module("photo.bel.admin.photographer")
        .controller("photographerSettingsCtrl", photographerSettingsCtrl);

    photographerSettingsCtrl.$inject = ["$scope", "$http", "$routeParams", "$location"];

    function photographerSettingsCtrl($scope, $http, $routeParams, $location) {
        $scope.title = "photographerSettingsCtrl";
        //$scope.title = $routeParams.id;
        $scope.photographerShortcut = $routeParams.id;
        $scope.routeTypeSelected = "Photographer";
        $scope.setTab = function (val) {
            $scope.tab = val;
        }

        $scope.setTab("tab1");
        $scope.item = {};
        $http.get("/api/v2/photographer/?shortcut=" + $routeParams.id).success(function (response) {
            $scope.item = response;
            console.log($scope.item);
            console.log("First name:" + $scope.item.firstName);
            console.log("Last name:" + $scope.item.lastName);
        });
        $scope.addName = function () {
            $http({
                method: "PUT",
                url: "/api/v2/photographer/?shortcut=" + $routeParams.id,
                data: $scope.item,
                headers: { 'Content-Type': "application/json" }
            })
	            .success(function (data) {
	                $scope.dataEntityID = data.id;
	            });
            $scope.lockTab = true;
            if ($scope.lockTab) {
                $scope.setTab("tab2");
                $scope.lockTab = false;
            }
        };
        $scope.loadPhoto = function () {
            $scope.addName();
            $scope.setTab("tab4");
        }
        $scope.updateData = function () {
            $scope.setTab("tab3");
        }
        $scope.add = function () {
            $location.path("/admin");
        };

        $scope.findByphone = function () {
            $http.get("/api/v2/route/" + $scope.photographerShortcut + "/Participant?page=0&size=10&search=" + $scope.newAdminPhone).then(function (data) {
                $scope.newParticipants = data.data.elements;
            });
        }
        $scope.loadParticipants = function () {
            $http.get("/api/v2/route/" + $scope.photographerShortcut + "/Participant").then(function (data) {
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

            $http.post("/api/v2/route/" + $scope.photographerShortcut + "/Participant", $scope.selectedAdmin).then(
                function (data) {
                    swal({
                        title: "Выполнено!",
                        text: "Новый админ добавлен",
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
            $http.delete("/api/v2/route/" + $scope.photographerShortcut + "/Participant/" + id).then(function (data) {
                swal({
                    title: "Выполнено!",
                    text: "Новый админ добавлен",
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
                $http.get("/api/v2/route/" + $scope.photographerShortcut + "/Setting").then(function (response) {
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

        $scope.delRoute = function () {
            $http.delete("/api/v2/photographer/?id=" + $scope.item.id).success(
                $location.url("admin/")
            );
        }

        $http.get("/api/v2/photographer/" + $routeParams.id).then(function (response) {
            $scope.item = response.data;
            if ($scope.item.phones.length > 0) {
                $scope.phone.number = $scope.item.phones[0].number;
            };
            $scope.phone.City = $scope.item.city;
            $scope.phone.Adress = $scope.item.adress;
            $scope.loadParticipants();
        });
        $scope.photosURL = "/api/v2/routephotos/?shortcut=" + $routeParams.id;
        $scope.updateCharacteristics = function () {
            $scope.setTab("tab5");
        };
        $scope.phonePage = function () {
            if ($scope.item.phones.length != 0 && $scope.phone.number == $scope.item.phones[0].number) {
                $scope.setTab("tab4");
            } else {
                $scope.validatePhone();
            }
        };
        $scope.updateName = function () {
            $http({
                method: "PUT",
                url: "/api/v2/photographer/?id=" + $scope.item.id,
                data: $scope.item
            })
                .success(function (data) {
                    $scope.setTab("tab2");
                    $scope.message = data.message;
                });
        };
        $scope.validatePhone = function () {
            $scope.phone.EntityId = $scope.item.id;

            $scope.showConfirm = true;
            $http({
                method: "POST",
                url: "/api/v2/route/" + $scope.photographerShortcut + "/phonecheck",
                data: $scope.phone,
                headers: { 'Content-Type': "application/json" }
            })
                .success(function (data) {
                    $scope.message = data.message;
                });
        }
        $scope.confirm = function () {
            $scope.phone.ConfirmCode = $scope.Code;
            $http({
                method: "PUT",
                url: "/api/v2/route/" + $scope.photographerShortcut + "/phonecheck",
                data: $scope.phone,
                headers: { 'Content-Type': "application/json" }
            })
                .success(function (data) {
                    swal({
                        title: "Успех!",
                        text: data.message,
                        type: "success",
                        timer: 2000,
                        showConfirmButton: false
                    })
                })
                .error(function (data) {
                    swal({
                        title: "Ошибка!",
                        text: data.message,
                        type: "error",
                        timer: 2000,
                        showConfirmButton: false
                    })
                });
        };

        $scope.add = function () {
            $location.path("/admin");
        };
        $scope.setTab("tab1");

        //
        activate();

        function activate() { }
    }
})();