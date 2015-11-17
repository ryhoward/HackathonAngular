var demoApp = angular.module('mainApp');

demoApp.controller('LogonController', ['$scope', '$http', function ($scope, $http) {

    $scope.UserName = 'wut';
    $scope.Password = '';
    $scope.PersistCookie = false;

    $scope.Submit = function () {
        //$http({
        //    url: 'http://localhost/api/membership',
        //    method: 'POST',
        //    dataType: 'jsonp',
        //    data: {
        //        'ID': 27,
        //        'First': $scope.UserName,
        //        'Last': $scope.Password
        //    },
        //    headers: { 'Content-Type': 'text/json' }
        //}).then(function (items) {
        //    alert('hell yeah!');
        //})
        //.catch(function (error) {
        //    $scope.error = error;
        //})

        var data = { 'ID': 27, 'First': $scope.UserName, 'Last': $scope.Password }
        //$http.post('http://localhost/api/membership', data).success(function (data, status, headers) {
        //    alert('hell yeah');
        //}).error(function (data, status, headers, config) {
        //    alert('hell no');
        //});

        $http({
            url: 'http://localhost/api/membership',
            method: "POST",
            data: data,
            dataType: 'jsonp',
            //headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            headers: { 'Content-Type': 'text/json' }
        }).success(function (data, status, headers, config) {
            $scope.persons = data; // assign  $scope.persons here as promise is resolved here 
        }).error(function (data, status, headers, config) {
            $scope.status = status;
        });
    }

}]);