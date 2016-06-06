(function () {
    'use strict';

    angular
        .module('photo.bel.admin.directivies')
        .directive('widgetBar', widgetBar);

    widgetBar.$inject = ['$window', '$http', '$routeParams'];

    function widgetBar($window, $http, $routeParams) {

        var directive = {
            link: link,
            restrict: 'EA',
            replace: true,
            scope: {},
            templateUrl: "Scripts/admin/directivies/widgetBar/index.html"
        };

        return directive;

        function link(scope, element, attrs) {


            var originals = [
           {
               type: "gallery",
               name: "name",
               files: []
           },
           {
               type: "description"
           },
           {
               type: "splitWidget",
               widgets: []
           },
           {
               type: "containerWidget",
               widgets: []
           },
           { type: "faqWidget" },
           { type: "descriptionWidget" },
           { type: "headerWidget" },
           { type: "calendarWidget" },
           { type: "hallWidget" },
           { type: "menuWidget" }];

            scope.baseWidgets = originals.slice();

            scope.widgetsOptions = {
                connectWith: ".ng-widgets-container",
                stop: function (e, ui) {
                    if ($(e.target).hasClass("ng-first") && ui.item.sortable.droptarget && e.target !== ui.item.sortable.droptarget[0]) {
                        scope.baseWidgets = [];
                        $.each(originals, function () {
                            scope.baseWidgets.push($.extend(true, {}, this));
                        });
                    }
                }
            };

            scope.sortableOptions = {
                placeholder: "app",
                connectWith: ".ng-apps-container"
            };



            function loadFiles() {
                $http.get("/api/v2/route/" + $routeParams.id + "/files?page=" + (scope.currentPage - 1) + "&directory=" + scope.state.currentFolderID).then(function (data) {
                    scope.files = data.data;
                });
            };
            function loadFolders() {
                $http.get("/api/v2/route/" + $routeParams.id + "/files?foldersOnly=true").then(function (data) {
                    scope.folders = data.data;
                });
            };

            scope.$watch("state.currentFolderID", function () {
                loadFiles();
            });


            loadFolders();
            loadFiles();

        }
    }
})();
