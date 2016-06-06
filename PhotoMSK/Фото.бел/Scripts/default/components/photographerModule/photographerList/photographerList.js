(function () {
    "use strict";

    angular
        .module("photo.bel.photographer")
        .filter("firstLetter", function () {
            return function (input) {
                var result = {};
                angular.forEach(input, function (val, index) {
                    var key = val.city.charAt(0).toUpperCase();
                    result[key] = result[key] || [];
                    result[key].push(val);
                });
                return result;
            };
        })
        .controller("photographerListCtrl", photographerListCtrl);

    photographerListCtrl.$inject = ["$scope", "$http", "$filter"];

    function photographerListCtrl($scope, $http, $filter) {
        $scope.title = "photographerListCtrl";
        var vm = this;
        vm.pagingParams = {};
        $scope.maxSize = 5;
        $scope.currentPage = 1;
        $scope.headCity = "Минск";

        // Default City
        $scope.showPagination = true;
        $scope.city = "Минск";
        $http.get("/api/v2/photographer/?city=" + $scope.city).success(function (response) { setScope($scope, response) });
        $scope.isCityOpen = false;


        function setScope($scope, response) {
            vm.pagingParams = response;
            $scope.page = response;
            $scope.itemsPerPage = vm.pagingParams.pageSize;
            $scope.numPages = vm.pagingParams.pagesCount;
            $scope.totalItems = vm.pagingParams.itemsCount;
        }

        // Change the page
        $scope.pageChanged = function () {
            $http.get("api/v2/photographer/?city=" + $scope.city + "&from=" + ($scope.currentPage - 1) * vm.pagingParams.pageSize).success(function (response) {
                setScope($scope, response);
            });
        };

        // Cities lists
        var belarus = [
            "Бобруйск", "Борисов", "Быхов", "Витебск", "Ветка", "Гомель", "Горки", "Городок", "Дзержинск", "Дубровно", "Добруш",
            "Дрисса", "Калинковичи", "Климовичи", "Костюковичи", "Кричев", "Лепель", "Минск", "Могилёв", "Мозырь", "Мстиславль",
            "Орша", "Осиповичи", "Полоцк", "Речица", "Слуцк"
        ];

        var russia = [
            "Барнаул", "Владивосток", "Волгоград", "Воронеж", "Екатеринбург", "Ижевск", "Иркутск", "Казань", "Краснодар", "Красноярск",
            "Махачкала", "Москва", "Нижний Новгород", "Новосибирск", "Омск", "Пермь", "Ростов-на-Дону", "Самара", "Санкт-Петербург",
            "Саратов", "Тольятти", "Томск", "Тюмень", "Ульяновск", "Уфа", "Хабаровск", "Челябинск", "Ярославль"
        ];

        var ukraine = [
            "Алчевск", "Белая Церковь", "Бердянск", "Винница", "Горловка", "Днепродзержинск", "Днепропетровск", "Донецк", "Евпатория",
            "Житомир", "Запорожье", "Ивано-Франковск", "Каменец-Подольский", "Керчь", "Киев", "Кировоград", "Краматорск", "Кременчуг",
            "Кривой Рог", "Лисичанск", "Луганск", "Луцк", "Львов", "Макеевка", "Мариуполь", "Мелитополь", "Николаев", "Никополь",
            "Одесса", "Павлоград", "Полтава", "Ровно", "Севастополь", "Северодонецк", "Симферополь", "Славянск", "Сумы", "Тернополь",
            "Ужгород", "Харьков", "Херсон", "Хмельницкий", "Черкассы", "Чернигов", "Черновцы"
        ];

        var belarusCities = [];
        var russiaCities = [];
        var ukraineCities = [];
        var otherCities = [];
        var country = "";
        $scope.cities = [];
        var temp = {};

        // CamelCase Converter
        function camelCase(string) {
            return string.charAt(0).toUpperCase() + string.slice(1);
        }

        // GET the cities
        $http.get("api/v2/cities").success(function (response) {
            for (var key in response) {
                if (response.hasOwnProperty(key)) {
                    var value = response[key];
                    key = camelCase(key), -1 !== belarus.indexOf(key) ? (country = "Беларусь", temp = {
                        city: key,
                        count: value,
                        country: country
                    }, belarusCities.push(temp)) : -1 !== russia.indexOf(key) ? (country = "Россия", temp = {
                        city: key,
                        count: value,
                        country: country
                    }, russiaCities.push(temp)) : -1 !== ukraine.indexOf(key) ? (country = "Украина", temp = {
                        city: key,
                        count: value,
                        country: country
                    }, ukraineCities.push(temp)) : (otherCities[key] = value, country = "Другие", temp = {
                        city: key,
                        count: value,
                        country: country
                    }, otherCities.push(temp)), temp = {
                        city: key,
                        count: value,
                        country: country
                    }, $scope.cities.push(temp);
                }
            }
            $scope.cities = $filter("firstLetter")($scope.cities);
        });

        $scope.setBelarus = function () {
            $scope.cities = belarusCities;
            $scope.cities = $filter("firstLetter")($scope.cities);
        };
        $scope.setRussia = function () {
            $scope.cities = russiaCities;
            $scope.cities = $filter("firstLetter")($scope.cities);
        };
        $scope.setUkraine = function () {
            $scope.cities = ukraineCities;
            $scope.cities = $filter("firstLetter")($scope.cities);
        };
        $scope.setOthers = function () {
            $scope.cities = otherCities;
            $scope.cities = $filter("firstLetter")($scope.cities);
        };

        // Change the city
        $scope.changeCity = function (currentCity) {
            $scope.showPagination = true;
            $scope.city = currentCity;
            $scope.headCity = currentCity;
            $http.get("/api/v2/photographer/?city=" + $scope.city).success(function (response) { setScope($scope, response) });
            $scope.isCityOpen = false;
        };

        activate();
        function activate() { }
    }
})();
