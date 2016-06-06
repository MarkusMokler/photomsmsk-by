angular.module("photo.bel.admin.directivies")
    .directive('adminHallLogoLoad', function () {
        return {
            controller: ['$scope', '$http', 'Upload', '$timeout', function ($scope, $http, Upload) {
                var url = "http://ph1.photomsk.by/" + "Api/Files";
                $scope.uploadLogo = function (dataUrl) {
                    $scope.item.teaserImage = "";
                    Upload.upload({
                        url: url,
                        data: {
                            file: Upload.dataUrltoBlob(dataUrl)
                        },
                    }).then(function (response) {
                        $scope.item.teaserImage = response.data[0].url;
                        $scope.item.teaserImageUrl = response.data[0].url;
                        $scope.updateHall();
                    }, function (response) {
                        if (response.status > 0) $scope.errorMsg = response.status
                            + ': ' + response.data;
                        $scope.item.teaserImage = response.data;
                        $scope.item.teaserImageUrl = response.data;
                    }, function (evt) {
                        $scope.progress = parseInt(100.0 * evt.loaded / evt.total);
                    });

                }
        }
        ],
            restrict: 'EA',
            templateUrl: '/Scripts/admin/directivies/halllogoload/index.html'
        };
    });