(function () {
    "use strict";
    angular
		.module("photo.bel.admin.settings")
        .controller("legalInformationCtrl", legalInformation);

    legalInformation.$inject = ["$scope", "$http", "$routeParams"];

    function legalInformation($scope, $http, $routeParam) {

        $scope.isAdmin = true;
        $scope.showModal = function () {
            $scope.isModalShow = true;
        }

        var fetch = function () {
            $http.get("/api/v2/route/" + $routeParam.id + "/legalInformation").then(function (data) {
                $scope.legal = data.data;
            });
        }

        $scope.save = function () {
            $http.post("/api/v2/route/" + $routeParam.id + "/legalInformation", $scope.legal).then(function (data) {
                $scope.legal = data.data;
            });
        }

        fetch();
        activate();

        function activate() { }
    }

})();
