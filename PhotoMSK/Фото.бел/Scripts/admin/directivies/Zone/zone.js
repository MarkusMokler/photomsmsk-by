(function () {
    'use strict';

    angular
        .module('photo.bel.admin')
        .directive('zone', zone);

    zone.$inject = ['$window', '$http', '$compile'];

    function zone() {

        var directive = {
            restrict: 'EA',
            scope: {
                page: "="
            },
            templateUrl: "Scripts/admin/directivies/zone/index.html",
  
            link: function (scope, element, attrs) {
                scope.name = attrs.name;

                scope.widgets = [];

                scope.removeWidgets = function (item) {
                    var iof = scope.zone.widgets.indexOf(item);
                    scope.zone.widgets.splice(iof, 1);
                }

                scope.setedit = function (widget) {
                    $.each(scope.zone.widgets, function () {
                        this.isEdit = false;
                    });
                    widget.isEdit = true;
                }

                scope.$watch("page", function () {
                    if (!scope.page)
                        return;

                    scope.name = attrs.name;
                    scope.page.zones = scope.page.zones || [];

                    scope.zone = null;

                    $.each(scope.page.zones, function () {
                        if (this.name === scope.name)
                            scope.zone = this;
                    });

                    if (!scope.zone) {
                        var obj = { name: scope.name }
                        scope.zone = obj;
                        scope.page.zones.push(obj);

                    }

                    scope.zone.widgets = scope.zone.widgets || [];

                    if (scope.page.mode == "view") {
                        scope.widgetsOptions = {};
                    }

                });
            },

        };

        return directive;
    }

})();
