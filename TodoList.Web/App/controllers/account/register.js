TasksApp.Controllers.controller('account.register', ['$scope', '$location', 'service.auth', function ($scope, $location, serviceAuth) {

    $scope.register = function () {
        serviceAuth.register({
            username: $scope.username,
            password: $scope.password,
            confirmPassword: $scope.confirmPassword,
        }, function () {
            toastr.success('Registration successfull<br />Please login');
            $location.path('/account/login');
        });
    };

    $scope.isValid = function () {
        if (!$scope.username || !$scope.password || !$scope.confirmPassword) return false;

        if ($scope.password != $scope.confirmPassword) return false;

        return true;
    };

}]);
