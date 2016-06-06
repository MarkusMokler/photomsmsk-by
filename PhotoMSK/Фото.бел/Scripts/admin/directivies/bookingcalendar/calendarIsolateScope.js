(function () {
    "use strict";

    angular
        .module("photo.bel.admin.directivies")
        .directive("adminBookingCalendar", adminBookingCalendar);

    adminBookingCalendar.$inject = ["$window", "$http", "localStorageService", "$cart", "uiCalendarConfig"];

    function adminBookingCalendar($window, $http, localStorageService, $cart, uiCalendarConfig) {
        var directive = {
            link: link,
            restrict: "EA",
            transclude: true,
            scope: {
                calendars: "=",
                shortcut: "=",
                dataurl: "=",
                isAdmin: "=adminside",
                bookingTimeInc: "=",
                minBookingTime: "=",
                allDaySlot: "="
            },

            templateUrl: "/Scripts/admin/directivies/bookingcalendar/index.html"
        };

        function link(scope, element, attrs) {

            var token;
            var authData = localStorageService.get("authorizationData");

            if (authData) {
                token = "Bearer " + authData.token;
            }

            function eventDropped(event) {
                scope.currentSpan = event;
                $http.put("api/v2/event/" + event.id, event);
            };
            function eventResize(event) {
                scope.currentSpan = event;
                $http.put('api/v2/event/' + event.id, event);
            };
            function eventRender(event, items) {
                scope.data = [];
                scope.data.push(event);
                scope.data.push(items);
                if (event.title === "online") {
                    items.find(".fc-title").html('<span class="badge badge--warning">Онлайн</span>');
                }
                if (event.tags != null && typeof (event.tags) != "undefined" && event.tags != "") {
                    var tags = event.tags.split("||");
                    if (scope.isAdmin) {
                        $.each(tags, function () {
                            if (this == "workshop") {
                                items.find(".fc-content")
                                    .append($compile('<span class="badge badge--warning" ng-mouseover="popupWorkshop(data)" ng-mouseleave="hidePopupWorkshop()">' + "workshop" + '</span>')(scope));
                            } else {
                                items.find(".fc-content").append('<span class="badge badge--warning">' + this + '</span>');
                            }
                        });
                    } else {
                        $.each(tags, function () {
                            if (this == "онлайн" || this == "online") {
                                items.find(".fc-content").append('<span class="badge badge--warning">' + "онлайн" + '</span>');
                            }
                            if (this == "workshop") {
                                items.find(".fc-content")
                                    .append('<span class="badge badge--warning" data-ng-mouseover="popupWorkshop(data)" ng-mouseleave="hidePopupWorkshop()">' + "workshop" + '</span>');
                            }
                        });
                    };
                }

            };
            function onEventClick(date, jsEvent) {

                if (scope.isAdmin === true) {

                    $http.get('api/v2/event?id=' + date.id).success(function (data) {
                        $http.get('api/v2/calendar/?id=' + data.id).success(function (data) {
                            scope.calendarName = data;
                        });
                        scope.userInfo = data;
                        scope.timeAgo = jQuery.timeago(scope.userInfo.creaTime);
                        scope.eventPrice = scope.userInfo.price;
                        if (typeof (data.tags) != 'undefined' && data.tags != null) {
                            scope.eventTags = scope.userInfo.tags.split("||");
                        } else {
                            scope.eventTags = null;
                        }

                        if (jsEvent.ctrlKey) {
                            scope.eventClicked = false;
                            scope.state.editBookingEvent = !scope.state.editBookingEvent;
                        } else {
                            scope.state.editBookingEvent = false;
                            scope.popupShowHide();
                        }
                    });
                    scope.eventPopupPositionX = jsEvent.clientX - 120;
                    scope.eventPopupPositionY = jsEvent.clientY - 280;

                }
            };
            function select(start, end, notOpen) {
                var event = {
                    title: "Бронируется",
                    start: start,
                    end: end,
                    color: "red",
                    className: "role-add-booking"
                };
                scope.selectedEvents.push(event);
                scope.currentSpan = event;
                if (!scope.isRentBooking) {
                    if (notOpen !== true) {
                        scope.openModal();
                    }
                } else {
                    scope.rentBookingFunc();
                }
            }

            scope.state = { shortcut: scope.shortcut }

            scope.eventSources = [];

            scope.$on('shoppingState', function (event, data) {
                if (data == "clear") {
                    scope.resetData();
                }
            });

            scope.resetData = function () {
                scope.modalsState = {
                    open: false
                };
                scope.isRentBooking = false;
                scope.item = null;
                scope.selected = null;
                scope.user = {};
                scope.selectedEvents = [];
                scope.currentSpan = null;
                scope.calendarsID = [];
                scope.bookEvents = [];
                scope.eventClicked = false;
                scope.isPenaltySet = false;
                scope.selectedCalendars = [];
                scope.penalty = {};
                scope.uiConfig = {
                    calendar: {
                        defaultView: "agendaWeek",
                        lang: "ru",
                        allDaySlot: scope.allDaySlot,
                        slotDuration: moment.duration(scope.bookingTimeInc || 30, "minutes"),
                        snapDuration: moment.duration(scope.isAdmin === true ? 30 : scope.minBookingTime || 30, "minutes"),
                        selectHelper: true,
                        selectable: true,
                        editable: true,
                        nowIndicator: true,
                        scrollTime: '08:30:00',

                        columnFormat: {
                            month: 'ddd',
                            week: 'ddd DD',
                            day: 'dddd d.M'
                        },

                        header: {
                            right: "month,agendaWeek,agendaDay, today, prev,next"
                        },
                        events: null,
                        select: select,
                        eventDrop: eventDropped,
                        eventResize: eventResize,
                        eventRender: eventRender,
                        eventClick: onEventClick
                    }
                }
                $http({
                    method: "GET",
                    url: "/api/v2/CalendarSearch",
                    params: {
                        ids: scope.calendars
                    }
                }).then(function (response) {
                    scope.fetchedCalendars = response.data;
                    scope.changeCalendar(null);
                    scope.loadCalendarDone = true;
                });
            }

            function rebuildSources() {
                scope.eventSources.splice(0, scope.eventSources.length);

                if (scope.selectedCalendars.length === 0) {
                    $.each(scope.fetchedCalendars, function () {
                        scope.eventSources.push({
                            url: "/api/v2/event?",
                            type: "GET",
                            data: { calendarid: this.calendarID },
                            headers: { Authorization: token }
                        });
                    });
                } else {
                    $.each(scope.selectedCalendars, function () {
                        scope.eventSources.push({
                            url: "/api/v2/event?",
                            type: "GET",
                            data: { calendarid: this },
                            headers: { Authorization: token }
                        });
                    });
                }
            };
            /*on eventClick */

            scope.popupShowHide = function () {
                if (scope.eventClicked) {
                    scope.eventClicked = false;
                } else {
                    scope.eventClicked = true;
                }
            };
            scope.deleteEventWithPenalty = function () {
                scope.showPenaltyModal = false;
                scope.penaltySet();
                scope.isPenaltySet = true;
                scope.deleteEvent();
            };
            scope.deleteEventWithoutPenalty = function () {
                scope.showPenaltyModal = false;
                scope.isPenaltySet = true;
                scope.deleteEvent();
            };
            scope.deleteEvent = function () {
                $http.delete(
                    '/api/v2/event/?id=' + scope.userInfo.id + '&isPenaltySet=' + scope.isPenaltySet
                ).success(function (data) {
                    if (data.isPenalty) {
                        scope.penalty = {};
                        scope.showPenaltyModal = true;
                        scope.penaltyData = data;
                        scope.penalty.Price = data.summ;
                    }
                    scope.resetData();
                });
                scope.eventClicked = false;
            }

            /* Render Tooltip */
            scope.popupWorkshop = function (dataEvent) {
                scope.showWorkShopPopup = true;
                $http.get('api/v2/event?id=' + dataEvent[0].id).success(function (data) {
                    scope.workShopInfo = data;
                });

                scope.workShopPopupX = dataEvent[1].offset().left;
                scope.workShopPopupY = dataEvent[1].offset().top - 25;
            }
            scope.hidePopupWorkshop = function () {
                scope.showWorkShopPopup = false;
            }

            scope.changeCalendar = function (id) {
                scope.rentCalendarID = id;
                if (id !== null && !scope.isRentBooking) {
                    var index = scope.selectedCalendars.indexOf(id);
                    if (index === -1) {
                        scope.selectedCalendars.push(id);
                    } else {
                        scope.selectedCalendars.splice(index, 1);
                    }
                } else {
                    scope.selectedCalendars = [];
                }
                if (scope.isRentBooking) {
                    if (scope.selectedCalendars.indexOf(id) === -1) {
                        scope.selectedCalendars.splice(scope.selectedCalendars.indexOf(id), 1);
                    }
                    scope.selectedCalendars.push(id);
                }
                rebuildSources();

                if (typeof id == "undefined" || id == null) {

                    $.each(scope.selectedEvents, function () {
                        uiCalendarConfig.calendars[scope.calendar].fullCalendar('renderEvent', this, true);
                    });
                } else {
                    $.each($cart.getShoppingCartList(), function (key) {
                        if (this.calendarID === id) {
                            uiCalendarConfig.calendars[scope.calendar].fullCalendar('renderEvent', scope.selectedEvents[key], true);
                        }
                    });
                }
            };

            scope.penaltySet = function () {
                $http.post(
                    '/api/v2/userinformation/' + scope.userInfo.userID + '/penalties', {
                        eventID: scope.userInfo.id,
                        description: scope.penalty.description,
                        price: scope.penalty.price
                    }
                );
            }

            scope.shoppingCart = function () {
                var shoppingCartData = {
                    validate: !scope.isAdmin,
                    start: scope.currentSpan.start,
                    end: scope.currentSpan.end,
                    calendarIDs: scope.calendarsID,
                    id: scope.currentSpan.id
                };
                $cart.add(shoppingCartData);

                scope.shoppingCartClicked = true;
                if ($.UIkit.modal("#modal").isActive()) {
                    $.UIkit.modal("#modal").hide();
                }
                if (scope.calendarsID.length === 0) {
                    uiCalendarConfig.calendars[scope.calendar].fullCalendar("unselect");
                }
            }

            scope.hideModal = function () {
                $.UIkit.modal('#modal').hide();
            }
            /*  modal  */
            scope.openModal = function () {

                if (scope.calendars.length === 1) {
                    scope.addHall(scope.calendars[0].calendarID);
                    scope.shoppingCart();
                    return;
                };
                scope.modalsState.open = true;
            };

            scope.isSelectedButton = function (calendarID) {
                return scope.selectedCalendars.indexOf(calendarID) !== -1;
            }

            scope.$watch("calendars", function () {
                scope.resetData();
            });
        }


        return directive;
    }


})();
