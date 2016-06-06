(function () {
    'use strict';

    angular
        .module('photo.bel.admin.directivies')
        .directive("adminBookingUserInfo", userInfo);

    userInfo.$inject = ["$window", "$http", "$compile", "$cart"];

    function userInfo($window, $http, $compile, $cart) {

        var directive = {
            restrict: 'EA',
            scope: {
                page: "="
            },
            templateUrl: '/Scripts/admin/directivies/bookinginfo/index.html',

            link: link


        };

        return directive;

        function link(scope, element, attrs) {
            scope.usersByPhoneUrl = "/api/v2/userinformation/?search=";

            scope.init = function() {
                scope.results = [];
                scope.user = $cart.getUser();
                scope.user.firstName = null;
                scope.user.lastName = null;
                scope.selected = null;
                scope.more = $cart.getMore();
                scope.more.isMore = false;

                scope.noResults = false;

                scope.dataInit = "+375";
            }

            scope.$on('shoppingState', function (event, data) {
                if (data == "clear") {
                    scope.init();
                }
            });

            scope.$watch('selected', function () {
                $cart.setSelected(scope.selected);
            });

            scope.init();

            scope.removePenalty = function (penalty) {
                $http({
                    method: 'DELETE',
                    url: '/api/v2/userinformation/' + scope.selected.originalObject.id + '/penalties',
                    data: scope.selected.originalObject.penalties[penalty],
                    headers: { 'Content-Type': 'application/json' }
                })
           .success(function () {
               scope.selected.originalObject.penalties.splice(penalty, 1);
           });
            };
        }
    }
})();
