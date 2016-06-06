(function () {
    'use strict';

    angular
        .module('photo.bel.widgets')
        .directive('containerWidgetView', containerWidget);

    containerWidget.$inject = ['$window', '$http'];

    function containerWidget($window, $http) {

        var directive = {
            link: link,
            restrict: 'EA',
            replace: true,
            scope: {
                widget: '=',
                sortableOptions: '=',
            },
            templateUrl: "Scripts/default/directives/widgets/containerWidget.html"
        };

        return directive;

        function link(scope, element, attrs) {

        }
    }

})();
