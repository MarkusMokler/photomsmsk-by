(function () {
    'use strict';

    angular
        .module('photo.bel.admin')
        .directive("headerWidget", headerWidget );

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
            templateUrl: "Scripts/admin/components/photostudioModule/hallModule/widgets/headerWidget.html"
        };

        return directive;

        function link(scope, element, attrs) {

        }
    }

})();
