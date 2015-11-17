var demoApp = angular.module('mainApp');

demoApp.factory('cartFactory', function ($q, $http) {
    var factory = {};

    factory.GetCartItems = function () {
        var request = $http({
            url: '/Home/GetCartItems',
            method: "POST",
            dataType: 'jsonp',
            headers: { 'Content-Type': 'text/json' }
        });

        return request;
    }
    return factory;
})