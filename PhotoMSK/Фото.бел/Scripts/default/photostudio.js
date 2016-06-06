var photostudio = angular.module("photostudio.bel", [
    "angularTreeview",
    "ui.tree",
    "ui.codemirror",
    "ngAnimate",
    "ui.bootstrap",
    "ui.calendar",
    "ngRoute",
    "yaMap",
    "photostudio.bel.directives",
    "LocalStorageModule",
    "photo.bel.admin",
    "photo.bel.service",
    "photo.bel.photostudio",
    "photo.bel.account",
    "photo.bel.directives",
    "photo.bel.admin.directivies",
    "photo.bel.widgets",
    "ui.timepicker",
    "ngSanitize"
]);

photostudio.run(['$rootScope', function ($rootScope) {
    $rootScope.isWhiteLabel = true;
}]);

photostudio.config(["$routeProvider", function ($routeProvider) {
    $routeProvider.when("/", {
        templateUrl: "Scripts/default/components/photostudioModule/photostudio/index",
        controller: "photostudioCtrl"
    })
          .when("/admin/whitelabel/", {
              label: 'Вайт лейбл'
          })

          .when("/admin/whitelabel/:id", {
              label: '{{id}}'
          });
}]);

photostudio.config([
    '$locationProvider', function ($locationProvider) {
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });
    }
]);

photostudio.config(["localStorageServiceProvider", function (localStorageServiceProvider) {
    localStorageServiceProvider.setPrefix('фото.бел');
}]);

photostudio.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
    $httpProvider.interceptors.push('httpInterceptor');
}]);