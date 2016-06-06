(function () {
    'use strict';

    angular
        .module('photo.bel.admin')
        .directive("menuWidget", menuWidget);

    menuWidget.$inject = ['$window', '$http'];

    function menuWidget($window, $http) {

        var directive = {
            link: link,
            restrict: 'EA',
            scope: {
                widget: '=',
                sortableOptions: '='
            },
            templateUrl: "Scripts/admin/components/photostudioModule/hallModule/widgets/menuWidget.html"
        };

        return directive;

        function link(scope, element, attrs) {
            console.log(scope.widget)

            scope.menuByNameUrl = "/api/v2/RouteMenuSearch/?name=";

            if (typeof (scope.widget.menu) == "undefined") {
                scope.widget.menu = [];
            }
            $http.get("api/v2/MenuItem/?shortcut=" + " " + "&routeMenuId=" + scope.widget.menu.id).then(function (response) {
                scope.widget.menu = response.data.menu;
            });
            scope.saveMenu = function (selected) {
                scope.widget.isEdit = false;
                scope.widget.menu = selected.originalObject;
                
                $http.get("api/v2/MenuItem/?shortcut=" + " " + "&routeMenuId=" + scope.widget.menu.id).then(function (response) {
                    scope.widget.menu.nodes = response.data.menu.nodes;
                });
            }

            
        }
    }

})();
