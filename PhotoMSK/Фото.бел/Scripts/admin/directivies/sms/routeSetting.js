(function () {
    'use strict';

    angular
        .module('photo.bel.admin.directivies')
        .directive('routeSetting', setting);

    setting.$inject = ['$window', '$http'];

    function setting($window, $http) {

        var directive = {
            link: link,
            restrict: 'EA',
            replace: true,
            scope: {
                setting: '=',
            },
            templateUrl: "Scripts/admin/directivies/sms/routeSetting.html"
        };

        return directive;

        function link(scope, element, attrs) {

        }
    }

})();
