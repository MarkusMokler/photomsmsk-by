angular.module("photo.bel.admin.directivies")
    .directive("adminStudioNamePage", function () {
        return {
            controller: ["$scope", "$http", "$location", function ($scope, $http, $location) {

                $scope.routesTypes = [
                {
                    type: "Photostudio",
                    name: "Фотостудия"
                },
                {
                    type: "Photorent",
                    name: "Аренда техники"
                },
                {
                    type: "Photoshop",
                    name: "Фотомагазин"
                },
                {
                    type: "Photographer",
                    name: "Фотограф"
                }];

                if ($scope.routeTypeSelected === "Photographer") {
                    $scope.showOthers = false;
                    $scope.showPhotographer = true;
                } else {
                    $scope.routeTypeSelected = "Photostudio";
                    $scope.showOthers = true;
                }

                $scope.setType = function (value) {
                    if (value === "Photographer") {
                        $scope.showOthers = false;
                        $scope.showPhotographer = true;
                    } else {
                        $scope.showPhotographer = false;
                        $scope.showOthers = true;
                    }
                    $scope.routeTypeSelected = value;
                }

                // Checker
                var isInvalid = function (value) {
                    return !value;
                }

                $scope.addNewRoute = function () {
                    console.log($scope.item);
                    if ($scope.routeTypeSelected === "Photostudio")
                        $scope.routeUrl = "/api/v2/photostudio";
                    if ($scope.routeTypeSelected === "Photoshop")
                        $scope.routeUrl = "/api/v2/photoshop";
                    if ($scope.routeTypeSelected === "Photorent")
                        $scope.routeUrl = "/api/v2/photorent";
                    if ($scope.routeTypeSelected === "Photographer") {
                        $scope.routeUrl = "/api/v2/photographer";

                        var i = $scope.item;
                        if (isInvalid(i.firstName) || isInvalid(i.lastName) ||
                            isInvalid(i.country) || isInvalid(i.city))
                            return void alert(
                                "Обязательные поля не заполнены.");
                    }

                    $http({
                        method: "POST",
                        url: $scope.routeUrl,
                        data: $scope.item,
                        headers: { 'Content-Type': "application/json" }
                    })
	                    .success(function (data) {
	                        swal({
	                            title: "Успешно",
	                            text: "",
	                            type: "success",
	                            timer: 2000,
	                            showConfirmButton: false
	                        });

	                        if ($scope.routeTypeSelected === "Photostudio")
	                            $location.url("/admin/photostudio/" + $scope.item.shortcut);
	                        if ($scope.routeTypeSelected === "Photoshop")
	                            $location.url("/api/v2/photoshop/" + $scope.item.shortcut);
	                        if ($scope.routeTypeSelected === "Photorent")
	                            $location.url("/admin/photorent/" + $scope.item.shortcut + "/booking");
	                        if ($scope.routeTypeselected === "Photographer")
	                            $location.url("/admin/photographer/" + $scope.item.shortcut);

	                    });
                }
            }],
            restrict: "EA",
            templateUrl: "/Scripts/admin/directivies/studioname/index.html"
        };
    });
