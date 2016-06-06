angular.module("photo.bel.admin.directivies")
    .directive("adminCreatePage", function () {
        return {
            controller: ["$scope", "$location", function ($scope, $location) {
                $scope.add = function () {
                    $location.path("/admin");
                };
            }],
            restrict: "EA",
            templateUrl: "/Scripts/admin/directivies/createpage/index.html"
        };
    });