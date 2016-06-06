(function () {
    "use strict";

    angular
        .module("photo.bel.admin")
        .directive("menuItem", menuItem);

    menuItem.$inject = ["$window", "$http"];

    function menuItem($window, $http) {

        var directive = {
            link: link,
            restrict: "EA",
            replace: true,
            scope: {
                item: "="
            },
            templateUrl: "Scripts/admin/components/siteModule/menu/directive/menuItem.html"
        };

        return directive;

        function link(scope, element, attrs) {
            scope.item.nodes = scope.item.nodes || [];
            scope.sortableOptions = {
                placeholder: "placeholder--menu",
                connectWith: ".ng-menus",
                handle: '.sortable__handle'
            };
        }
    }

})();
