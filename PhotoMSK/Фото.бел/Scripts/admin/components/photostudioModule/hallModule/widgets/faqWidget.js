(function () {
    'use strict';

    angular
        .module('photo.bel.admin')
        .directive('faqWidget', faqWidget );

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
            templateUrl: "Scripts/admin/components/photostudioModule/hallModule/widgets/faqWidget.html"
        };

        return directive;

        function link(scope, element, attrs) {

        }
    }

})();
