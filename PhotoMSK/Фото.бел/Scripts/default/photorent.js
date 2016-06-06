var app = angular.module("photorent.bel", [
    "ngAnimate",
    "ui.bootstrap",
    "ui.calendar",
    "ngRoute",
    "yaMap",
    "LocalStorageModule",
    "photo.bel.photorent",
    "photo.bel.service",
    "photo.bel.account",
    "photo.bel.directives",
  "photo.bel.admin.directivies",
  "photo.bel.admin.blog",
  "photo.bel.admin.callhandler",
  "photo.bel.admin.cardchecker",
  "photo.bel.admin.deleteroute",
  "photo.bel.admin.developer",
  "photo.bel.admin.dialog",
  "photo.bel.admin.hall",
  "photo.bel.admin.home",
  "photo.bel.admin.masterclass",
  "photo.bel.admin.page",
  "photo.bel.admin.photographer",
  "photo.bel.admin.photomodel",
  "photo.bel.admin.photorent",
  "photo.bel.admin.photoshop",
  "photo.bel.admin.photostudio",
  "photo.bel.admin.plasticcard",
  "photo.bel.admin.statistics",
  'photo.bel.admin.statistic',
  "photo.bel.admin.userinformation",
  "photo.bel.admin.usersmanagement",
  "photo.bel.admin.whitelabel",
  "photo.bel.admin.route",
  "photo.bel.widgets",
    "angularMoment",
    "ui.timepicker"
]);

app.config(["$routeProvider", function ($routeProvider) {
    $routeProvider.when("/", {
        templateUrl: "Scripts/default/whitelabel/rentApp/index.html",
        controller: "rentAppCtrl"
    });
}]);

app.config([
    '$locationProvider', function ($locationProvider) {
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });
    }
]);

app.config(["localStorageServiceProvider", function (localStorageServiceProvider) {
    localStorageServiceProvider.setPrefix('фото.бел');
}]);

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
    $httpProvider.interceptors.push('httpInterceptor');
});