(function () {
    "use strict";

    angular
        .module("photo.bel.admin.services")
        .factory("$permission", permission);

    permission.$inject = ["$location"];

    function permission($location) {

        var service = {
            isAdmin: function () {
                return $location.path().indexOf("/admin") === 0;
            }
        };
        return service;
    }
})();