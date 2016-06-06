(function () {
    "use strict";
    angular
		.module("photo.bel.admin.photographer")
        .controller("photographerAdminCtrl", photographerAdminCtrl);

    photographerAdminCtrl.$inject = ["$scope", "$http", "$location"];

    function photographerAdminCtrl($scope, $http, $location) {

        $scope.setTab = function (val) {
            $scope.tab = val;
        }
        $scope.tab = "tab1";
        $scope.title = "photographerAdminCtrl";
        $scope.item = {};
        $scope.item.firstName = "aaa-ddd";
        $scope.phone = {};
        $scope.addPhotographer = function () {
            $http({
                method: "POST",
                url: "/api/v2/photographer",
                data: $scope.item,
                headers: { 'Content-Type': "application/json" }
            })
	            .success(function (data) {
	                $scope.dataEntityID = data.id;
	            });
            $scope.setTab("tab2");
        };
        $scope.updateData = function () {
            $scope.setTab("tab3");
        }
        $scope.add = function () {
            $location.path("/admin");
        };

        activate();

        function activate() { }
    }
})();