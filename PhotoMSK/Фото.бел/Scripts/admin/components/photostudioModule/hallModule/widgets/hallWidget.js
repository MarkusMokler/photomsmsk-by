(function () {
    'use strict';

    angular
        .module('photo.bel.admin')
        .directive("hallWidget", hallWidget);

    hallWidget.$inject = ['$window', '$http'];

    function hallWidget($window, $http) {

        var directive = {
            link: link,
            restrict: 'EA',
            scope: {
                widget: '=',
                sortableOptions: '='
            },
            templateUrl: "Scripts/admin/components/photostudioModule/hallModule/widgets/hallWidget.html"
        };

        return directive;

        function link(scope, element, attrs) {

            scope.hallByNameUrl = "/api/v2/HallSearch/?name=";

            if (typeof (scope.widget.hall) == 'undefined') {
                scope.widget.hall = null;
            }

            scope.saveHall = function (selected) {
                scope.widget.isEdit = false;
                scope.widget.hall = selected.originalObject;
            }

        }
    }

})();
