'use strict';

; (function ($) {

    var b64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/",
        a256 = '',
        r64 = [256],
        r256 = [256],
        i = 0;

    var UTF8 = {

        /**
         * Encode multi-byte Unicode string into utf-8 multiple single-byte characters
         * (BMP / basic multilingual plane only)
         *
         * Chars in range U+0080 - U+07FF are encoded in 2 chars, U+0800 - U+FFFF in 3 chars
         *
         * @param {String} strUni Unicode string to be encoded as UTF-8
         * @returns {String} encoded string
         */
        encode: function (strUni) {
            // use regular expressions & String.replace callback function for better efficiency
            // than procedural approaches
            var strUtf = strUni.replace(/[\u0080-\u07ff]/g, // U+0080 - U+07FF => 2 bytes 110yyyyy, 10zzzzzz
            function (c) {
                var cc = c.charCodeAt(0);
                return String.fromCharCode(0xc0 | cc >> 6, 0x80 | cc & 0x3f);
            })
            .replace(/[\u0800-\uffff]/g, // U+0800 - U+FFFF => 3 bytes 1110xxxx, 10yyyyyy, 10zzzzzz
            function (c) {
                var cc = c.charCodeAt(0);
                return String.fromCharCode(0xe0 | cc >> 12, 0x80 | cc >> 6 & 0x3F, 0x80 | cc & 0x3f);
            });
            return strUtf;
        },

        /**
         * Decode utf-8 encoded string back into multi-byte Unicode characters
         *
         * @param {String} strUtf UTF-8 string to be decoded back to Unicode
         * @returns {String} decoded string
         */
        decode: function (strUtf) {
            // note: decode 3-byte chars first as decoded 2-byte strings could appear to be 3-byte char!
            var strUni = strUtf.replace(/[\u00e0-\u00ef][\u0080-\u00bf][\u0080-\u00bf]/g, // 3-byte chars
            function (c) { // (note parentheses for precence)
                var cc = ((c.charCodeAt(0) & 0x0f) << 12) | ((c.charCodeAt(1) & 0x3f) << 6) | (c.charCodeAt(2) & 0x3f);
                return String.fromCharCode(cc);
            })
            .replace(/[\u00c0-\u00df][\u0080-\u00bf]/g, // 2-byte chars
            function (c) { // (note parentheses for precence)
                var cc = (c.charCodeAt(0) & 0x1f) << 6 | c.charCodeAt(1) & 0x3f;
                return String.fromCharCode(cc);
            });
            return strUni;
        }
    };

    while (i < 256) {
        var c = String.fromCharCode(i);
        a256 += c;
        r256[i] = i;
        r64[i] = b64.indexOf(c);
        ++i;
    }

    function code(s, discard, alpha, beta, w1, w2) {
        s = String(s);
        var buffer = 0,
            i = 0,
            length = s.length,
            result = '',
            bitsInBuffer = 0;

        while (i < length) {
            var c = s.charCodeAt(i);
            c = c < 256 ? alpha[c] : -1;

            buffer = (buffer << w1) + c;
            bitsInBuffer += w1;

            while (bitsInBuffer >= w2) {
                bitsInBuffer -= w2;
                var tmp = buffer >> bitsInBuffer;
                result += beta.charAt(tmp);
                buffer ^= tmp << bitsInBuffer;
            }
            ++i;
        }
        if (!discard && bitsInBuffer > 0) result += beta.charAt(buffer << (w2 - bitsInBuffer));
        return result;
    }

    var Plugin = $.base64 = function (dir, input, encode) {
        return input ? Plugin[dir](input, encode) : dir ? null : this;
    };

    Plugin.btoa = Plugin.encode = function (plain, utf8encode) {
        plain = Plugin.raw === false || Plugin.utf8encode || utf8encode ? UTF8.encode(plain) : plain;
        plain = code(plain, false, r256, b64, 8, 6);
        return plain + '===='.slice((plain.length % 4) || 4);
    };

    Plugin.atob = Plugin.decode = function (coded, utf8decode) {
        coded = coded.replace(/[^A-Za-z0-9\+\/\=]/g, "");
        coded = String(coded).split('=');
        var i = coded.length;
        do {
            --i;
            coded[i] = code(coded[i], true, r64, a256, 6, 8);
        } while (i > 0);
        coded = coded.join('');
        return Plugin.raw === false || Plugin.utf8decode || utf8decode ? UTF8.decode(coded) : coded;
    };
}(jQuery));


var service = angular.module("photo.bel.service", ["LocalStorageModule"]);

