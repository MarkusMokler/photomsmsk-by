angular.module("photo.bel.admin.directivies")
    .directive('bookingOptions', function () {
        return {
            controller: ['$scope', '$http', '$location', '$routeParams', '$rootScope', function ($scope, $http, $location, $routeParams, $rootScope) {
                $scope.setBookingOptions = function() {
                    $http({
                        method: 'PUT',
                        url: '/api/v2/photostudio/?id=' + $scope.item.id,
                        data: $scope.item
                    })
	            .success(function (data) {
	                swal({
	                    title: 'Успех',
	                    text: '',
	                    type: "success",
	                    timer: 2000,
	                    showConfirmButton: false
	                });
	            }).error(function(data) {
	                swal({
	                    title: 'Ошибка',
	                    text: data.message,
	                    type: "error",
	                    timer: 2000,
	                    showConfirmButton: false
	                });
                        });
                }
            }],
            restrict: 'EA',
            templateUrl: '/Scripts/admin/components/shared/RouteEdit/_Booking.html'
        };
    });
