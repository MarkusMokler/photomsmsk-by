(function () {
    "use strict";

    angular
        .module("photo.bel.admin.services")
        .factory("$cart", cart);

    cart.$inject = ["$http", "$permission", "$rootScope"];

    function cart($http, $permission, $rootScope) {

        var service = {
            add: pushData,
            remove: removeData,
            getShoppingCartList: getShoppingCartList,
            getTotal: getTotal,
            tryBookHalls: tryBookHalls,
            getMore: getMore,
            getUser: getUser,
            getSelected: getSelected,
            setSelected: setSelected,
            resetData: clearShoppingCart,
            endTime: new Date()
        };


        var shoppingCartList = [];
        var more = {};
        var user = {};
        var selected = {};


        return service;

        function setState(state) {
            $rootScope.$broadcast("shoppingState", state);
        }

        function getUser() {
            return user;
        };

        function getSelected() {
            return selected;
        }

        function setSelected(item) {
            selected = item;
        }

        function pushData(item) {
            $http.post("api/v2/event", item).then(function (response) {
                $.each(response.data, function () {
                    shoppingCartList.push(this);
                });
            });

        }

        function getMore() {
            return more;
        }

        function removeData(item) {
            var index = shoppingCartList.indexOf(item);
            shoppingCartList.splice(index, 1);
        }

        function clearShoppingCart() {
            shoppingCartList = [];
            more = {};
            user = {};
            selected = {};
            setState("clear");
        }

        function getShoppingCartList() {
            return shoppingCartList;
        }

        function getTotal() {
            var totalPrice = 0;
            $.each(shoppingCartList, function () {
                totalPrice += this.summ;
            });
            return totalPrice;
        }

        function tryBookHalls() {
            if (more.isMore) {
                $.each(shoppingCartList, function () {
                    this.description = more.Description;
                    this.price = more.Price;
                    var arrr = [];

                    $.each(more.Tags || [], function () {
                        arrr.push(this.text);
                    });

                    this.tags = arrr.join("||");
                });
            }

            var bookingObject = {
                isAdmin: $permission.isAdmin(),
                events: shoppingCartList,
                ready: true
            }
            //$scope.showPhotographerModal = false;
            //if ($scope.isAdmin && selected.originalObject.clientType !== "Photographer" && !$scope.isModalShowed) {
            //    $scope.showPhotographerModal = true;
            //    $scope.isModalShowed = true;
            //    return;
            //}

            if ($permission.isAdmin()) {
                bookingObject.ready = false;

                if (selected) {
                    if (selected.originalObject) {
                        if (selected.originalObject.clientType !== "Photographer" && false) {
                            bookingObject.userInformation = bookingPhotographer.originalObject;
                        } else {
                            bookingObject.userInformation = selected.originalObject;
                        }
                        bookingObject.ready = true;

                    } else {
                        swal({
                            title: "Ошибка",
                            text: "Не выбран пользователь",
                            type: "error",
                            showConfirmButton: true
                        });
                    }

                } else {
                    if (user != null && user.firstName != null && user.lastName != null) {
                        bookingObject.userInformation =
                            {
                                FirstName: user.firstName,
                                LastName: user.lastName,
                                UserPhone: user.userPhone
                            };
                        bookingObject.ready = true;
                    } else {
                        swal({
                            title: "Ошибка",
                            text: "Не введены данные пользователя",
                            type: "error",
                            showConfirmButton: true
                        });
                    }
                }
            }

            if (!bookingObject.ready)
                return;
            $http.post("/api/v2/booking", bookingObject).then(function () {
                clearShoppingCart();
            });
        }
    }
})();