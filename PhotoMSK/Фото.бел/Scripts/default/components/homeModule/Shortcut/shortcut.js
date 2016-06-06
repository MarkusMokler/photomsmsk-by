(function () {
    "use strict";

    angular
        .module("photo.bel.home")
        .controller("shortcutCtrl", shortcutCtrl);

    shortcutCtrl.$inject = ["$scope", "$http", "$routeParams"];

    function shortcutCtrl($scope, $http, $routeParams) {
        $scope.title = "shortcut";

        $http.get("/api/v2/route/" + $routeParams.id).then(function (data) {
            $scope.item = data.data;
        });

        activate();

        function activate() { }
    }
})();