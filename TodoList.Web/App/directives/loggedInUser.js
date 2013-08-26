TasksApp.Directives.directive('loggedInUser', function () {
    return {
        template: '<ul class="nav navbar-nav"><li class="navbar-text"><span class="glyphicon glyphicon-user"></span> {{username()}}</li><li><a href="#" data-ng-click="logout()">Log out</a></li></ul>',
        replace: true,
        controller: ['$scope', '$location', '$cookies', 'service.auth', function ($scope, $location, $cookies, serviceAuth) {
            $scope.username = function () {
                return $cookies.auth;
            };

            $scope.logout = function () {
                serviceAuth.logout(function () {
                    $location.path('/account/login');
                });

                return false;
            };
        }]
    };
});

