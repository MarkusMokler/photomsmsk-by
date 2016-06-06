angular.module("photo.bel.admin.directivies")
    .directive("leftMenu", function () {
        return {
            controller: ["$scope", "$http", "$location", "$routeParams", "$rootScope", function ($scope, $http, $location, $routeParams, $rootScope) {
                $scope.getDataLeftMenu = function () {
                    $scope.showLeftMenuClick = true;
                    $scope.url = $location.absUrl();
                    if ($scope.url.indexOf("admin") > -1) {
                        $scope.showLeftMenu = true;
                        $scope.showTopMenu = true;
                        if ($scope.routeLast !== $routeParams.id || typeof ($routeParams.id) == "undefined") {
                            $scope.routeLast = $routeParams.id;
                            $http.get("/api/v2/home").then(function (data) {
                                $scope.routes = data.data;
                                if ($scope.routes.length === 0) {
                                    $location.url("/admin/route/add");
                                }

                                if (typeof ($routeParams.id) == "undefined" && $scope.url.indexOf("route/add") === -1 && $scope.url.indexOf("admin/photostudio/") === -1
                                    && $scope.url.indexOf("admin/photorent/") === -1 && $scope.url.indexOf("admin/photoshop/") === -1
                                    && $scope.url.indexOf("admin/photographer/") === -1 && $scope.url.indexOf("hall") === -1) {
                                    $scope.setRoute($scope.routes[0]);
                                } else {
                                    if (typeof ($routeParams.id) == "undefined" && $scope.url.indexOf("hall") === -1) {
                                        $scope.selectedRouteLeftMenu = $scope.routes[0];
                                    }
                                    $.each($scope.routes, function () {
                                        if (this.shortcut === $routeParams.id) {
                                            $scope.setRoute(this);
                                        }
                                    });
                                }
                            });
                        }
                    } else {
                        $scope.showLeftMenu = false;
                        $scope.showTopMenu = false;
                    }
                }
                $scope.getDataLeftMenu();
                $scope.iconsOnly = false;
                $scope.changeViewMode = function () {
                    $scope.viewMode = !$scope.viewMode;
                    $scope.iconsOnly = !$scope.iconsOnly;
                }

                $rootScope.$on("$locationChangeSuccess", function (event, newUrl) {
                    if (newUrl.indexOf("admin") > -1) {
                        $scope.showLeftMenu = true;
                        $scope.showTopMenu = true;
                        $scope.getDataLeftMenu();
                    } else {
                        $scope.showLeftMenu = false;
                        $scope.showTopMenu = false;
                    }
                    if (newUrl.indexOf("client") > -1) {
                        $http.get("/api/v2/route/" + $routeParams.id + "/clients?ClientsCategoties=true").then(function (data) {
                            $scope.clientType = data.data;
                        });
                    }
                });

                $scope.setRoute = function (item) {
                    console.log(item);
                    if (typeof ($routeParams.id) == "undefined" || item.shortcut !== $routeParams.id) {
                        if (item.routeType === "Photostudio") {
                            $location.url("admin/photostudio/" + item.shortcut);
                        }
                        if (item.routeType === "Photorent") {
                            $location.url("admin/photorent/" + item.shortcut + "/booking");
                        }
                        if (item.routeType === "Photoshop") {
                            $location.url("admin/photoshop/" + item.shortcut);
                        }
                        if (item.routeType === "Photographer") {
                            $location.url("admin/photographer/" + item.shortcut);
                        }

                    }
                    if (item.routeType === "Photostudio") {
                        $http.get("/api/v2/photostudio/" + item.shortcut).then(function (response) {
                            $scope.selectedRouteLeftMenu = response.data;
                        });
                    }
                    if (item.routeType === "Photorent") {
                        $http.get("/api/v2/photorent/?shortcut=" + item.shortcut).then(function (response) {
                            $scope.selectedRouteLeftMenu = response.data;
                        });
                    }
                    if (item.routeType === "Photoshop") {
                        $http.get("/api/v2/photoshop/?shortcut=" + item.shortcut).then(function (response) {
                            $scope.selectedRouteLeftMenu = response.data;
                        });
                    }
                    if (item.routeType === "Photographer") {
                        $http.get("api/v2/photographer/?shortcut=" + item.shortcut).then(function (response) {
                            $scope.selectedRouteLeftMenu = response.data;
                        });
                        // GET projects list
                        $http.get("api/v2/project/?shortcut=" + item.shortcut).then(function (response) {
                            $scope.projects = response.data;
                            });
                    }
                };
            }],
            restrict: "EA",
            templateUrl: "/Scripts/admin/components/shared/_LeftAdminMenu.html"
        };
    });
