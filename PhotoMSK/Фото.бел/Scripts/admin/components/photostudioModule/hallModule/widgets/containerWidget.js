(function () {
    'use strict';

    angular
        .module('photo.bel.admin')
        .directive('containerWidget', containerWidget);

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
            templateUrl: "Scripts/admin/components/photostudioModule/hallModule/widgets/containerWidget.html"
        };

        return directive;

        function link(scope, element, attrs) {

        }
    }

})();
