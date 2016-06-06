(function () {
    'use strict';

    angular
        .module('photo.bel.admin')
        .directive('splitWidget', splitWwidget);

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
            templateUrl: "Scripts/admin/components/photostudioModule/hallModule/widgets/splitWidget.html"
        };

        return directive;

        function link(scope, element, attrs) {

        }
    }

})();
