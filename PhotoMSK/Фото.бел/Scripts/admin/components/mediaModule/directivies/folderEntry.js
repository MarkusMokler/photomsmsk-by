(function () {
    'use strict';

    angular
        .module('photo.bel.admin.media')
        .directive('folderEntry', folderEntry);

    folderEntry.$inject = ['$window', '$http'];

    function folderEntry($window, $http) {
        // Usage:
        //     <folderEntry></folderEntry>
        // Creates:
        // 
        var directive = {
            link: link,
            restrict: 'EA',
            replace: true,
            scope: {
                folder: '=',
                route: '=',
                selected:'='
            },
            templateUrl: "/Scripts/admin/components/mediaModule/directivies/index.html"
        };

        return directive;

        function link(scope, element, attrs) {

            scope.open = function (item) {
                scope.selected.currentFolderID = scope.folder.id;
                $http.get('/api/v2/route/' + scope.route + "/files?foldersOnly=true&directory=" + scope.folder.id).then(function (data) {
                    scope.collection = data.data;
                });

            }
        }
    }

})();