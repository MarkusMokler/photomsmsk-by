﻿(function () {
    'use strict';

    angular
        .module('photo.bel.admin')
        .directive('textAndDesc', textAndDesc);

    textAndDesc.$inject = ['$window', '$http'];

    function textAndDesc($window, $http) {

        var directive = {
            link: link,
            restrict: 'EA',
            replace: true,
            scope: {
                widget: '=',
            },
            templateUrl: "Scripts/admin/components/photostudioModule/hallModule/widgets/TextDesc.html"
        };

        return directive;

        function link(scope, element, attrs) {

            scope.files = [];

            scope.options = {
                update: function (e, ui) {
                    scope.widget.photo = ui.item.sortable.moved.attachment;
                    scope.$apply();
                }
            };

            scope.$watch('files', function () {
                if (scope.files.length > 0)
                    scope.widget.photo = scope.files.splice()[0];
            });
        }
    }

})();