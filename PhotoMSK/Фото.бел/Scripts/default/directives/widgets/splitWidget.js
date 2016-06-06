(function () {
    'use strict';

    angular
        .module('photo.bel.widgets')
        .directive('splitWidgetView', splitWwidget);

    splitWwidget.$inject = ['$window', '$http'];

    function splitWwidget($window, $http) {

        var directive = {
            link: link,
            restrict: 'EA',
            replace: true,
            scope: {
                widget: '=',
                sortableOptions: '=',
            },
            templateUrl: "Scripts/default/directives/widgets/splitWidget.html"
        };

        return directive;

        function link(scope, element, attrs) {

        }
    }

})();
