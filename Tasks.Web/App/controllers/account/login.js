TasksApp.Controllers.controller('account.login', ['$scope', 'service.auth', function ($scope, serviceAuth) {

    $scope.signIn = function () {
        serviceAuth.login({
            username: $scope.username,
            password: $scope.password,
            rememberMe: $scope.rememberMe
        }, function (user) {
            toastr.success('Stuff is done');
        });
    }

}]);
