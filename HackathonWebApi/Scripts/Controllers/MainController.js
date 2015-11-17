var mainApp = angular.module('mainApp');

mainApp.controller('MainController', ['$scope', '$http', function ($scope, $http) {
    $scope.IsLoggedOn = function () {
        //return false;

        $http({
            url: '/Home/IsUserAuthenticated',
            method: "GET",
            dataType: 'jsonp',
            headers: { 'Content-Type': 'text/json' }
        }).success(function (data, status, headers, config) {
            if (data == true) {
                $scope.status = '';
                $scope.Username = '';
                $scope.Password = '';
                $location.path('/');
            }
            else if (data == false) {
                $scope.status = 'Error Validating authentication';
            }
        }).error(function (data, status, headers, config) {
            $scope.status = 'Error with login. Please try again';
        });
    }
    $scope.Username = function () {
        return 'johnny';
    }
}]);