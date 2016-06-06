(function () {
    "use strict";
    angular
		.module("photo.bel.admin.settings")
        .controller("friendshipCtrl", friendshipCtrl);

    friendshipCtrl.$inject = ["$scope", "$http", "$routeParams"];

    function friendshipCtrl($scope, $http, $routeParam) {

        $scope.isAdmin = true;
        $scope.showModal = function () {
            $scope.isModalShow = true;
        }

        $scope.addFriendship = function () {
            $http.post("/api/v2/route/" + $routeParam.id + "/friends", {
                childRouteId: $scope.childId,
                FrienshipType: 0
            }).then(function (data) {
                $scope.friend = data;
                $scope.isModalShow = false;
                $http.get("/api/v2/route/" + $routeParam.id + "/friends").then(function (data) {
                    $scope.friends = data;
                });

            });

        };
        $http.get("/api/v2/route/" + $routeParam.id + "/friends").then(function (data) {
            $scope.friends = data.data;
        });

        activate();


        function activate() { }
    }

})();
