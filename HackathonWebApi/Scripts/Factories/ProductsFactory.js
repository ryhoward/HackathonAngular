var demoApp = angular.module('mainApp');

demoApp.factory('productsFactory', function ($q, $http) {
    var factory = {};
    var products = {};
    factory.getProducts = function () {

        var request = $http({
            url: 'http://onboardingexercise2.azurewebsites.net/api/v1/products',
            method: 'GET',
        });

        return request;

        //var deferred = $q.defer();
        //deferred.resolve(customers);
        //return deferred.promise;

        //return customers;
    }

    factory.getProductCategories = function () {
        var request = $http({
            url: 'http://onboardingexercise2.azurewebsites.net/api/v1/productcategories',
            method: 'GET'
        });

        return request;
    }
    return factory;
})