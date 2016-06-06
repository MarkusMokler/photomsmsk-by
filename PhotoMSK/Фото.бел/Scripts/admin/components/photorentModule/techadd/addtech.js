(function () {
    'use strict';
    angular
		.module('photo.bel.admin.photorent')
        .controller('addTechCtrl', addTechCtrl);

    addTechCtrl.$inject = ['$scope', '$http', '$routeParams'];
    function addTechCtrl($scope, $http, $routeParams) {
        $scope.title = 'addTechCtrl';
        $scope.routeShortcut = $routeParams.id;
        $http.get("/api/v2/RentCalendar/?shortcut=" + $routeParams.id)
            .success(function (data) {
                $scope.item = data.elements;
            });
        $scope.url = "/api/v2/Phototechnics/?search=";
        $scope.addRent = function () {
            $scope.view.PhototechnicsID = $scope.selected.originalObject.id;
            $http({
                method: 'POST',
                url: '/api/v2/RentCalendar/?shortcut=' + $scope.routeShortcut,
                data: $scope.view,
                headers: { 'Content-Type': 'application/json' }
            })
            .success(function (dadta) {
                swal({
                    title: 'Успешно',
                    text: 'Техника добавлена',
                    type: "success",
                    timer: 2000,
                    showConfirmButton: false
                });
            });
        };
        activate();
        function activate() { }
    }
})();