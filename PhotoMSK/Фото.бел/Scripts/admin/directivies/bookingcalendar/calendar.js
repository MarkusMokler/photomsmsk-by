angular.module("photo.bel.admin.directivies")
    .directive('adminBookingCalendarOld', ['localStorageService', function (localStorageService) {
        return {
            controller: [
                '$scope', '$routeParams', '$http', '$compile', "$cart", function ($scope, $routeParams, $http, $compile, $cart) {

                    var token;
                    var authData = localStorageService.get('authorizationData');

                    if (authData) {
                        token = "Bearer " + authData.token;
                    }

                    $scope.state = {
                        shortcut: $routeParams.id
                    }

                    $scope.resetData = function () {
                        $scope.isRentBooking = false;
                        $scope.item = null;
                        $scope.user = {};
                        $scope.selectedEvents = [];
                        $scope.currentSpan = null;
                        $scope.calendarsID = [];
                        $scope.bookEvents = [];
                        $scope.eventClicked = false;
                        $scope.isPenaltySet = false;
                        $scope.selectedCalendars = [];
                        if (typeof ($scope.dataUrl) != 'undefined' && $scope.dataUrl != null) {
                            $http.get($scope.dataUrl).then(function (data) {
                                $scope.item = data.data;
                                if ("elements" in data.data) {
                                    $scope.isRentBooking = true;
                                    $scope.item = data.data;
                                    $scope.setData($scope.item.elements[0]);
                                }
                                if ("halls" in data.data) {
                                    $scope.isRentBooking = false;
                                    $scope.item = data.data;
                                    $scope.changeCalendar(null);
                                }
                                if ($scope.isAdmin === true) {
                                    $scope.snap = 30;
                                } else {
                                    $scope.snap = $scope.item.minBookingTime;
                                }
                                if ($scope.item.routeType === "Photostudio") {
                                    $scope.isAllDayCalendar = false;
                                } else {
                                    $scope.isAllDayCalendar = true;
                                }
                                $scope.uiConfig = {
                                    calendar: {
                                        defaultView: "agendaWeek",
                                        lang: "ru",
                                        allDaySlot: $scope.isAllDayCalendar,
                                        slotDuration: moment.duration($scope.item.bookingTimeInc || 30, 'minutes'),
                                        snapDuration: moment.duration($scope.snap || 30, 'minutes'),
                                        selectHelper: true,
                                        selectable: true,
                                        editable: true,
                                        nowIndicator: true,
                                        header: {
                                            right: "today,prev,next"
                                        },
                                        events: null,
                                        select: function (start, end, notOpen) {
                                            var event = {
                                                title: "Бронируется",
                                                start: start,
                                                end: end,
                                                color: "red",
                                                className: "role-add-booking"
                                            };
                                            $scope.selectedEvents.push(event);
                                            $scope.currentSpan = event;
                                            $('#calendar').fullCalendar('renderEvent', event, true);
                                            if (!$scope.isRentBooking) {
                                                if (notOpen != true) {
                                                    $scope.openModal();
                                                }
                                            } else {
                                                $scope.rentBookingFunc();
                                            }

                                        },
                                        eventDrop: $scope.eventDropped,
                                        eventResize: $scope.eventResize,
                                        eventRender: $scope.eventRender,
                                        eventClick: $scope.onEventClick
                                    }
                                }
                                $scope.loadCalendarDone = true;
                            });
                        } else {
                            $scope.isRentBooking = false;
                            $scope.item = {};
                            $scope.item.halls = [];
                            $.each($scope.calendars, function () {
                                $scope.item.halls.push(this);
                            });
                            $scope.changeCalendar(null);
                            if ($scope.isAdmin === true) {
                                $scope.snap = 30;
                            } else {
                                $scope.snap = $scope.item.minBookingTime;
                            }
                            if ($scope.item.routeType === "Photostudio") {
                                $scope.isAllDayCalendar = false;
                            } else {
                                $scope.isAllDayCalendar = true;
                            }
                            $scope.uiConfig = {
                                calendar: {
                                    defaultView: "agendaWeek",
                                    lang: "ru",
                                    allDaySlot: false,
                                    slotDuration: moment.duration($scope.item.bookingTimeInc || 30, 'minutes'),
                                    snapDuration: moment.duration($scope.snap || 30, 'minutes'),
                                    selectHelper: true,
                                    selectable: true,
                                    editable: true,
                                    nowIndicator: true,
                                    header: {
                                        right: "today,prev,next"
                                    },
                                    events: null,
                                    select: function (start, end, notOpen) {
                                        var event = {
                                            title: "Бронируется",
                                            start: start,
                                            end: end,
                                            color: "red",
                                            className: "role-add-booking"
                                        };
                                        $scope.selectedEvents.push(event);
                                        $scope.currentSpan = event;
                                        $('#calendar').fullCalendar('renderEvent', event, true);
                                        if (!$scope.isRentBooking) {
                                            if (notOpen != true) {
                                                $scope.openModal();
                                            }
                                        } else {
                                            $scope.rentBookingFunc();
                                        }

                                    },
                                    eventDrop: $scope.eventDropped,
                                    eventResize: $scope.eventResize,
                                    eventRender: $scope.eventRender,
                                    eventClick: $scope.onEventClick
                                }
                            }
                            $scope.loadCalendarDone = true;
                        }
                    }
                    $scope.eventDropped = function (event) {
                        $scope.currentSpan = event;
                        $http.put('api/v2/event/' + event.id, event);
                    };
                    $scope.eventResize = function (event) {
                        $scope.currentSpan = event;
                        $http.put('api/v2/event/' + event.id, event).then(function (data) {
                            swal({
                                title: 'Успех',
                                text: 'Вы успешно забронировали студию',
                                type: "success",
                                timer: 2000,
                                showConfirmButton: false
                            });
                        });
                    };
                    var setEventSources = function () {
                        $('#calendar').fullCalendar('removeEvents');
                        var sourcesArr = [];
                        if ($scope.selectedCalendars.length === 0) {
                            $.each($scope.item.halls, function () {
                                sourcesArr.push({
                                    url: '/api/v2/event?',
                                    type: 'GET',
                                    data: {
                                        calendarid: this.calendarID
                                    },
                                    headers: {
                                        Authorization: token
                                    }
                                });
                            });
                        } else {
                            $.each($scope.selectedCalendars, function () {
                                sourcesArr.push({
                                    url: '/api/v2/event?',
                                    type: 'GET',
                                    data: {
                                        calendarid: this
                                    },
                                    headers: {
                                        Authorization: token
                                    }
                                });
                            });
                        }
                        return sourcesArr;
                    };
                    /*on eventClick */
                    $scope.onEventClick = function (date, jsEvent) {

                        if ($scope.isAdmin === true) {

                            $http.get('api/v2/event?id=' + date.id).success(function (data) {
                                $scope.userInfo = data;
                                $scope.timeAgo = jQuery.timeago($scope.userInfo.creaTime);
                                $scope.eventPrice = $scope.userInfo.price;
                                if (typeof (data.tags) != 'undefined' && data.tags != null) {
                                    $scope.eventTags = $scope.userInfo.tags.split("||");
                                } else {
                                    $scope.eventTags = null;
                                }

                                if (jsEvent.ctrlKey) {
                                    $scope.eventClicked = false;
                                    $scope.state.editBookingEvent = !$scope.state.editBookingEvent;
                                } else {
                                    $scope.state.editBookingEvent = false;
                                    $scope.popupShowHide();
                                }
                            });
                            $scope.eventPopupPositionX = jsEvent.clientX - 120;
                            $scope.eventPopupPositionY = jsEvent.clientY - 280;

                        }
                    };
                    $scope.popupShowHide = function () {
                        if ($scope.eventClicked) {
                            $scope.eventClicked = false;
                        } else {
                            $scope.eventClicked = true;
                        }
                    };

                    $scope.deleteEventWithPenalty = function () {
                        $scope.showPenaltyModal = false;
                        $scope.penaltySet();
                        $scope.isPenaltySet = true;
                        $scope.deleteEvent();
                    };

                    $scope.deleteEventWithoutPenalty = function () {
                        $scope.showPenaltyModal = false;
                        $scope.isPenaltySet = true;
                        $scope.deleteEvent();
                    };

                    $scope.deleteEvent = function () {
                        $http.delete(
                            "/api/v2/event/?id=" + $scope.userInfo.id + "&isPenaltySet=" + $scope.isPenaltySet
                        ).success(function (data) {
                            if (data.isPenalty) {
                                $scope.penalty = {};
                                $scope.showPenaltyModal = true;
                                $scope.penaltyData = data;
                                $scope.penalty.Price = data.summ;
                            } else {
                                swal({
                                    title: "Успех",
                                    text: data,
                                    type: "success",
                                    timer: 2000,
                                    showConfirmButton: false
                                });
                            }
                            $scope.resetData();
                        });
                        $scope.eventClicked = false;
                    }

                    /* Render Tooltip */
                    $scope.eventRender = function (event, element) {
                        $scope.data = [];
                        $scope.data.push(event);
                        $scope.data.push(element);
                        if (event.title === 'online') {
                            element.find('.fc-title').html('<span class="badge badge--warning">Онлайн</span>');
                        }
                        if (event.tags != null && typeof (event.tags) != "undefined" && event.tags != '') {
                            var tags = event.tags.split("||");
                            if ($scope.isAdmin) {
                                $.each(tags, function () {
                                    if (this == "workshop") {
                                        element.find(".fc-content")
                                            .append($compile('<span class="badge badge--warning" ng-mouseover="popupWorkshop(data)" ng-mouseleave="hidePopupWorkshop()">' + "workshop" + '</span>')($scope));
                                    } else {
                                        element.find(".fc-content").append('<span class="badge badge--warning">' + this + '</span>');
                                    }
                                });
                            } else {
                                $.each(tags, function () {
                                    if (this == "online") {
                                        element.find(".fc-content").append('<span class="badge badge--warning">' + "онлайн" + '</span>');
                                    }
                                    if (this == "workshop") {
                                        element.find(".fc-content")
                                            .append('<span class="badge badge--warning" data-ng-mouseover="popupWorkshop(data)" ng-mouseleave="hidePopupWorkshop()">' + "workshop" + '</span>');
                                    }
                                });
                            };
                        }

                    };

                    $scope.popupWorkshop = function (dataEvent) {
                        $scope.showWorkShopPopup = true;
                        $http.get('api/v2/event?id=' + dataEvent[0].id).success(function (data) {
                            $scope.workShopInfo = data;
                        });

                        $scope.workShopPopupX = dataEvent[1].offset().left;
                        $scope.workShopPopupY = dataEvent[1].offset().top - 25;
                    }

                    $scope.hidePopupWorkshop = function () {
                        $scope.showWorkShopPopup = false;
                    }

                    $scope.changeCalendar = function (id) {
                        $scope.rentCalendarID = id;
                        if (id !== null && !$scope.isRentBooking) {
                            var index = $scope.selectedCalendars.indexOf(id);
                            if (index === -1) {
                                $scope.selectedCalendars.push(id);
                            } else {
                                $scope.selectedCalendars.splice(index, 1);
                            }
                        } else {
                            $scope.selectedCalendars = [];
                        }
                        if ($scope.isRentBooking) {
                            if ($scope.selectedCalendars.indexOf(id) === -1) {
                                $scope.selectedCalendars.splice($scope.selectedCalendars.indexOf(id), 1);
                            }
                            $scope.selectedCalendars.push(id);
                        }
                        $.each($scope.eventSources, function () {
                            $("#calendar").fullCalendar("removeEventSource", this);
                        });
                        if (typeof id == "undefined" || id == null) {
                            $scope.eventSources = setEventSources();
                            $.each($scope.selectedEvents, function () {
                                $("#calendar").fullCalendar("renderEvent", this, true);
                            });
                        } else {
                            $scope.eventSources = setEventSources();
                            $.each($scope.shoppingCartList, function (key) {
                                if (this.calendarID == id) {
                                    $("#calendar").fullCalendar("renderEvent", $scope.selectedEvents[key], true);
                                }
                            });
                        }
                        $.each($scope.eventSources, function () {
                            $('#calendar').fullCalendar('addEventSource', this);
                        });
                    };
                    $scope.eventSources = [];
                    $('#modal').on({
                        'hide.uk.modal': function () {
                            if ($scope.calendarsID.length === 0 || !$scope.shoppingCartClicked)
                                $scope.resetData();
                        }
                    });

                    $scope.penaltySet = function () {
                        $http.post(
                            '/api/v2/userinformation/' + $scope.userInfo.userID + '/penalties', {
                                eventID: $scope.userInfo.id,
                                description: $scope.penalty.Description,
                                price: $scope.penalty.Price
                            }
                        );
                    }

                    $scope.hideModal = function () {
                        $.UIkit.modal('#modal').hide();
                    }
                    $scope.daysOfWeek = ['Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб', 'Вс'];
                    $scope.months = ['Янв', 'Фев', 'Мар', 'Апр', 'Мая', 'Июня', 'Июля', 'Авг', 'Сен', 'Окт', 'Ноя', 'Дек'];
                    /*  modal  */
                    $scope.openModal = function () {
                        if ($scope.item.halls.length === 1) {
                            $scope.addHall($scope.item.halls[0].calendarID);
                            $scope.shoppingCart();
                            return;
                        };
                        $scope.modalDayOfWeekStart = $scope.currentSpan.start.day();
                        if ($scope.modalDayOfWeekStart === 0) {
                            $scope.modalDayOfWeekStart = 7;
                        }
                        $scope.modalDayOfWeekEnd = $scope.currentSpan.end.day();
                        if ($scope.modalDayOfWeekEnd === 0) {
                            $scope.modalDayOfWeekEnd = 7;
                        }
                        $scope.modalViewStartDay = parseInt(moment($scope.currentSpan.start).format('DD'));
                        $scope.modalViewEndDay = parseInt(moment($scope.currentSpan.end).format('DD'));
                        $scope.modalViewStartMonth = parseInt(moment($scope.currentSpan.start).format('MM'));
                        $scope.modalViewEndMonth = parseInt(moment($scope.currentSpan.end).format('MM'));
                        $scope.modalViewStartHour = moment($scope.currentSpan.start).format('H');
                        $scope.modalViewEndHour = moment($scope.currentSpan.end).format('H');
                        $scope.modalViewStartMinutes = moment($scope.currentSpan.start).format('mm');
                        $scope.modalViewEndMinutes = moment($scope.currentSpan.end).format('mm');
                        $scope.calendarsID = [];
                        $http.get("/api/v2/Calendar?routeID=" + $scope.item.id + "&start=" + moment($scope.currentSpan.start).format('YYYY-MM-DD HH:mm') + "&end=" + moment($scope.currentSpan.end).format('YYYY-MM-DD HH:mm'))
                            .then(function (data) {
                                $scope.hallPricesDay = data.data[0];
                                $scope.friendlyRoutes = data.data.splice(1, data.data.length - 1) || [];
                            });
                        if ($.UIkit.modal('#modal').isActive()) {
                            $.UIkit.modal('#modal').hide();
                        }
                        $.UIkit.modal('#modal').show();
                    };
                    $scope.isSelected = function (calendarID) {
                        return $scope.calendarsID.indexOf(calendarID) !== -1;
                    }
                    $scope.isSelectedButton = function (calendarID) {
                        return $scope.selectedCalendars.indexOf(calendarID) !== -1;
                    }
                    $scope.addHall = function (calendarID) {

                        var index = $scope.calendarsID.indexOf(calendarID);

                        if (index === -1) {
                            $scope.calendarsID.push(calendarID);
                        } else {
                            $scope.calendarsID.splice(index, 1);
                        }
                    }
                    $scope.resetData();
                }
            ],
            restrict: "EA",
            templateUrl: "/Scripts/admin/directivies/bookingcalendar/index.html"
        };
    }]);
