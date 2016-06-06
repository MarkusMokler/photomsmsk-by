(function () {
    'use strict';
    angular
        .module('photo.bel.admin.site')
        .controller('pageCreateWLCtrl', pageCreateWLCtrl);

    pageCreateWLCtrl.$inject = ['$scope', '$http', '$routeParams'];

    function pageCreateWLCtrl($scope, $http, $routeParams) {

        $scope.title = 'pageWLCtrl';
        $scope.shortcut = $routeParams.id;
        $scope.dateTimeNow = new Date();
        $scope.selected ={}
        $scope.page = {};
        $scope.category = {};

        $scope.editorOptions = {
            lineWrapping: true,
            lineNumbers: true,
            mode: 'htmlmixed'
        };

        $scope.routePages = "api/v2/TextPage/?search=";

        $http.get('api/v2/route/' + $routeParams.id + '/TextPageCategory').success(function (data) {
            $scope.categoryList = data;
        });
        $scope.createCategory = function () {
            $http({
                method: 'POST',
                url: '/api/v2/route/' + $routeParams.id + "/TextPageCategory",
                data: $scope.category,
                headers: { 'Content-Type': 'application/json' }
            })
                .success(function (data) {
                    $scope.categoryList.push(data);
                });
        }
        $scope.savePage = function () {
            $scope.page.pageCategoryID = $scope.selectedCategory.id;
            $scope.page.pageLayoutID = $scope.selected.originalObject.id;
            $http({
                method: 'POST',
                url: '/api/v2/TextPage/?shortcut=' + $routeParams.id,
                data: $scope.page,
                headers: { 'Content-Type': 'application/json' }
            });
        }


        activate();
        function activate() { }
    }
})();