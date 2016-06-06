angular.module("photo.bel.admin.directivies")
    .directive('adminPhotoLoadPage', function () {
        return {
            controller: ['$scope', '$http', 'Upload', '$timeout', function ($scope, $http, Upload, $timeout) {
                $scope.loadPhoto = function () {
                    $scope.photos = [];
                    $.each($scope.uploadedImages, function () {
                        $scope.photos.push({
                            FileName: this[0].fileName,
                            Url: this[0].url,
                            Title: " ",
                            Description: null
                        });
                    });
                    $http({
                        method: 'PUT',
                        url: $scope.photosURL,
                        data: $scope.photos
                    })
                    .success(function (data) {
                            $scope.savedPhotos = data.photos;
                        });
                };
                $scope.log = '';
                var url = "http://ph1.photomsk.by/" + "Api/Files";
                    $scope.uploadedImages = [];
                $scope.photosUpload = function (files) {
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
               
                $scope.$watch('files', function () {
                    if (typeof ($scope.files) != "undefined") {
                        $scope.upload($scope.files);
                    }
                });
                $scope.$watch('file', function () {
                    if ($scope.file != null) {
                        $scope.files = [$scope.file];
                    }
                });
        }
        ],
            restrict: 'EA',
            templateUrl: '/Scripts/admin/directivies/photoload/index.html'
        };
    });