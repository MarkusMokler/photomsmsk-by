(function () {
    'use strict';

    angular
        .module('photo.bel.admin')
        .directive('galleryEdit', galleryEdit);

    galleryEdit.$inject = ['$window', '$http'];

    function galleryEdit($window, $http) {

        var directive = {
            link: link,
            restrict: 'EA',
            replace: true,
            scope: {
                widget: '=',
                sortableOptions: '=',
            },
            templateUrl: "Scripts/admin/components/photostudioModule/hallModule/widgets/Gallery.html"
        };

        return directive;

        function link(scope, element, attrs) {

            scope.remove = function(item) {
                var iof = scope.widget.files.indexOf(item);
                scope.widget.files.splice(iof,1);
            }
        }
    }

})();
