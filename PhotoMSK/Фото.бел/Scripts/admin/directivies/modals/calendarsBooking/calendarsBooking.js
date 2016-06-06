/// <reference path="index.html" />
(function () {
    'use strict';

    angular
        .module("photo.bel.admin.directivies")
        .directive("calendarsBooking", calendarsBooking);

    calendarsBooking.$inject = ["$http", "$cart", "$permission"];

    function calendarsBooking($http, $cart, $permission) {
        // Usage:
        //     <calendarsBooking></calendarsBooking>
        // Creates:
        // 
        var directive = {
            link: link,
            restrict: "EA",
            scope: {
                calendars: "=",
                span: "=",
                state: "="
            },
            templateUrl: "/Scripts/admin/directivies/modals/calendarsBooking/index.html"

        };
        return directive;

        function link(scope, element, attrs) {
            scope.calendarsID = [];
            function fetch() {
                scope.start = moment(scope.span.start);
                scope.end = moment(scope.span.end);
                $http({
                    method: "GET",
                    url: "/api/v2/CalendarSearch",
                    params: {
                        ids: scope.calendars,
                        start:scope.start.format("YYYY-MM-DD HH:mm"),
                        end: scope.end.format("YYYY-MM-DD HH:mm")
                    }
                }).then(function (response) {
                    scope.bookinngs = response.data;
                });
            }

            scope.closeModal = function() {
                scope.state.open = false;
            }

            scope.addHall = function (calendarID) {
                var index = scope.calendarsID.indexOf(calendarID);

                if (index === -1) {
                    scope.calendarsID.push(calendarID);
                } else {
                    scope.calendarsID.splice(index, 1);
                }
            }

            scope.isSelected = function (calendarID) {
                return scope.calendarsID.indexOf(calendarID) !== -1;
            }

            scope.shoppingCart = function () {
                var shoppingCartData = {
                    validate: !$permission.isAdmin,
                    start: scope.span.start,
                    end: scope.span.end,
                    calendarIDs: scope.calendarsID
                };
                $cart.add(shoppingCartData);
                scope.state.open = false;
             }

            scope.$watch("span", function () {
                if (scope.span != null && typeof (scope.span) != "undefined")
                    fetch();
            });
        }
    }

})();