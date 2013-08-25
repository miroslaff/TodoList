TasksApp.Controllers.controller('account.register', ['$scope', 'service.auth', function ($scope, serviceAuth) {

    $scope.register = function () {
        serviceAuth.register({
            username: $scope.username,
            password: $scope.password,
            confirmPassword: $scope.confirmPassword,
        }, function (user) {
            toastr.success('Stuff is done');
        });
    };

    $scope.isValid = function () {
        if (!$scope.username || !$scope.password || !$scope.confirmPassword) return false;

        if ($scope.password != $scope.confirmPassword) return false;

        return true;
    };

}]);
