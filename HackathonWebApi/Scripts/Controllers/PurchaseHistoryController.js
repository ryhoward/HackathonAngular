var mainApp = angular.module('mainApp');

mainApp.controller('PurchaseHistoryController', ['$scope', '$http', 'historyFactory', function ($scope, $http, historyFactory) {
    $scope.HistoryItems = [];

    historyFactory.GetHistoryItems()
        .then(function (items) {
            var displayItems = [];
            $.each(items.data, function (index, item) {
                item.imgPath = '00' + ((item.ProductId.toString().length > 1) ? '' : '0') + item.ProductId;
                displayItems.push(item);
            });
            $scope.HistoryItems = displayItems;
        })
        .catch(function (error) {
            $scope.error = error;
        })
}]);