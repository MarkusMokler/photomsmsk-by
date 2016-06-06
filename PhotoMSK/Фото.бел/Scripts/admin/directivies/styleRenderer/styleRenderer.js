(function () {
    'use strict';

    angular
        .module('photo.bel.admin')
        .directive('styleRenderer', styleRenderer);

    styleRenderer.$inject = ['$compile'];

    function styleRenderer($compile) {

        var linker = function (scope, element, attrs) {
            scope.$watch('code', function (data) {
                element.html("<style type='text/css'>" + scope.code + "</style>").show();
                $compile(element.contents())(scope);
            });
        }

        return {
            restrict: "EA",
            link: linker,
            scope: {
                code: '='
            }
        };
    }

})();
