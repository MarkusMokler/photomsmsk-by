angular.module("photo.bel.directives")
    .directive('mskHref', function () {
        return function (scope, element, attrs) {
            if (!('href' in attrs))
            { element.attr('href', 'javascript:void(0);'); }
        };
    });
