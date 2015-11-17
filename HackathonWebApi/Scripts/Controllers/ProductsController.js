var demoApp = angular.module('mainApp');

demoApp.controller('ProductsController', ['$scope', 'productsFactory', '$http', '$window', function ($scope, productsFactory, $http, $window) {

    $scope.allProducts = [];
    $scope.filteredProducts = [];
    $scope.productCategories = [];
    $scope.selectedCategory = null;
    $scope.searchWord = '';


    $scope.AddToCart = function (productId, productName, productPrice) {
        var data = JSON.stringify({
            ProductId: productId,
            name: productName,
            price: productPrice
        });
        $http({
            url: '/Home/AddToCart',
            method: "POST",
            data: data,
            dataType: 'jsonp',
            headers: { 'Content-Type': 'application/json; charset=utf-8' }
        }).success(function (data, status, headers, config) {

            if (data == '0') {
                $window.location.href = '/Home/logon';
            }
            else {
                $.each($scope.filteredProducts, function (index, item) {
                    if (item.productId == productId) {
                        item.enabled = false;
                    }
                });
            }

        }).error(function (data, status, headers, config) {
            $scope.status = 'Error with login. Please try again';
        });
    }

    $scope.searchChanged = function () {
        var tempProducts = [];
        $.each($scope.allProducts, function (index, item) {
            if ((item.name.toString().toUpperCase().indexOf($scope.searchWord.toUpperCase()) > -1
                || item.description.toString().toUpperCase().indexOf($scope.searchWord.toUpperCase()) > -1)
                && (item.productCategoryId == $scope.selectedCategory || $scope.selectedCategory == null)) {
                tempProducts.push(item);
            }
        });
        $scope.filteredProducts = tempProducts;
    }

    $scope.categoryChanged = function () {
        var tempProducts = [];
        $.each($scope.allProducts, function (index, item) {
            if (item.productCategoryId == $scope.selectedCategory || $scope.selectedCategory == '') {
                tempProducts.push(item);
            }
        });
        $scope.filteredProducts = tempProducts;
        $scope.searchWord = '';
    }

    productsFactory.getProducts()
    .then(function (items) {
        $.each(items.data, function (index, item) {
            var product = item;
            product.imgPath = '00' + ((item.productId.toString().length > 1) ? '' : '0') + item.productId;
            product.enabled = true;
            $scope.allProducts.push(product);
        });
        $scope.filteredProducts = $scope.allProducts;
    })
    .catch(function (error) {
        $scope.error = error;
    })
    .finally(function () {
        $scope.message = 'All finished';
    })

    productsFactory.getProductCategories()
    .then(function (items) {
        //items.data.push({ productCategoryId: null, name: '' })
        $scope.productCategories.push({ productCategoryId: null, name: 'Select Filter' });
        $.each(items.data, function (index, item) {
            $scope.productCategories.push(item);
        })
        //$scope.productCategories.push(items.data);
    })
    .catch(function (error) {
        $scope.error = error;
    })
    .finally(function () {
        $scope.message = 'All finished';
    })
}]);




//var app = angular.module('mainApp')

//var controllers = {};

//controllers.ProductsController = function ($scope, productsFactory) {
//    //var simpleController = this;
//    $scope.products = [];

//    //productsFactory.getProducts()
//    //.then(function (items) {
//    //    $scope.products = items;
//    //})
//    //.catch(function (error) {
//    //    $scope.error = error;
//    //})
//    //.finally(function () {
//    //    $scope.message = 'All finished';
//    //})
//    //$scope.customers = [];

//    //init();

//    //function init() {
//    //    $scope.customers = simpleFactory.getCustomers();
//    //}


//    //$scope.addCustomer = function () {
//    //    //$scope.addCustomer = function () {
//    //    $scope.customers.push({
//    //        name: $scope.newCustomer.name,
//    //        city: $scope.newCustomer.city
//    //    })
//    //}

//    $scope.message = 'testing123';
//};

//app.controller(controllers);