service.factory("@authService", ["$http", "$q", "localStorageService", function ($http, $q, localStorageService) {

    var authServiceFactory = {};

    var _authentication = {
        isAuth: false,
        userName: ""
    };
    authServiceFactory.authentication = _authentication;
    var _saveRegistration = function (registration) {

        _logOut();

        return $http.post('api/account/register', registration).then(function (response) {
            return response;
        });

    };

    var _login = function (loginData) {
        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

        var deferred = $q.defer();

        $http.post('/api/system/account/token', data, { headers: { 'Content-Type': "application/x-www-form-urlencoded" } }).success(function (response) {

            localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName });

            $.signalR.ajaxDefaults.headers = { Authorization: "Bearer " + response.access_token };

            authServiceFactory.authentication.isAuth = true;
            authServiceFactory.authentication.userName = loginData.userName;
            deferred.resolve(response);
        }).error(function (err, status) {
            swal({
                title: "Ошибка",
                text: "Пользователя с таким логином или паролем не существует",
                type: "error",
                timer: 20000,
                showConfirmButton: true
            });
            _logOut();
            deferred.reject(err);
            return err;
        });

        return deferred.promise;

    };

    var _logOut = function () {

        localStorageService.remove('authorizationData');

        authServiceFactory.authentication.isAuth = false;
        authServiceFactory.authentication.userName = "";

    };

    var _fillAuthData = function () {

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            _authentication.isAuth = true;
            _authentication.userName = authData.userName;
        }

    }

    var isAuth = function () {
        return _authentication.isAuth;
    }
    authServiceFactory.saveRegistration = _saveRegistration;
    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.isAuth = isAuth;
    authServiceFactory.fillAuthData = _fillAuthData;


    return authServiceFactory;
}]);

service.factory('authInterceptorService', [
    '$q', '$location', 'localStorageService', function ($q, $location, localStorageService) {

        var authInterceptorServiceFactory = {};

        var _request = function (config) {

            config.headers = config.headers || {};

            var authData = localStorageService.get('authorizationData');
            if (authData) {
                config.headers.Authorization = 'Bearer ' + authData.token;
            }

            return config;
        }

        var _responseError = function (rejection) {
            if (rejection.status === 401) {
                $location.path('/login');
            }
            return $q.reject(rejection);
        }

        authInterceptorServiceFactory.request = _request;
        authInterceptorServiceFactory.responseError = _responseError;

        return authInterceptorServiceFactory;
    }
]).factory('httpInterceptor', ['$q', function ($q) {

    var numLoadings = 0;

    return {
        request: function (config) {

            numLoadings++;

            // Show loader
            if (typeof NProgress != "undefined")
                NProgress.start();
            return config || $q.when(config);

        },
        response: function (response) {

            if ((--numLoadings) === 0) {
                if (typeof NProgress != "undefined")
                    NProgress.done();
            }

            return response || $q.when(response);

        },
        responseError: function (response) {

            if (!(--numLoadings)) {
                if (typeof NProgress != "undefined")
                    NProgress.done();
            }

            return $q.reject(response);
        }
    };
}]).factory('httpStatusInspector', ['$q', function ($q) {

    return {
        request: function (config) {
            return config || $q.when(config);
        },
        response: function (response) {

            if (response.config.method === "PUT" || response.config.method === "POST" || response.config.method === "DELETE")
                if (response.headers('X-ResponseStatus')) {
                    swal({
                        title: 'Успех',
                        text: $.base64.atob(response.headers('X-ResponseStatus'), true),
                        type: "success",
                        timer: 2000,
                        showConfirmButton: false
                    });
                }

            return response || $q.when(response);
        },
        responseError: function (response) {
            var dialog = {
                title: 'Ошибка',
                text: response.data.exceptionMessage,
                type: "error"
            }
            if (response.statusCode === 503) {
                dialog.timer = 2000;
                dialog.showConfirmButton = true;

            }
            if (response.data.message === "Authorization has been denied for this request.") {
                dialog.text = "Вы не авторизированы";
            }
            swal(dialog);
            return $q.reject(response);
        }
    };
}]);

// Declare app level module which depends on views, and components
var app = angular.module("photo.bel", [
    "ngAnimate",
    "ui.bootstrap",
    "ui.calendar",
    "ngRoute",
    "yaMap",
    "LocalStorageModule",
    "photo.bel.service",
    "photo.bel.photostudio",
    "photo.bel.photographer",
    "photo.bel.photorent",
    "photo.bel.model",
    "photo.bel.photoshop",
    "photo.bel.phototechnic",
    "photo.bel.photomonth",
    "photo.bel.account",
    "photo.bel.page",
    "photo.bel.masterclass",
    "photo.bel.admin",
    "photo.bel.directives",
    "photo.bel.admin.directivies",
    "photo.bel.home",
    "photo.bel.widgets",
    "angularMoment",
    "ui.timepicker",
    "internationalPhoneNumber",
    "rzModule",
    'ngImgCrop',
    "photo.bel.admin",
    'photo.bel.admin.services'
]);

app.config(["$routeProvider", function ($routeProvider) {

    $routeProvider.otherwise({ redirectTo: "/" });
}]);

app.factory("$signalr",["localStorageService", function (localStorageService) {
    $.signalR.ajaxDefaults.headers = { Authorization: "Bearer " + localStorageService.get('authorizationData').token };

    $.connection.hub.start().done(function () { });

    return {
        cartHub:$.connection.cartHub
    }
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

app.config(["$httpProvider", function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
    $httpProvider.interceptors.push('httpInterceptor');
    $httpProvider.interceptors.push('httpStatusInspector');
}]);

app.config([
    "$sceDelegateProvider", function ($sceDelegateProvider) {
        $sceDelegateProvider.resourceUrlWhitelist([
            // Allow same origin resource loads.
            "self",
            // Allow loading from our assets domain.  Notice the difference between * and **.
            "http://*.photomsk.by/**"
        ]);
    }
]);
