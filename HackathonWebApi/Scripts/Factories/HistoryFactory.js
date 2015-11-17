var demoApp = angular.module('mainApp');

demoApp.factory('historyFactory', function ($q, $http) {
    var factory = {};

    factory.GetHistoryItems = function () {
        var request = $http({
            url: '/Home/GetPurchaseHistoryItems',
            method: "POST",
            dataType: 'jsonp',
            headers: { 'Content-Type': 'text/json' }
        });

        return request;
    }
    return factory;
})