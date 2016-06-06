(function() {
    'use strict';

    angular
        .module('photo.bel.admin.directivies')
        .directive('editEventDetails', editEventDetails);

    editEventDetails.$inject = ['$window', '$http'];

    function editEventDetails($window, $http) {

        var directive = {
            link: link,
            restrict: 'EA',
            replace: true,
            scope: {
                event: '=',
                state: '='
            },
            templateUrl: "Scripts/admin/directivies/editEventDetails/editEventDetails.html"
        };

        return directive;

        function link(scope, element, attrs) {

            scope.$watch('event.price', function () {
                    scope.dp = scope.event.price - scope.event.prePayed;
            });

            scope.$watch('event.prePayed', function () {
                scope.dp = scope.event.price - scope.event.prePayed;
            });

            scope.dp = 0;
            scope.updateEvent = function () {
                if (typeof (scope.tags) != "undefined" && scope.tags != "") {
                    console.log(scope.tags);
                    var arr = $.map(scope.tags, function (obj) {
                        return obj.text;
                    });
                    scope.event.tags = arr.join("||");
                }
                $http.put('api/v2/event/' + scope.event.id, scope.event);
            }

            scope.$watch('editEventTab', function(data) {
                switch (data) {
                case 1:
                    $http.get("api/v2/route/" + scope.state.shortcut + "/clients?clientID=" + scope.event.userID).then(function(response) {
                        scope.client = response.data;
                    });
                    break;
                case 2:
                    $http.get('api/v2/event?id=' + scope.event.id + "&activity=true").then(function(response) {
                        scope.activities = response.data;
                    });
                    break;
                case 3:
                    break;
                }
            });

            scope.$watch('event', function(data) {
                if (typeof (scope.event) != "undefined") {
                    scope.dp = scope.event.price - scope.event.prePayed;
                    scope.start = new Date(scope.event.start);
                    scope.end = new Date(scope.event.end);
                    scope.creationTime = moment(scope.creaTime).format('YYYY-DD-MM : HH-mm');;
                    scope.tags = scope.event.tags == null ? "" : scope.event.tags.split("||");
                    $http.get("api/v2/route/" + scope.state.shortcut + "/clients?clientID=" + scope.event.userID).then(function (response) {
                        scope.client = response.data;
                        scope.editEventTab = 1;
                    });
                }
                
            });
        }
    }
})();
