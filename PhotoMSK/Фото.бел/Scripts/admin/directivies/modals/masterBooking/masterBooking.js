(function () {
    'use strict';

    angular
        .module('photo.bel.admin.directivies')
        .directive('masterBooking', masterBooking)
        .filter('datetimeToNow', function () {
            return function (input) {
                if (!input) return;

                return moment(input).fromNow();
            };
        });

    masterBooking.$inject = ["$http", "$cart", "$permission", "@authService", "$interval", "$window", "$signalr"];

    function masterBooking($http, $cart, $permission, $authService, $interval, $window, $signalr) {
        var directive = {
            link: link,
            restrict: "EA",
            scope: {
                shortcut: "=",
                span: "=",
                state: "="
            },
            templateUrl: "/Scripts/admin/directivies/modals/masterBooking/index.html"

        };
        return directive;

        function link(scope, element, attrs) {
            scope.routes = [];
            scope.authService = $authService;
            scope.permission = $permission;
            scope.$cart = $cart;
            scope.$cart.endTime = moment().add(10, "m");


         

            angular.element($window).bind("beforeunload", function () {
                $signalr.cartHub.server.clearCart();
            });

            scope.item = {}

            $interval(function () {
                scope.$cart.endTime.add(1, "s");
            }, 1000 * 30);

            scope.masterNextPage = function () {
                var index = scope.routes.indexOf(scope.currentRote);
                if (index + 1 < scope.routes.length)
                    scope.currentRote = scope.routes[index + 1];
                else {

                    scope.offer = true;
                    scope.shoppingCart();
                }
            }

            scope.calendarsID = [];

            function fetch() {

                scope.offer = false;
                scope.currentRote = null;

                $http.get("/api/v2/Calendar?shortcut=" + scope.shortcut + "&start=" + moment(scope.span.start).format("YYYY-MM-DD HH:mm") + "&end=" + moment(scope.span.end).format("YYYY-MM-DD HH:mm"))
                    .then(function (data) {
                        scope.routes = data.data;
                        if ($authService.isAuth())
                            scope.currentRote = scope.routes[0];

                        $http.get("/api/v2/route/" + scope.shortcut + "/legalInformation").then(function (data) {
                            scope.legal = data.data;
                        });
                    });
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
            scope.isRouteActive = function (item) {
                return scope.currentRote === item;
            }
            scope.setRoute = function (item) {
                scope.currentRote = item;
            }
            scope.shoppingCart = function () {

                var shoppingCartData = {
                    validate: !$permission.isAdmin,
                    start: scope.span.start,
                    end: scope.span.end,
                    calendarIDs: scope.calendarsID
                };

                $cart.add(shoppingCartData);
                scope.calendarsID = [];

                if (window === window.top)
                    scope.state.open = false;
            };
            scope.prefinish = function () {
                scope.offer = false;
                scope.accepted = true;
                scope.lastStep = true;
            }

            scope.addmore = function () {
                scope.offer = false;
                scope.accepted = false;
                scope.lastStep = false;
                scope.state.open = false;
            }

            scope.init = function () {
                scope.shoppingCartList = $cart.getShoppingCartList();
                scope.userInformation = null;
            }

            scope.removeEventFromCart = function (item) {
                $cart.remove(item);
            }

            scope.$on("shoppingState", function (event, data) {
                if (data === "clear") {
                    scope.init();
                }
            });

            scope.init();


            scope.tryBookHalls = function () {
                $cart.tryBookHalls();
            }
            scope.closeModal = function () {
                scope.state.open = false;
            }
            scope.isHasNext = function () {
                return scope.routes.indexOf(scope.currentRote) !== scope.routes.length - 1;
            }
            scope.byPass = function () {
                scope.choosed = true;
                scope.displayPassword = true;
            }
            scope.bySMS = function () {
                scope.choosed = true;
                scope.sendPhone();
            }
            scope.login = function () {
                scope.item.userName = scope.item.phone;
                $authService.login(scope.item).then(function () {

                    if ($authService.isAuth())
                        scope.currentRote = scope.routes[0];

                }, function () {
                    scope.loginError = "Пользователя с таким логином или паролем не существует";
                    scope.loginErrorEmpty = false;
                });
            }
            scope.sendPhone = function () {
                $http.post("/api/Services/Account/Create?id=" + scope.item.phone).success(function (data) {
                    scope.formState = data.action;
                    scope.item.userId = data.userID;
                }).error(function (data) {
                    console.log(data);
                    scope.message = data.message;
                });
            }
            scope.registerUser = function () {
                $http.post("/api/Services/Account/Register", scope.item).success(function () {
                    authService.login(scope.item);
                }).error(function (data) {
                    console.log(data);
                    scope.userModelErrors = data.modelState;
                });
            }
            scope.restoreUser = function () {
                scope.item.userName = scope.item.phone;
                scope.item.password = scope.item.confirmCode;
                $authService.login(scope.item).then(function () {
                    if ($authService.isAuth())
                        scope.currentRote = scope.routes[0];
                });
            }
            scope.$watch("span", function () {
                if (scope.span != null && typeof (scope.span) != "undefined")
                    fetch();
            });
        }
    }

})();