(function () {
    "use strict";
    angular
        .module("photo.bel.admin.media")
        .controller("mediaManagerCtrl", reviewsCtrl);
    
    reviewsCtrl.$inject = ["$scope", "$http", "$routeParams", "@authService", "Upload", "$timeout"];

    function reviewsCtrl($scope, $http, $routeParams, authService, Upload, $timeout) {
        $scope.routeShortcut = $routeParams.id;
        $scope.state = {};
        $scope.state.currentFolderID = null;
        $scope.loadPhoto = function () {
            $scope.photos = [];
            $.each($scope.uploadedImages, function () {
                $scope.photos.push({
                    FileName: this[0].fileName,
                    Url: this[0].url,
                    Title: " ",
                    type: "photo",
                    parentFolderid: $scope.state.currentFolderID
                });
            });
            $http({
                method: "POST",
                url: "/api/v2/route/" + $routeParams.id + "/files",
                data: $scope.photos
            })
            .success(function (data) {
                swal({
                    title: 'Успешно',
                    text: 'файлы загрузены на портал',
                    type: "success",
                    timer: 2000,
                    showConfirmButton: false
                });
            });
        };
        $scope.log = '';
        $scope.currentPage = 1;
        var url = "http://ph1.photomsk.by/" + "Api/Files";
        $scope.uploadedImages = [];
        $scope.photosUpload = function (files) {
            $scope.selectedFilesLength = files.length;
            if (files && files.length) {
                var j = 0;
                for (var i = 0; i < files.length; i++) {
                    var file = files[i];
                    if (!file.$error) {
                        Upload.upload({
                            url: url,
                            data: {
                                username: $scope.username,
                                file: file
                            }
                        }).then(function (resp) {
                            $scope.uploadedImages.push(resp.data);
                            $timeout(function () {
                                $scope.log = 'file: ' +
                                resp.config.data.file.name +
                                ', Response: ' + JSON.stringify(resp.data) +
                                '\n' + $scope.log;
                            });
                        }, null, function (evt) {
                            var progressPercentage = parseInt(100.0 *
                                    evt.loaded / evt.total);
                            $scope.log = 'progress: ' + progressPercentage +
                                '% ' + evt.config.data.file.name + '\n' +
                              $scope.log;
                        });
                    }
                }
            }
        };

        $scope.$watch('uploadedImages.length', function () {
            if ($scope.uploadedImages.length == $scope.selectedFilesLength) {
                $scope.loadPhoto();
            }
        });
        $scope.auth = authService;
        $scope.mediaModel = {}
        $scope.toggleSelected = function (item) {
            item.selected = !item.selected;
            $scope.mediaModel.ifSelected = item.selected;
            if (item.selected) {
                $scope.currentSelection = item;
            }
        }

        $scope.updateCurrent = function () {
            currentSelection.title = currentSelection.name;
            $http({
                method: "PUT",
                url: "/api/v2/route/" + $routeParams.id + "/files/" + currentSelection.id,
                data: currentSelection,
            });
        }

        $scope.removeItem = function (id) {
            $http({
                method: "DELETE",
                url: "/api/v2/route/" + $routeParams.id + "/files/" + id
            });
        }



        $scope.createNewFolder = function () {
            $scope.isCreateNewFolder = true;
        }

        $scope.cancelDialog = function () {
            $scope.isCreateNewFolder = false;

        }
        function loadFiles() {
            $http.get("/api/v2/route/" + $routeParams.id + "/files?page=" + ($scope.currentPage - 1) + "&directory=" + $scope.state.currentFolderID).then(function (data) {
                $scope.files = data.data;
            });
        }
        function loadFolders() {
            $http.get("/api/v2/route/" + $routeParams.id + "/files?foldersOnly=true").then(function (data) {
                $scope.folders = data.data;
            });
        }

        $scope.saveNewFolder = function () {
            $http({
                method: "POST",
                url: "/api/v2/route/" + $routeParams.id + "/files",
                data: [
                    {
                        FileName: $scope.newFolderName,
                        type: "folder",
                        parentFolderid: $scope.state.currentFolderID

                    }
                ]
            }).then(function () {
                $scope.isCreateNewFolder = false;
                loadFolders();
            });
        }



        loadFolders();


        $scope.$watch('currentPage', function () {
            loadFiles();
        });
        $scope.$watch('state.currentFolderID', function () {
            loadFiles();
        });

        loadFiles();
        $scope.isAdmin = true;
        activate();

        function activate() { }
    }

})();
