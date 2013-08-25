TasksApp.Controllers.controller('account.login', ['$scope', '$location', 'service.auth', function ($scope, $location, serviceAuth) {

    $scope.login = function () {
        serviceAuth.login({
            username: $scope.username,
            password: $scope.password,
            rememberMe: $scope.rememberMe
        }, function () {
            toastr.success('Logged in successfully.');

            $location.path('/');
        });
    };

    $scope.isValid = function () {
        if (!$scope.username || !$scope.password) return false;

        return true;
    };

}]);
