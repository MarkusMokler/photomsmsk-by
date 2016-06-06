angular.module("photo.bel.admin.directivies")
    .directive("adminPhotosPage", function () {
        return {
            restrict: "EA",
            templateUrl: "/Scripts/admin/directivies/photos/index.html"
        };
    });