(function () {
    'use strict';

    angular
        .module('photo.bel.widgets')
        .directive("headerWidgetView", headerWidget);

    headerWidget .$inject = ['$window', '$http'];

    function headerWidget ($window, $http) {

        var directive = {
            link: link,
            restrict: 'EA',
            replace: true,
            scope: {
                widget: '=',
                sortableOptions: '=',
            },
            templateUrl: "Scripts/default/directives/widgets/headerWidget.html"
        };

        return directive;

        function link(scope, element, attrs) {

        }
    }

})();
