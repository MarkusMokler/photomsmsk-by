(function () {
    'use strict';
    angular
        .module('whitelabel.bel')
        .controller('whiteLabelCtrl', whiteLabelCtrl);

    whiteLabelCtrl.$inject = ['$scope', '$http', '$routeParams'];

    function whiteLabelCtrl($scope, $http, $routeParams) {

        $scope.title = 'whiteLabelCtrl';
        var slug;
        if (typeof ($routeParams.slug) == "undefined")
            slug = "";
        else {
            slug = $routeParams.slug;
        }

        $http.get("/api/v2/route/" + $scope.Route + "/page?url=" + slug).then(function (response) {
            $scope.itemRoutePage = response.data;
            $scope.itemRoutePage.mode = "view";
        });

        activate();

        function activate() { }
    }
})();