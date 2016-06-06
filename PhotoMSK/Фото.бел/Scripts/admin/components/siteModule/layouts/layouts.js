(function () {
    'use strict';
    angular
        .module('photo.bel.admin.site')
        .controller('layoutsCtrl', layoutsCtrl);

    layoutsCtrl.$inject = ['$scope', '$http', '$routeParams'];

    function layoutsCtrl($scope, $http, $routeParams) {
        $scope.title = 'layoutsCtrl';
        $scope.shortcut = $routeParams.id;
        $http.get('/api/v2/layouts/?shortcut=' + $routeParams.id).then(function(response) {
            $scope.layouts = response.data;
        });

        $scope.removeLayout = function(lid) {
            $http.delete('/api/v2/layouts/?id=' + lid).then(function (response) {
                $scope.layouts = response.data;
            });
        }

        $scope.editorOptions = {
            lineWrapping : true,
            lineNumbers: true,
            readOnly: 'nocursor',
            mode: 'xml',
        };

        activate();

        function activate() { }
    }
})();
