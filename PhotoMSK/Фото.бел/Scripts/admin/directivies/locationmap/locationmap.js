angular.module("photo.bel.admin.directivies")
    .directive('adminLocationMapPage', function () {
        return {
            controller: ['$scope', '$http', function ($scope, $http) {
                var _map;
                $scope.urlPUT = "/api/v2/photostudio?id=";
                $scope.afterMapInit = function (map) {
                    _map = map;
                };
                $scope.thisMapLocation = true;
                $scope.updateLocation = function(id) {
                    $http({
                        method: 'PUT',
                        url: $scope.urlPUT+id,
                        data: $scope.item,
                        headers: { 'Content-Type': 'application/json' }
                    })
	            .success(function (data) {
	                $scope.message = data;
	            });
                }
                $scope.mapClick = function (e) {
                    if (!_map.balloon.isOpen()) {
                        $scope.coords = e.get('coords');
                        _map.balloon.open($scope.coords, {
                            contentHeader: $scope.item.name,
                            contentBody: 'Ваша студия тут',
                            contentFooter: '<sup>Если Вы ошиблись, просто закройте это окно и попробуйте снова</sup>'
                        });
                        $scope.item.longitude = $scope.coords[0];
                        $scope.item.latitude = $scope.coords[1];
                    }
                    else {
                        _map.balloon.close();
                    }
                };
                $scope.handleContext = function (e) {
                    _map.hint.open(e.get('coords'), '');
                };
                $scope.balloonOpen = function () {
                    _map.hint.close();
                }
            }
            ],
            restrict: 'EA',
            templateUrl: '/Scripts/admin/directivies/locationmap/index.html'
        };
    });