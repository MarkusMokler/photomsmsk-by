(function () {
    "use strict";
    angular
		.module("photo.bel.admin.settings")
        .controller("smsModuleCtrl", smsModule);

    smsModule.$inject = ["$scope", "$http", "$routeParams"];

    function smsModule($scope, $http, $routeParam) {

        $scope.isAdmin = true;
        $scope.smsData = {}

        $scope.editorOptions = {
            //mode: { name: "handlebars", base: "htmlmixed" }
            lineNumbers: false,
            lineWrapping: true,
            matchBrackets: true,
            mode: "handlebars"
        };

        //                {{calendarName}} � {{name}} �� {{eventStartDay}}.{{eventStartMonth}} c {{eventStartHour}}:{{eventStartMinute}} �� {{eventEndHour}}:{{eventEndMinute}}

        $scope.testObject = {
            calendarName: "Family",
            name: "Fotostudio",
            eventStartDay: "01",
            eventStartMonth: "05",
            eventStartHour: "19",
            eventStartMinute: "00",
            eventEndHour: "20",
            eventEndMinute: "00"
        }

        $scope.$watch("smsData.bookingHelloText", function () {
            $scope.smsText = $scope.smsData.bookingHelloText + $scope.smsData.bookingLineText + $scope.smsData.bookingEndText;
        });


        $scope.$watch("smsData.bookingLineText", function () {
            $scope.smsText = $scope.smsData.bookingHelloText + $scope.smsData.bookingLineText + $scope.smsData.bookingEndText;
        });


        $scope.$watch("smsData.bookingEndText", function () {
            $scope.smsText = $scope.smsData.bookingHelloText + $scope.smsData.bookingLineText + $scope.smsData.bookingEndText;
        });


        var fetch = function () {
            $http.get("/api/v2/route/" + $routeParam.id + "/smsModule").then(function (data) {
                $scope.smsData = data.data;
            });
        }

        $scope.save = function () {
            $http.post("/api/v2/route/" + $routeParam.id + "/smsModule", $scope.smsData).then(function (data) {
                $scope.smsData = data.data;
            });
        }

        fetch();
        activate();

        function activate() { }
    }

})();
