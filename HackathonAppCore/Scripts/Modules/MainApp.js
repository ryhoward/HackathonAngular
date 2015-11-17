var demoApp = angular.module('mainApp', ['ngRoute', ]);

demoApp.config(function ($routeProvider) {
    $routeProvider.when('/',
    {
        controller: 'ProductsController',
        templateUrl: 'Partials/Home.html'
    })
    .when('/Products',
    {
        controller: 'ProductsController',
        templateUrl: 'Partials/Products.html'
    })
    .when('/logon',
    {
        controller: 'LogonController',
        templateUrl: 'Partials/Logon.html'
    })
    .when('/me/get', {
        templateUrl: 'Me/Get'
    })
    .otherwise({ redirectTo: '/' });
});