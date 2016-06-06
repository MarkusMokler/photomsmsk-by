var app = angular.module("photoshop.bel", [
    'photoshop.bel.module',
    "ngAnimate",
    "ui.bootstrap",
    "ui.calendar",
    "ngRoute",
    "yaMap",
    "LocalStorageModule",
    "photo.bel.service",
    "photo.bel.photoshop",
    "photo.bel.account",
    "photo.bel.directives",
    "photo.bel.admin.directivies",
      "photo.bel.directives",
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
   "photo.bel.admin.route",
   "photo.bel.widgets",
    "angularMoment",
    "ui.timepicker"
]);

app.config(["$routeProvider", function ($routeProvider) {
    $routeProvider.otherwise({ redirectTo: "/" });
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