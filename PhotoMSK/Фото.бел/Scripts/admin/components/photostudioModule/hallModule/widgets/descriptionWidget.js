(function () {
    'use strict';

    angular
        .module('photo.bel.admin')
        .directive('descriptionWidget', descriptionWidget);

    descriptionWidget.$inject = ['$window', '$http'];

    function descriptionWidget($window, $http) {

        var directive = {
            link: link,
            restrict: 'EA',
            replace: true,
            scope: {
                widget: '=',
            },
            templateUrl: "Scripts/admin/components/photostudioModule/hallModule/widgets/descriptionWidget.html"
        };

        return directive;

        function link(scope, element, attrs) {

        }
    }

})();
