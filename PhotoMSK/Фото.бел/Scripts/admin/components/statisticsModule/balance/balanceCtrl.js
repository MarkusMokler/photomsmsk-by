(function () {
    'use strict';
    angular
        .module('photo.bel.admin.photostudio')
        .controller('balanceCtrl', balanceCtrl)
        .directive('datepicker', function () {
            return {
                restrict: 'A',
                require: 'ngModel',
                link: function (scope, element, attrs, ngModelCtrl) {
                    $(function () {
                        element.datepicker({
                            dateFormat: 'dd/mm/yy',
                            onSelect: function (date) {
                                scope.$apply(function () {
                                    ngModelCtrl.$setViewValue(date);
                                });
                            }
                        });
                    });
                }
            }
        })
        .directive('popover', ["$http", "$routeParams", function ($http, $routeParams) {
            return {
                restrict: "AE",
                templateUrl: 'Scripts/admin/components/statisticsModule/balance/addNew.html',
                scope: {
                    date: "="
                },
                link: function (scope, element, attrs) {
                    scope.line = {
                        type: 2,
                        date: moment(scope.date, "DD MMMM YYYY").toDate()
                    };

                    scope.save = function () {
                        $http({
                            method: "POST",
                            url: "/api/v2/reports/" + $routeParams.id + "/balance",
                            data: scope.line
                        });
                    }

                    scope.$watch('date', function() {
                        scope.line.date = moment(scope.date, "DD MMMM YYYY").toDate();
                    });
                }
            }
        }]);

    balanceCtrl.$inject = ['$scope', '$http', '$routeParams', "@authService"];

    function balanceCtrl($scope, $http, $routeParams, authService) {
        $scope.auth = authService;
        $scope.title = 'reserveCtrl';
        $scope.visibles = {};
        $scope.addNew = function (date) {
            $scope.visibles[date] = true;
        }


        $scope.convertTo = function (arr, key, dayWise) {
            var groups = {};
            for (var i = 0; i < arr.length; i++) {
                if (dayWise) {
                    arr[i]['_' + key] = moment(arr[i][key]).format("DD MMMM YYYY");
                }
                else {
                    arr[i]['_' + key] = arr[i][key].toTimeString();
                }
                groups[arr[i]['_' + key]] = groups[arr[i]['_' + key]] || [];
                groups[arr[i]['_' + key]].push(arr[i]);
            }
            return groups;
        };


        $scope.getDayTotal = function (data) {
            var total = 0;
            $.each(data, function () {
                total += this.total;
            });
            return total;
        }

        var apiLink = "/api/v2/Reports/" + $routeParams.id + "/balance/";

        $http.get(apiLink).then(function (data) {
            $scope.balance = data.data;
            $.each($scope.balance, function () {
                this.groups = $scope.convertTo(this.balanceLines, "date", true);
            });
        });
        $scope.isAdmin = true;
        activate();

        $scope.saveLine = function (line) {
            line.isEdit = false;

            if (typeof (line.id) != "undefined")
                $http.put(apiLink + line.id, line).then();
            else {
                $http.post(apiLink, line);

            }
        }
        $scope.removeLine = function (line) {
            week.balanceLines.splice(line);
        }
        function activate() { }
    }
})();