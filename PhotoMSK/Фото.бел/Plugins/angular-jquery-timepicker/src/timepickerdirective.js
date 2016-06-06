/*global angular */
/*
 Directive for jQuery UI timepicker (http://jonthornton.github.io/jquery-timepicker/)

 */
var m = angular.module('ui.timepicker', []);


m.value('uiTimepickerConfig', {
    'step': 15
});

m.directive('uiTimepicker', ['uiTimepickerConfig', '$parse', '$window', function (uiTimepickerConfig, $parse, $window) {
    var moment = $window.moment;

    var isAMoment = function (date) {
        return moment !== undefined && moment.isMoment(date) && date.isValid();
    };
    var isADateString = function (date) {
        var d = moment(new Date(date));
        return d != null;
    }
    var isDateOrMoment = function (date) {
        return date !== null && (angular.isDate(date) || isAMoment(date) || isADateString(date));
    };

    return {
        restrict: 'A',
        require: 'ngModel',
        scope: {
            ngModel: '=',
            baseDate: '=',
            uiTimepicker: '=',
        },
        priority: 1,
        link: function (scope, element, attrs, ngModel) {
            'use strict';
            var config = angular.copy(uiTimepickerConfig);
            var asMoment = config.asMoment || false;
            var asString = config.asString || true;
            delete config.asMoment;

            ngModel.$render = function () {
                var date = ngModel.$modelValue;
                if (!angular.isDefined(date)) {
                    return;
                }
                if (isAMoment(date)) {
                    date = date.toDate();
                }
                if (typeof data != typeof (new Date())) {
                    ngModel.parsedDate = date = new moment(date).toDate();
                }

                if (!element.is(':focus') && !invalidInput()) {
                    element.timepicker('setTime', date);
                }
            };

            scope.$watch('ngModel', function () {
                ngModel.$render();
            }, true);

            config.appendTo = config.appendTo || element.parent();

            element.timepicker(
                angular.extend(
                    config, scope.uiTimepicker ?
                        scope.uiTimepicker :
                        {}
                )
            );

            var userInput = function () {
                return element.val().trim();
            };

            var invalidInput = function () {
                return userInput() && ngModel.$modelValue === null;
            };

            element.on('$destroy', function () {
                element.timepicker('remove');
            });

            var asDate = function () {
                var baseDate = ngModel.$modelValue ? ngModel.$modelValue : scope.baseDate;
                return isAMoment(baseDate) ? baseDate.toDate() : ngModel.parsedDate == null ? ngModel.parsedDate.toDate() : baseDate;
            };

            var asMomentOrDate = function (date) {
                if (asMoment)
                    return moment(date)
                if (asString)
                    return moment(date).format('YYYY-MM-DDTHH:mm:ss')
                return date;
            };

            if (element.is('input')) {
                ngModel.$parsers.unshift(function (viewValue) {
                    var date = element.timepicker('getTime', asDate());
                    return date ? asMomentOrDate(date) : date;
                });
                ngModel.$validators.time = function (modelValue) {
                    return (!attrs.required && !userInput()) ? true : isDateOrMoment(modelValue);
                };
            } else {
                element.on('changeTime', function () {
                    scope.$evalAsync(function () {
                        var date = element.timepicker('getTime', asDate());
                        if (angular.isString(ngModel.$modelValue))
                            ngModel.$setViewValue(new moment(date).format('YYYY-MM-DDTHH:mm:ss'));
                        else {
                            ngModel.$setViewValue(date);
                        }
                    });
                });
            }
        }
    };
}]);
