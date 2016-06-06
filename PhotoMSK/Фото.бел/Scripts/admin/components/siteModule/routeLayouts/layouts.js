(function () {
    'use strict';
    angular
        .module('photo.bel.admin.site')
        .controller('routeLayoutsCtrl', routeLayoutsCtrl);

    routeLayoutsCtrl.$inject = ['$scope', '$http', '$routeParams'];

    function routeLayoutsCtrl($scope, $http, $routeParams) {

        $scope.modeView = function(page) {
            $.each(page, function () {
                this.mode = "view";
            });
            return page;
        };

        $scope.shortcut = $routeParams.id;
        $http.get('/api/v2/RoutePageLayouts/?shortcut=' + $routeParams.id).then(function (response) {
            $scope.rpLayouts = $scope.modeView(response.data);
        });

        $scope.setAsDefault = function(layout) {
            $http({
                method: 'PUT',
                url: '/api/v2/RoutePageLayouts/?shortcut=' + $routeParams.id,
                data: layout,
                headers: { 'Content-Type': 'application/json' }
            });
        }

        $scope.deleteRoutePageLayout = function(id) {
            $http.delete('/api/v2/RoutePageLayouts/?shortcut=' + $routeParams.id + '&rpId=' + id).then(function(response) {
                $scope.rpLayouts = $scope.modeView(response.data);
            });
        };

        var originals = [
           {
               type: "gallery",
               name: "name",
               files: []
           },
           {
               type: "description"
           },
           {
               type: "splitWidget",
               widgets: []
           },
           {
               type: "containerWidget",
               widgets: []
           },
           {
               type: "faqWidget"
           },
           {
               type: "descriptionWidget"
           },
           {
               type: "headerWidget"
           }


        ];

        $scope.baseWidgets = originals.slice();
        activate();



        $scope.currentPage = 1;
        $scope.state = {};
        $scope.state.currentFolderID = null;

        $scope.widgetsOptions = {
            connectWith: ".ng-widgets-container",
            stop: function (e, ui) {
                if ($(e.target).hasClass('ng-first') && ui.item.sortable.droptarget && e.target != ui.item.sortable.droptarget[0]) {
                    $scope.baseWidgets = [];
                    $.each(originals, function () {
                        $scope.baseWidgets.push($.extend(true, {}, this));
                    });
                }
            }
        };

        $scope.sortableOptions = {
            placeholder: "app",
            connectWith: ".ng-apps-container"
        };

        function activate() { }
    }
})();