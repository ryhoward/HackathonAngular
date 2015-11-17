var mainApp = angular.module('mainApp');

mainApp.controller('CheckoutController', ['$scope', '$http', 'cartFactory', function ($scope, $http, cartFactory) {
    $scope.CartItems = [];
    $scope.Name = '';
    $scope.Phone = '';
    $scope.Email = '';

    $scope.BillingStreet = '';
    $scope.BillingStree2 = '';
    $scope.BillingCity = '';
    $scope.BillingState = '';
    $scope.BillingZip = '';

    $scope.ShippingStreet = '';
    $scope.ShippingStree2 = '';
    $scope.ShippingCity = '';
    $scope.ShippingState = '';
    $scope.ShippingZip = ''
    
    //Possible Checkout steps: 1) Verify(ViewOrder), 2) User Details Entry(UserDataEntry), 3) Confirmation (OrderConfirmation)
    $scope.CheckoutStep = '';

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

    $scope.ConfirmItems = function () {

        $scope.CheckoutStep = 'UserDataEntry';
    }

    $scope.OrderItems = function () {
        var customer = {
            PurchaserName: $scope.Name,
            PurchaserPhone: $scope.Phone,
            PurchaserEmail: $scope.Email,
            BillingStreet: $scope.BillingStreet,
            BillingStreet2: $scope.BillingStreet2,
            BillingCity: $scope.BillingCity,
            BillingState: $scope.BillingState,
            BillingZip: $scope.BillingZip,
            ShippingStreet: $scope.ShippingStreet,
            ShippingStreet2: $scope.ShippingStree2,
            ShippingCity: $scope.ShippingCity,
            ShippingState: $scope.ShippingState,
            ShippingZip: $scope.ShippingZip
        }
        
        var productData = JSON.stringify({
            customerOrder: customer,
            items: $scope.CartItems,
        });

        $http({
            url: '/Home/PlaceOrder',
            method: "POST",
            dataType: 'jsonp',
            headers: { 'Content-Type': 'application/json; charset=utf-8' },
            data: productData
        }).success(function (data, status, headers, config) {
            $scope.CheckoutStep = 'OrderConfirmation';
            $scope.EmptyCart();
        }).catch(function (error) {
            $scope.error = error;
        })
    }

    $scope.EmptyCart = function () {
        $http({
            url: '/Home/EmptyCart',
            method: "POST",
            dataType: 'jsonp',
        }).success(function (data, status, headers, config) {
        }).catch(function (error) {
            $scope.error = error;
        })
    }

    $scope.UseSameAddress = false;
    $scope.AddressCheckChanged = function () {

        if ($scope.UseSameAddress) {
            $scope.ShippingStreet = $scope.BillingStreet;
            $scope.ShippingStree2 = $scope.BillingStree2;
            $scope.ShippingCity = $scope.BillingCity;
            $scope.ShippingState = $scope.BillingState;
            $scope.ShippingZip = $scope.BillingZip;
        }
        else {
            $scope.ShippingStreet = '';
            $scope.ShippingStree2 = '';
            $scope.ShippingCity = '';
            $scope.ShippingState = '';
            $scope.ShippingZip = ''
        }
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