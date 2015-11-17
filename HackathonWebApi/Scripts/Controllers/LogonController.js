var mainApp = angular.module('mainApp');

mainApp.controller('LogonController', ['$scope', '$http', '$window', function ($scope, $http, $window) {

    $scope.Username = '';
    $scope.Password = '';
    $scope.PersistCookie = false;

    $scope.Submit = function () {

        //var data = { 'Username': $scope.Username, 'Password': $scope.Password }
        var data = JSON.stringify({
            Username: $scope.Username, Password: $scope.Password
        });
        $http({
            url: '/Home/Login',
            method: "POST",
            data: data,
            dataType: 'jsonp',
            headers: { 'Content-Type': 'application/json; charset=utf-8' }//'text/json' }
        }).success(function (data, status, headers, config) {
            if (data == 'True') {
                $scope.status = '';
                $scope.Username = '';
                $scope.Password = '';
                $window.location.href = '/Home/Index';
            }
            else if (data == 'False') {
                $scope.status = 'Invalid credentials. Please try again';
            }
        }).error(function (data, status, headers, config) {
            $scope.status = 'Error with login. Please try again';
        });
    }

}]);