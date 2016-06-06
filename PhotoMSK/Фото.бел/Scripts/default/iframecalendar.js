var app = angular.module("iframecalendar.bel", [
    "ngAnimate",
    "ui.bootstrap",
    "ui.calendar",
    "ngRoute",
    "yaMap",
    "LocalStorageModule",
    "photo.bel.service",
    "photo.bel.photostudio",
    "photo.bel.account",
    "photo.bel.admin",
    "photo.bel.directives",
    "photo.bel.admin.directivies",
    "angularMoment",
    "ui.timepicker"
]);
app.config([
    '$locationProvider', function ($locationProvider) {
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });
    }
]);
app.config([
        "$routeProvider", function ($routeProvider) {
            $routeProvider.when("/calendar/:id", {
                templateUrl: "Scripts/default/components/iframecalendarModule/calendarCtrl/index.html",
                controller: "calendarIFrameCtrl"
            });
        }
]);

app.config(["$httpProvider", function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
    $httpProvider.interceptors.push('httpInterceptor');
    $httpProvider.interceptors.push('httpStatusInspector');
}]);

app.factory("$signalr", ["localStorageService", function (localStorageService) {

    $.signalR.ajaxDefaults.headers = { Authorization: "Bearer " + (localStorageService.get('authorizationData') || { token: "" }).token };

    $.connection.hub.start().done(function () { });

    return {
        cartHub: $.connection.cartHub
    }
}]);