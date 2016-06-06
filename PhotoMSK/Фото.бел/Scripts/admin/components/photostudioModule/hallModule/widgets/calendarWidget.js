(function () {
    'use strict';

    angular
        .module("photo.bel.admin")
        .directive("calendarWidget", calendarWidget);

    calendarWidget.$inject = ["$window", "$http"];

    function calendarWidget($window, $http) {
        var directive = {
            link: link,
            restrict: "EA",
            scope: {
                widget: "="
            },
            templateUrl: "Scripts/admin/components/photostudioModule/hallModule/widgets/calendarWidget.html"
        };

        return directive;

        function link(scope, element, attrs) {
            scope.calendars = [];
            scope.hallsByNameUrl = "/api/v2/calendarsearch/?name=";
            scope.calendarid = "1";
            if (typeof (scope.widget.calendarWidgets) == "undefined") {
                scope.widget.calendarWidgets = [];
            } else {
                $.each(scope.widget.calendarWidgets, function (key) {
                    scope.calendars.push(this.calendarID);
                });
            }

            scope.addCalendar = function (selected) {
                scope.widget.calendarWidgets.push(selected.originalObject);
                scope.calendars.push(selected.originalObject.calendarID);
            }

            scope.saveCalendars = function () {
                scope.widget.isEdit = false;
            }
        }
    }

})();
