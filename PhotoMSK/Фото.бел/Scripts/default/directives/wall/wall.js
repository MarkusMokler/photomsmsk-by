angular.module("photo.bel.directives")
    .directive('wallDirective', function () {
        return {
            controller: ['$scope','$http', '$routeParams', 'Upload', '$timeout', function($scope, $http, $routeParams, Upload, $timeout) {
                $scope.wallItem = null;
                $http.get('api/Services/Account/GetUserInformation').success(function (data) {
                    if ("userInformation" in data) {
                        $scope.ownerID = data.userInformation.id;
                    }
                });
                    $scope.addPost = function() {
                        if ($scope.wallItem != null) {
                            $scope.wallItem.attachments = [];
                            $scope.wallItem.routes = [];
                            $scope.wallItem.Owner = $scope.ownerID;
                            $scope.wallItem.routes.push($scope.item.id);
                            if ($scope.uploadedImages != undefined && $scope.uploadedImages.length > 0) {
                                $.each($scope.uploadedImages, function() {
                                    $scope.wallItem.attachments.push({
                                        FileName: this[0].fileName,
                                        Url: this[0].url,
                                        Title: " ",
                                        Description: null
                                    });
                                });
                            }

                            $http({
                                    method: 'POST',
                                    url: 'api/v2/wall/',
                                    data: $scope.wallItem
                                })
                                .success(function(data) {
                                    swal({
                                        title: 'Успех',
                                        text: "Новый пост добавлен",
                                        type: "success",
                                        timer: 2000,
                                        showConfirmButton: data.code & 1
                                    });
                                }).error(function(data) {
                                    swal({
                                        title: 'Ошибка',
                                        text: "Пост не добавлен",
                                        type: "error",
                                        timer: 2000,
                                        showConfirmButton: data.code & 1
                                    });
                                });
                            $scope.wallItem = null;
                        };
                    }
                    $scope.log = '';
                        var url = "http://ph1.photomsk.by/" + "Api/Files";
                        $scope.uploadedImages = [];
                        $scope.upload = function(files) {
                            if (files && files.length) {
                                for (var i = 0; i < files.length; i++) {
                                    var file = files[i];
                                    if (!file.$error) {
                                        Upload.upload({
                                            url: url,
                                            data: {
                                                username: $scope.username,
                                                file: file
                                            }
                                        }).then(function(resp) {
                                            $scope.uploadedImages.push(resp.data);
                                            $timeout(function() {
                                                $scope.log = 'file: ' +
                                                    resp.config.data.file.name +
                                                    ', Response: ' + JSON.stringify(resp.data) +
                                                    '\n' + $scope.log;
                                            });
                                        }, null, function(evt) {
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

                        $scope.$watch('files', function() {
                            $scope.upload($scope.files);
                        });
                        $scope.$watch('file', function() {
                            if ($scope.file != null) {
                                $scope.files = [$scope.file];
                            }
                        });
                }
            ],
            restrict: 'EA',
            templateUrl: '/Scripts/default/directives/wall/index.html'
        };
    });
