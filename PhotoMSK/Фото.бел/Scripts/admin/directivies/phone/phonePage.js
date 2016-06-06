angular.module("photo.bel.admin.directivies")
    .directive('adminPhonePage', function () {
        return {
            controller: ['$scope', '$http', function ($scope, $http) {
                $scope.validatePhone = function () {
                    if ($scope.phoneIsValidated) {
                        if ($scope.thisMapLocation) {
                            $scope.updateLocation($scope.dataEntityID);
                        }
                        
                    } else {
                        if (!$scope.lock) {
                            $scope.lock = true;
                            $scope.phone.EntityId = $scope.dataEntityID;
                            $scope.showConfirm = true;
                            $http({
                                    method: 'POST',
                                    url: '/api/v2/route/' + $scope.item.shortcut + '/phonecheck',
                                    data: $scope.phone,
                                    headers: { 'Content-Type': 'application/json' }
                                })
                                .success(function(data) {
                                    $scope.message = data.message;
                                });
                        } else {
                            $scope.message = 'Проверочный код был отправлен!';
                        }
                    }
                }
                $scope.confirm = function () {
                    $scope.phone.ConfirmCode = $scope.Code;
                    $http({
                        method: 'PUT',
                        url: '/api/v2/route/' + $scope.item.shortcut + '/phonecheck',
                        data: $scope.phone,
                        headers: { 'Content-Type': 'application/json' }
                    })
                        .success(function (data) {
                            $scope.message = data.message;
                            $scope.phoneIsValidated = true;
                        })
                        .error(function (data) {
                            $scope.message = data.message;
                        });
                };
            }],
            restrict: 'EA',
            templateUrl: '/Scripts/admin/directivies/phone/index.html'
        };
    });