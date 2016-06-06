(function () {
    'use strict';

    angular
        .module('photo.bel.photostudio')
        .controller('mapCtrl', mapCtrl);

    mapCtrl.$inject = ['$scope', '$http', '$timeout'];

    function mapCtrl($scope, $http) {
        $scope.mapWidth = window.screen.availWidth;
        $scope.mapHeight = window.screen.availHeight;
        $scope.title = 'mapCtrl';
        $scope.modeViewMap = true;
        var _map;
        $scope.afterMapInit = function (map) {
            _map = map;
        };
        $scope.setOnMap = function(item) {
            _map.panTo([item.longitude, item.latitude], { flying: true });
        }

        $scope.hoverOnMap = function (item) {
            var polygonLayout = ymaps.templateLayoutFactory.createClass('<i class="button">!</i>');
            var polygonPlacemark = new ymaps.Placemark(
                [item.longitude, item.latitude], {
                    hintContent: ''
                }, {
                    iconLayout: polygonLayout,
                    iconShape: {
                        type: 'Polygon',
                        coordinates: [[[-28, -76], [28, -76], [28, -20], [12, -20], [0, -4], [-12, -20], [-28, -20]]]
                    }
                }
            );
            _map.geoObjects.add(polygonPlacemark);
        }
        $scope.leaveOnMap = function () {
            _map.geoObjects.removeAll();
            $.each($scope.data, function () {
                if (this.longitude != 0 && this.latitude != 0 && typeof this.longitude != 'undefined' && this.latitude != null) {
                    $scope.geoObjects.push({
                        geometry: {
                            type: 'Point',
                            coordinates: [this.longitude, this.latitude]
                        },
                        properties: {
                            balloonContent: this.adress,
                            iconContent: this.name,
                            hintContent: this.adress
                        }
                    });
                }
            });
        }
        $http.get('api/v2/map').then(function (response) {
            $scope.data = response.data;
            $scope.geoObjects = [];

            $.each($scope.data, function () {
                if (this.longitude != 0 && this.latitude != 0 && typeof this.longitude != 'undefined' && this.latitude != null) {
                    $scope.geoObjects.push({
                        geometry: {
                            type: 'Point',
                            coordinates: [this.longitude, this.latitude]
                        },
                        properties: {
                            balloonContent: this.adress,
                            iconContent: this.name,
                            hintContent: this.adress
                        }
                    });
                }
            });
        });
        $http.get('/api/v2/photostudio').success(function (response) {
            $scope.page = response;
        });
        $scope.over = {
            build: function () {
                var ZoomLayout = templateLayoutFactory.get('zoomTemplate');
                ZoomLayout.superclass.build.call(this);
                this.zoomInCallback = ymaps.util.bind(this.zoomIn, this);
                this.zoomOutCallback = ymaps.util.bind(this.zoomOut, this);
                angular.element(document.getElementById('zoom-in')).bind('click', this.zoomInCallback);
                angular.element(document.getElementById('zoom-out')).bind('click', this.zoomOutCallback);
            },

            clear: function () {
                angular.element(document.getElementById('zoom-in')).unbind('click', this.zoomInCallback);
                angular.element(document.getElementById('zoom-out')).unbind('click', this.zoomOutCallback);

                var ZoomLayout = templateLayoutFactory.get('zoomTemplate');
                ZoomLayout.superclass.clear.call(this);
            },

            zoomIn: function () {
                var map = this.getData().control.getMap();
                this.events.fire('zoomchange', {
                    oldZoom: map.getZoom(),
                    newZoom: map.getZoom() + 1
                });
            },

            zoomOut: function () {
                var map = this.getData().control.getMap();
                this.events.fire('zoomchange', {
                    oldZoom: map.getZoom(),
                    newZoom: map.getZoom() - 1
                });
            }
        };
        activate();

        function activate() { }
    }
})();
