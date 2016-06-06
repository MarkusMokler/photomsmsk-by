(function () {
    'use strict';
    angular
        .module('photo.bel.admin.site')
        .controller('layoutsEditCtrl', layoutsEditCtrl);

    layoutsEditCtrl.$inject = ['$scope', '$http', '$routeParams'];

    function layoutsEditCtrl($scope, $http, $routeParams) {
        $scope.title = 'layoutsCtrl';
        $http.get('/api/v2/layouts/?id=' + $routeParams.lid).then(function (response) {
            $scope.layout = response.data;
        });
        activate();
        $scope.editorOptions = {
            lineWrapping: true,
            lineNumbers: true,
            mode: 'htmlmixed'
        };

        $scope.updateLayout = function () {
            $http({
                method: 'PUT',
                url: '/api/v2/Layouts/?id=' + $routeParams.lid,
                data: $scope.layout,
                headers: { 'Content-Type': 'application/json' }
            })
              .then(function (data) {
                  swal({
                      title: 'Успешно',
                      text: '',
                      type: "success",
                      timer: 2000,
                      showConfirmButton: false
                  });
              });
        };

        function activate() { }
    }
})();
