(function () {
    'use strict';
    angular
		.module('photo.bel.admin.photostudio')
        .controller('reviewsCtrl', reviewsCtrl);

    reviewsCtrl.$inject = ['$scope', '$http', '$routeParams', '@authService'];

    function reviewsCtrl($scope, $http, $routeParams, authService) {
        $scope.auth = authService;
        function loadReviews() {
            $http.get("/api/v2/reviews/?shortcut=" + $routeParams.id).then(function (data) {
                $scope.reviews = data.data;
            });
        }
        loadReviews();
        $scope.isAdmin = true;
        activate();
        $scope.getRating = function (likes, dislikes) {
            if (parseInt(dislikes) == 0) {
                return 0;
            }
            return parseInt(likes) / parseInt(dislikes);
        }
        function activate() { }
    }
})();
