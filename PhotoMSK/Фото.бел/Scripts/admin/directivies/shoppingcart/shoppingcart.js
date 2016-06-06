(function () {
    "use strict";


    angular.module("photo.bel.admin.directivies")
        .directive("adminShoppingCart", cartditective);

    cartditective.$inject = ["$http", "$cart"];

    function cartditective($http, $cart) {
        var directive = {
            scope: {},
            link: link,
            restrict: "EA",
            templateUrl: "/Scripts/admin/directivies/shoppingcart/index.html"
        };

        return directive;

        function link(scope, element, attrs) {

            scope.init = function() {
                scope.shoppingCartList = $cart.getShoppingCartList();
                scope.userInformation = null;
            }

            scope.removeEventFromCart = function (item) {
                $cart.remove(item);
            }

            scope.$on('shoppingState', function (event, data) {
                if (data === "clear") {
                    scope.init();
                }
            });

            scope.init();

            $http.get("/api/Services/Account/GetUserInformation").then(function (data) {
                scope.userInformation = data.data.userInformation;
            });

            scope.tryBookHalls = function () {
                $cart.tryBookHalls();
            }
        }
    };

})();
