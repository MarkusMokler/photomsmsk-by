(function () {
    'use strict';
    angular
        .module('photo.bel.admin.site')
        .controller('layoutsAddCtrl', layoutsAddCtrl);

    layoutsAddCtrl.$inject = ['$scope', '$http', '$routeParams', '$sce'];

    function layoutsAddCtrl($scope, $http, $routeParams, $sce) {
        $scope.title = 'layoutsAddCtrl';
        $scope.layout = {};
        $scope.layout.content = "<section>\n<div>\n<div class=\"group\">\n<zone page=\'page\' name=\'header\'/>\n</div>\n    </div>\n</section><header>\n    <div class=\"container container--center\">\n        <div class=\"grid\">\n            <div class=\"col--1-1\">\n                <zone page=\'page\' name=\'navigation\'/>\n            </div>\n        </div>\n    </div>\n</header>\n\n<!-- Featured  -->\n<section class=\"featured\">\n    <div>\n        <div id=\"layout-featured\" class=\"group \">\n            <zone page=\'page\' name=\'navigation\'/>\n        </div>\n    </div>\n</section>\n\n<!-- Before Main  -->\n<section id=\"main\">\n    <div class=\"container container--center\">\n\n        <div id=\"layout-before-main\" class=\"group\">\n            <zone page=\'page\' name=\'BeforeMain\'/>\n        </div>\n\n        <div id=\"layout-main-container\">\n            <div id=\"layout-main\" class=\"group\">\n                <div class=\"grid\">\n\n                    <div id=\"messages\">\n                        <zone page=\'page\' name=\'messages\'/>\n                    </div>\n\n                    <div id=\"before-content\">\n                        <zone page=\'page\' name=\'BeforeContent\'/>\n                    </div>\n\n                    <div id=\"content\" class=\"group\">\n                        <zone page=\'page\' name=\'BeforeContent\'/>\n                    </div>\n\n                    <div id=\"after-content\">\n                        <zone page=\'page\' name=\'AfterContent\'/>\n                    </div>\n\n                    <aside id=\"aside-first\" class=\"aside-first group col--1-3\">\n                        <zone page=\'page\' name=\'AsideFirst\'/>\n                    </aside>\n\n                    <aside id=\"aside-second\" class=\"aside-second col--1-3\">\n                        <zone page=\'page\' name=\'ASideSecond\'/>\n                    </aside>\n\n                </div>\n            </div>\n        </div>\n\n        <div id=\"layout-after-main\" class=\"group\">\n            <zone page=\'page\' name=\'AfterMain\'/>\n        </div>\n\n    </div>\n</section>\n\n<section class=\"tm-after-content\">\n    <div id=\"layout-tripel-container\" class=\"container container--center\">\n        <div class=\"group grid text--center\">\n\n            <div class=\"col--1-3\">\n                <zone page=\'page\' name=\'TripelFirst\'/>\n            </div>\n\n            <div class=\"col--1-3\">\n                <zone page=\'page\' name=\'TripelSecond\'/>\n            </div>\n\n            <div class=\"col--1-3\">\n                <zone page=\'page\' name=\'TripelThird\'/>\n            </div>\n\n        </div>\n    </div>\n</section>\n\n\n<footer id=\"site-footer\">\n    <div class=\"container container--center text--center\">\n        <div class=\"grid\">\n\n            <div id=\"footer-quad-first\" class=\"col--1-4\">\n                <zone page=\'page\' name=\'FooterQuadFirst\'/>\n            </div>\n\n            <div id=\"footer-quad-second\" class=\"col--1-4\">\n                <zone page=\'page\' name=\'FooterQuadSecond\'/>\n            </div>\n\n            <div id=\"footer-quad-third\" class=\"col--1-4\">\n                <zone page=\'page\' name=\'FooterQuadThird\'/>\n            </div>\n\n            <div id=\"footer-quad-fourth\" class=\"col--1-4\">\n                <zone page=\'page\' name=\'FooterQuadFourth\'/>\n            </div>\n        </div>\n        <div class=\"panel\">\n            <zone page=\'page\' name=\'Footer\'/>\n        </div>\n    </div>\n</footer>";

        activate();
        $scope.editorOptions = {
            lineWrapping: true,
            lineNumbers: true,
            mode: 'htmlmixed'
        };

        $scope.saveLayout = function() {
            $http({
                method: 'POST',
                url: '/api/v2/Layouts/?shortcut=' + $routeParams.id,
                data: $scope.layout,
                headers: { 'Content-Type': 'application/json' }
            })
              .then(function (data) {
                  swal({
                      title: 'Отлично',
                      text: '',
                      type: "success",
                      timer: 2000,
                      showConfirmButton: false
                  });
              });
        };

        function activate() { }
    }
})();
