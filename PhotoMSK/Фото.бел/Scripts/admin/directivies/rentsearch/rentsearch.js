(function () {
    "use strict";


    angular.module("photo.bel.admin.directivies")
        .directive("adminRentSearch", adminRentSearch);

    adminRentSearch.$inject = ["$http", "$cart"];

    function adminRentSearch($http, $cart) {
        var directive = {
            scope: {
                shortcut: "="
            },
            link: link,
            restrict: "EA",
            templateUrl: "/Scripts/admin/directivies/rentsearch/index.html"
        };

        return directive;

        function link(scope, element, attrs) {
            scope.dataUrl = 'api/v2/RentCalendar/?shortcut=' + scope.shortcut;
            $http.get(scope.dataUrl).then(function (response) {
                scope.item = response.data;
            });
        }
    };

})();