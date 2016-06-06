(function () {
    'use strict';
    angular
		.module('photo.bel.admin.photostudio')
        .controller('photostudioDashboardCtrl', photostudioDashboardCtrl);

    photostudioDashboardCtrl.$inject = ['$scope', '$http', '$routeParams', '@authService'];

    function photostudioDashboardCtrl($scope, $http, $routeParams, authService) {
        $scope.auth = authService;
        $scope.title = 'reserveCtrl';
        $scope.balance = {};
        $scope.shortcut = $routeParams.id;
        $scope.isAdmin = true;
        $scope.showPayedEventsModal = false;
        $scope.showMissedEventsModal = false;
        $scope.justDelEvent = true;
        $scope.viewEvent = {};
        activate();

        $scope.edit = function (value) {
            value.copy = angular.copy(value);
            value.isEdit = true;
        }
        $scope.cancel = function (value) {
            $.each(value.copy, function (key, ovalue) {
                value[key] = ovalue;
            });
            value.isEdit = false;
            value.copy = null;

        }
        $scope.approive = function (value) {
            value.copy = null;
            value.isEdit = false;
            value.isApproive = true;
        }

        $scope.showCloseDayModal = function() {
            $scope.showCloseDay = true;
            $scope.allSumm = $scope.summPayed + $scope.summDebit;
            $scope.balance.total = $scope.summPayed + $scope.summDebit;
        }

        $scope.closeDay = function () {
            $scope.balance.type = 1;
            $http({
                method: "POST",
                url: "/api/v2/reports/" + $scope.shortcut + "/balance",
                data: $scope.balance
            });
        }

        $scope.addReview = function (userID) {
            $scope.review.routeShortcut = $routeParams.id;
            if ($scope.review.title === "" || $scope.review.description === "" || $scope.review.badComment === "" || $scope.review.goodComment === "") {
                swal({
                    title: 'Ошибка',
                    text: "Заполните поля",
                    type: "error",
                    timer: 2000,
                    showConfirmButton: false
                });
            } else {
                $http({
                    method: 'POST',
                    url: "/api/v2/Reviews/?userID=" + userID,
                    data: $scope.review
                })
                    .success(function () {
                        swal({
                            title: 'Успех',
                            text: '',
                            type: "success",
                            timer: 2000,
                            showConfirmButton: false
                        });
                    }).error(function (data) {
                        swal({
                            title: 'Ошибка',
                            text: data.message,
                            type: "error",
                            timer: 2000,
                            showConfirmButton: false
                        });
                    });
                $scope.showPayedEventsModal = false;
            }
        };

        $scope.fetch = function() {
            var dd = new Date();
            var today = moment([dd.getFullYear(), dd.getMonth(), dd.getDate()]);
            var tomorrow = moment([dd.getFullYear(), dd.getMonth(), dd.getDate()]);
            tomorrow.add(1, "d");

            $scope.payedEvents = [];
            $scope.debitEvents = [];
            $scope.missedEvents = [];
            $scope.activityEvents = [];

            $scope.summPayed = 0;
            $scope.summDebit = 0;
            $scope.summMissed = 0;

            $http.get("/api/v2/event/?shortcut=" + $scope.shortcut + "&start=" + today.format("YYYY-MM-DDTHH:mm:ss") + "&end=" + tomorrow.format("YYYY-MM-DDTHH:mm:ss")).then(function(response) {
                $.each(response.data, function() {
                    switch (this.eventState) {
                    case "PayByCash":
                        $scope.payedEvents.push(this);
                        $scope.summPayed += this.price;
                        break;
                    case "PayByCard":
                        $scope.debitEvents.push(this);
                        $scope.summDebit += this.price;
                        break;
                    case "Missed":
                        $scope.missedEvents.push(this);
                        $scope.summMissed += this.price;
                        break;

                    default:
                        $scope.activityEvents.push(this);
                        break;
                    }
                });
            });
        };

        $scope.$watch("payedEvents", function () {
            $scope.summPayed = 0;
            $.each($scope.payedEvents, function() {
                $scope.summPayed += this.price;
            });
        });

        $scope.$watch("debitEvents", function () {
            $scope.summDebit = 0;
            $.each($scope.debitEvents, function () {
                $scope.summDebit += this.price;
            });
        });

        $scope.$watch("missedEvents", function () {
            $scope.summMissed = 0;
            $.each($scope.summMissed, function () {
                $scope.summMissed += this.price;
            });
        });

        $scope.getLastPrice = function(p1, p2) {
            return p1 - p2;
        }

        $scope.getDetails = function(userId, eventId) {
            $http.get("/api/v2/userinformation/?id=" + userId).then(function(data) {
                $scope.userData = data.data;
            });
            $http.get("/api/v2/event/?id=" + eventId).then(function(data) {
                $scope.eventData = data.data;
            });
        };

        $scope.updateEvent = function (event) {
            event.price = parseInt(event.price);
            $http.put('api/v2/event/' + event.id, event);
            $scope.fetch();
        };

        $scope.setPenalty = function() {
            $http.post('/api/v2/userinformation/' + $scope.viewEvent.userID + '/penalties', {
                eventID: $scope.viewEvent.id,
                description: $scope.penalty.Description,
                price: $scope.penalty.Price
            });
        };

        $scope.modalCloseModals = function () {
            $scope.showCloseDay = false;
            $scope.showPayedEventsModal = false;
            $scope.showMissedEventsModal = false;
        };

        $scope.changeState = function(e, ui, state) {
            $scope.getDetails(ui.item.sortable.model.userID, ui.item.sortable.model.id);
            $scope.viewEvent = ui.item.sortable.model;
            $scope.viewEvent.EventState = state;
            $scope.updateEvent($scope.viewEvent);
        }

        $scope.options1 = {
            connectWith: ".ng-eventzone",
            placeholder: "placeholder",
            receive: function (e, ui) {
                $scope.changeState(e, ui, 'NotSet');
            }
        }

        $scope.options2 = {
            connectWith: ".ng-eventzone",
            placeholder: "placeholder",
            receive: function (e, ui) {
                $scope.changeState(e, ui, 'PayByCash');
                $scope.showPayedEventsModal = true;
            }
        }

        $scope.options3 = {
            connectWith: ".ng-eventzone",
            placeholder: "placeholder",
            receive: function (e, ui) {
                $scope.changeState(e, ui, 'PayByCard');
                $scope.showPayedEventsModal = true;
            }
        }

        $scope.options4 = {
            connectWith: ".ng-eventzone",
            placeholder: "placeholder",
            receive: function (e, ui) {
                $scope.changeState(e, ui, 'Missed');
                $scope.showMissedEventsModal = true;
            }
        }

        $scope.closePenaltyModal = function () {
            $scope.setPenalty();
            $scope.showMissedEventsModal = false;
        }

        $scope.fetch();

        function activate() { }
    }
})();