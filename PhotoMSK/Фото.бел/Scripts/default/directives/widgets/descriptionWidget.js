(function () {
    'use strict';

    angular
        .module('photo.bel.widgets')
        .directive('descriptionWidgetView', descriptionWidget);

    descriptionWidget.$inject = ['$window', '$http'];

    function descriptionWidget($window, $http) {

        var directive = {
            link: link,
            restrict: 'EA',
            replace: true,
            scope: {
                widget: '=',
            },
            templateUrl: "Scripts/default/directives/widgets/descriptionWidget.html"
        };

        return directive;

        function link(scope, element, attrs) {

        }
    }

})();
