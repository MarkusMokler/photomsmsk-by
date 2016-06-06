angular.module("photo.bel.admin.directivies")
    .directive('adminLogoPage', function () {
        return {
            controller: ['$scope', function ($scope) {

                var url = "http://ph1.photomsk.by/" + "Api/Cropper";
                var curl = "http://ph1.photomsk.by/" + "Api/Cropp";

                var cropCover = new Croppic('role-cover', {
                    customUploadButtonId: "role-change-cover",
                    cropUrl: curl,
                    uploadUrl: url,
                    onAfterImgCrop: function (data) {
                        model.set("CoverImage", data.url);
                    },
                    imgEyecandy: true,
                    imgEyecandyOpacity: 0.2,
                    cropData: {
                        "zoomFactor": 2,
                    }

                });

                var cropAvatar = new Croppic('role-avatar', {
                    customUploadButtonId: "role-change-avatar",
                    cropUrl: curl,
                    modal: true,
                    uploadUrl: url,
                    width: 400,
                    height: 400,
                    onAfterImgCrop: function(data)  {
                        model.set("TeaserImage", data.url);
                    }

                });


            }],
            restrict: 'EA',
            templateUrl: '/Scripts/admin/directivies/logoload/index.html'
        };
    });