var whitelabel = angular.module("whitelabel.bel", [
    "angularTreeview",
    "ui.tree",
    "ui.codemirror",
    "ngAnimate",
    "ui.bootstrap",
    "ui.calendar",
    "ngRoute",
    "yaMap",
    "LocalStorageModule",
    "photo.bel.service",
    "photo.bel.account",
    "photo.bel.directives",
    "photo.bel.widgets",
    "ui.timepicker",
    "ngSanitize",
    "ui.sortable",
    "photo.bel.directives",
    "photo.bel.admin.directivies",
      "photo.bel.admin.site",
  'photo.bel.admin.services',
  "textAngular"
]);

whitelabel.config(["$routeProvider", function ($routeProvider) {
    $routeProvider.when("/", {
        templateUrl: "Scripts/default/whitelabel/wl/index.html",
        controller: "whiteLabelCtrl"
    })
    .when("/:slug", {
        templateUrl: "Scripts/default/whitelabel/wl/index.html",
        controller: "whiteLabelCtrl"
    });
}]);

whitelabel.config([
    '$locationProvider', function ($locationProvider) {
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });
    }
]);

whitelabel.config(["localStorageServiceProvider", function (localStorageServiceProvider) {
    localStorageServiceProvider.setPrefix('фото.бел');
}]);

whitelabel.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
    $httpProvider.interceptors.push('httpInterceptor');
}]);