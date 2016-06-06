(function () {
    'use strict';

    angular
        .module('photo.bel.widgets')
        .directive('faqWidgetView', faqWidget);

    faqWidget .$inject = ['$window', '$http'];

    function faqWidget ($window, $http) {

        var directive = {
            link: link,
            restrict: 'EA',
            replace: true,
            scope: {
                widget: '=',
                sortableOptions: '=',
            },
            templateUrl: "Scripts/default/directives/widgets/faqWidget.html"
        };

        return directive;

        function link(scope, element, attrs) {

        }
    }

})();
