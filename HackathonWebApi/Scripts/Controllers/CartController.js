var mainApp = angular.module('mainApp');

mainApp.controller('CartController', ['$scope', '$http', 'cartFactory', '$window', function ($scope, $http, cartFactory, $window) {
    $scope.CartItems = [];

    cartFactory.GetCartItems()
        .then(function (items) {
            var displayItems = [];
            $.each(items.data, function (index, item) {
                item.imgPath = '00' + ((item.ProductId.toString().length > 1) ? '' : '0') + item.ProductId;
                displayItems.push(item);
            });
            $scope.CartItems = displayItems;
        })
        .catch(function (error) {
            $scope.error = error;
        })

    $scope.RemoveFromCart = function (productId) {
        $.each($scope.CartItems, function (index, item) {
            if (item.ProductId == productId) {

                var productData = JSON.stringify({
                    productId: productId
                });

                var index = $scope.CartItems.indexOf(item);
                $scope.CartItems.splice(index, 1);

                $http({
                    url: '/Home/RemoveFromCart',
                    method: "POST",
                    dataType: 'jsonp',
                    headers: { 'Content-Type': 'application/json; charset=utf-8' },
                    data: productData
                }).catch(function (error) {
                    $scope.error = error;
                })
            }
        })
    }

    $scope.ItemCountUpdate = function (productId, count) {

        var productData = JSON.stringify({
            productId: productId,
            count: count,
        });

        $http({
            url: '/Home/UpdateCartItem',
            method: "POST",
            dataType: 'jsonp',
            headers: { 'Content-Type': 'application/json; charset=utf-8' },
            data: productData
        }).catch(function (error) {
            $scope.error = error;
        })
    }
}]);