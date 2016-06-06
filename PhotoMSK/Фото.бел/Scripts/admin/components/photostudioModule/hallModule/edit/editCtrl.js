(function () {
    "use strict";
    angular
		.module("photo.bel.admin.hall")
        .controller("hallEditAdminCtrl", hallEditAdminCtrl);

    hallEditAdminCtrl.$inject = ["$scope", "$http", "$routeParams", "$location", 'Upload'];

    function hallEditAdminCtrl($scope, $http, $routeParams, $location, Upload) {
        $scope.title = "hallEditAdminCtrl";
        $scope.item = {};
        $scope.croppedDataUrl = "";
        $scope.routeShortcut = $routeParams.id;
        $scope.hallPropertiesAutoURL = "api/v2/HallProperties/?search=";
        $scope.dayOfWeekStr = ["Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье"];
        $scope.studioShortcut = $routeParams.id;

        $http.get("api/v2/Photostudio/ " + $routeParams.id + "/Hall?hallId=" + $routeParams.hallId).then(function (response) {
            $scope.item = response.data;
            $scope.savedPhotos = $scope.item.photos;
        });

        $scope.update = function () {
            $http.get("api/v2/HallProperties/?hallId=" + $routeParams.hallId).then(function (response) {
                $scope.itemProperties = response.data.incl;
                $scope.exProperties = response.data.ex;
            });
        }

        $scope.saveLanding = function () {

            if (!$scope.item.landingPageID)
                $http.post("api/v2/Landing/", { widgets: $scope.widgets }).then(function (response) {
                    $scope.item.landingPageID = response.data.id;
                    $scope.updateHall();
                });
            else
                $http.put("api/v2/Landing/" + $scope.item.landingPageID, { widgets: $scope.widgets }).then(function () {
                });
        }
        $scope.photosURL = "/api/v2/hallphotos/?id=" + $routeParams.hallId;

        var originals = [
            {type: "gallery",name: "name",files: []},
            {type: "description"},
            {type: "splitWidget",widgets: []},
            {type: "containerWidget",widgets: []},
            {type: "faqWidget"},
            {type: "descriptionWidget"},
            {type: "headerWidget"}];

        $scope.baseWidgets = originals.slice();

        activate();
        $scope.tab = "tab1";
        $scope.tabSet = function (tab) {
            $scope.tab = tab;
            if (tab === "tab4") {
                if ($scope.item.landingPageID != null)
                    $http.get("api/v2/Landing/" + $scope.item.landingPageID).then(function (response) {
                        $scope.widgets = response.data.widgets;
                    });
            }
        };
        $scope.updateData = function () {
            $location.path("/admin");
        };
        $scope.addProperty = function (id) {
            if (id == null) {
                console.log(id);
                if ($scope.selected == undefined || $scope.selected == null) {
                    $http({
                        method: "POST",
                        url: "/api/v2/HallProperties/",
                        data: {
                            HallID: $routeParams.hallId,
                            Property: $scope.property
                        },
                        headers: { 'Content-Type': "application/json" }
                    })
                        .success(function (dadta) {

                            $scope.update();
                            swal({
                                title: "Успех",
                                text: "",
                                type: "success",
                                timer: 2000,
                                showConfirmButton: false
                            });
                        });
                } else {
                    $http({
                        method: "PUT",
                        url: "/api/v2/HallProperties/",
                        data: {
                            HallID: $routeParams.hallId,
                            HallPropertyID: $scope.selected.id
                        },
                        headers: { 'Content-Type': "application/json" }
                    })
                        .success(function (dadta) {

                            $scope.update();
                            swal({
                                title: "Успех",
                                text: "",
                                type: "success",
                                timer: 2000,
                                showConfirmButton: false
                            });
                        });
                }
            } else {
                $http({
                    method: "PUT",
                    url: "/api/v2/HallProperties/",
                    data: {
                        HallID: $routeParams.hallId,
                        HallPropertyID: id
                    },
                    headers: { 'Content-Type': "application/json" }
                })
                        .success(function (dadta) {

                            $scope.update();
                            swal({
                                title: "Успех",
                                text: "",
                                type: "success",
                                timer: 2000,
                                showConfirmButton: false
                            });
                        });
            }
        };
        $scope.deleteProperty = function (id) {
            $http({
                method: "DELETE",
                url: "/api/v2/HallProperties/?hallID=" + $routeParams.hallId + "&HallPropertyID=" + id,
                headers: { 'Content-Type': "application/json" }
            })
                    .success(function (dadta) {
                        $scope.update();
                    });
        }
        $scope.updateHall = function (croppedDataUrl) {
            if (croppedDataUrl != "" && typeof(croppedDataUrl) != "undefined" && croppedDataUrl!=null) {
                var url = "http://ph1.photomsk.by/" + "Api/Files";
                $scope.item.teaserImage = "";
                console.log(croppedDataUrl);
                Upload.upload({
                    url: url,
                    data: {
                        file: Upload.dataUrltoBlob(croppedDataUrl)
                    },
                }).then(function(response) {
                    $scope.item.teaserImage = response.data[0].url;
                    $scope.item.teaserImageUrl = response.data[0].url;
                    $http({
                        method: "PUT",
                        url: "/api/v2/photostudio/" + $routeParams.id + "/hall/" + $routeParams.hallId,
                        data: $scope.item,
                        headers: { 'Content-Type': "application/json" }
                    });
                }, function(response) {
                    if (response.status > 0)
                        $scope.errorMsg = response.status
                            + ': ' + response.data;
                    $scope.item.teaserImage = response.data;
                    $scope.item.teaserImageUrl = response.data;
                }, function(evt) {
                    $scope.progress = parseInt(100.0 * evt.loaded / evt.total);
                });
            } else {
                $http({
                    method: "PUT",
                    url: "/api/v2/photostudio/" + $routeParams.id + "/hall/" + $routeParams.hallId,
                    data: $scope.item,
                    headers: { 'Content-Type': "application/json" }
                });
            }
            
        };

        $scope.saveSchedulle = function (data) {
            $http({
                method: "PUT",
                url: "/api/v2/hall/" + $scope.item.id + "/scheduleDay/" + data.id,
                data: data,
                headers: { 'Content-Type': "application/json" }
            })
             .success(function (dadta) {
                 data.isApproive = false;
             });
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
            value.isApproive = true;
        }

        var i = 0;
        $scope.widgets = [];
        $scope.removeWidgets = function (item) {
            var iof = $scope.widgets.indexOf(item);
            $scope.widgets.splice(iof, 1);
        }
        $scope.setedit = function (widget) {
            $.each($scope.widgets, function () {
                this.isEdit = false;
            });
            widget.isEdit = true;
        }
        $scope.currentPage = 1;
        $scope.state = {};
        $scope.state.currentFolderID = null;

        $scope.widgetsOptions = {
            connectWith: ".widgets-container",
            stop: function (e, ui) {
                if ($(e.target).hasClass("first") &&
                    ui.item.sortable.droptarget &&
                    e.target != ui.item.sortable.droptarget[0]) {

                    $scope.baseWidgets = [];
                    $.each(originals, function () {
                        $scope.baseWidgets.push($.extend(true, {}, this));
                    });
                }
            }
        };
        $scope.sortableOptions = {
            placeholder: "app",
            connectWith: ".apps-container"
        };

       function activate() { }
    }
})();