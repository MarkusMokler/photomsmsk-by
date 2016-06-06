(function () {
    'use strict';

    angular
        .module('photo.bel.admin')
        .directive('htmlViewer', htmlViewer);

    htmlViewer.$inject = ['$compile'];

    function htmlViewer($compile) {

        var linker = function (scope, element, attrs) {
            scope.$watch("code", function (data) {
                element.html(scope.code).show();

                if (!scope.object)
                    $compile(element.contents())(scope);
                else {
                    $.each(scope.object, function(key, value) {
                        scope[key] = value;
                    });
                    $compile(element.contents())(scope);
                }
            });
        }

        return {
            restrict: "EA",
            link: linker,
            scope: {
                code: "=",
                page: "=",
                object: "="
            }
        };
    }

})();
