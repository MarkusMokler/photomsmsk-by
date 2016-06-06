(function () {
    'use strict';
    angular
        .module('photo.bel.admin.site')
        .controller('themesWLCtrl', themesWLCtrl);

    themesWLCtrl.$inject = ['$scope', '$http', '$routeParams'];

    function themesWLCtrl($scope, $http, $routeParams) {

        $scope.style = {};
        $scope.selectedStyle = null;
        $scope.selectedRoutePage = null;

        $http.get("/api/v2/styles/?shortcut=" + $routeParams.id).then(function (response) {
            $scope.styles = response.data;
        });

        $http.get("/api/v2/photostudio/?id=" + $routeParams.id).then(function (response) {
            $scope.photostudio = response.data;
        });

        $http.get("/api/v2/routepagelayouts/?shortcut=" + $routeParams.id).then(function (response) {
            $scope.routePages = response.data;
        });

        $scope.saveStyle = function () {
            $scope.selectedStyle.routeID = $scope.photostudio.id;
            $scope.selectedStyle.routePageLayoutID = $scope.selectedRoutePage.id;
            $http({
                method: "POST",
                url: "/api/v2/styles/",
                data: $scope.selectedStyle,
                headers: { 'Content-Type': "application/json" }
            })
              .then(function (data) {
                    $scope.styles.push(data.data);
                });
        }

        $scope.updateStyle = function () {
            $scope.selectedStyle.routeID = $scope.photostudio.id;
            $scope.selectedStyle.routePageLayoutID = $scope.selectedRoutePage.id;
            $http({
                method: "PUT",
                url: "/api/v2/styles/",
                data: $scope.selectedStyle,
                headers: { 'Content-Type': "application/json" }
            });
        }

        $scope.editorOptions = {
            lineWrapping: true,
            lineNumbers: true,
            mode: "text/css",
            extraKeys: { "Ctrl-Space": "autocomplete" }
        };
        activate();

        function activate() { }
    }
})();