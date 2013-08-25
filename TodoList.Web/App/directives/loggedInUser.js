TasksApp.Directives.directive('loggedInUser', function () {
    return {
        template: '<ul class="nav navbar-nav"><li class="navbar-text"><span class="glyphicon glyphicon-user"></span> {{username()}}</li><li><a href="#" data-ng-click="logout()">Log out</a></li></ul>',
        replace: true,
        controller: ['$scope', '$location', 'service.auth', function ($scope, $location, serviceAuth) {
            $scope.username = function () {
                return serviceAuth.getCurrentUser();
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

