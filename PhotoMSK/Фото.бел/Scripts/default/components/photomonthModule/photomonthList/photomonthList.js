(function () {
    "use strict";
    angular
        .module("photo.bel.photomonth")
        .controller("photomonthListCtrl", photomonthListCtrl);

    photomonthListCtrl.$inject = ["$scope", "$http", "$location", "$routeParams", "$rootScope", "$timeout", "@authService", "Upload"];

    function photomonthListCtrl($scope, $http, $location, $routeParams, $rootScope, $timeout, authService, Upload) {
        $scope.title = "photomonthListCtrl";
        $scope.uploadedImages = [];
        var url = "http://ph1.photomsk.by/" + "Api/Files";
        var vm = this;
        vm.pagingParams = {};
        var userId = "d6fa4a98-1927-4da9-8bb7-36ff5eecde41";

        // setScope
        function setScope($scope, data) {
            vm.pagingParams = data;
            $scope.page = data;
        }

        // Call basic GET
        $http.get("/api/v2/compare").then(function (response) {
            if (response.data.length !== 0) {
                $scope.showVote = true;
                setScope($scope, response);
            }
        });

        // Click and vote
        $scope.vote = function (item) {
            // +1 Point to winner
            $http.put("/api/v2/winner/" + item.photo.id, item.photo);

            $scope.photosid = {
                id1: $scope.page.data[0].photo,
                id2: $scope.page.data[1].photo
            };

            var photo1Id = $scope.page.data[0].photo.id;
            var photo2Id = $scope.page.data[1].photo.id;

            // +1 CompareCount to both
            $http.put("/api/v2/compare/" + photo1Id, $scope.page.data[0]);
            $http.put("/api/v2/compare/" + photo2Id, $scope.page.data[1]);

            // Remember the pair for the user
            $http.post("/api/v2/compare", [photo1Id, photo2Id]).success(function () {
                // Generate a new pair
                $http.get("/api/v2/compare").then(function (response) {
                    setScope($scope, response);
                });
            });
        };

        // Выделение фотографии при наведении мыши
        $scope.select = function ($event) {
            angular.element($event.target).addClass("active");
        };
        $scope.unselect = function ($event) {
            angular.element($event.target).removeClass("active");
        };

        $scope.checkFile = function () {
            console.log($scope.fileUpload);
        };

        // GET photographers
        $http.get("/api/v2/photographers?id=" + userId).then(function (response) {
            $scope.photographers = response.data;
        });

        // setPhotographer
        $scope.setPhotographer = function (item) {
            $scope.shortcut = item.shortcut;
        }

        // Upload the photo
        $scope.photoUpload = function (file) {
            file.upload = Upload.upload({
                url: url,
                data: {
                    username: $scope.username,
                    file: file
                }
            });

            file.upload.then(function (response) {
                $timeout(function () {
                    file.result = response.data;
                    $scope.shortcut = "luntsevich"; // Test
                    file.result[0].shortcut = $scope.shortcut;
                    $http.post("/api/v2/nominationphoto", response.data);
                    $scope.status = response.status;
                    $scope.data = response.data;
                });
            }, function (response) {
                if (response.status > 0) {
                    $scope.errorMsg = response.status + ": " + response.data;
                }
            });
        };
    }

    activate();
    function activate() { }
})();
