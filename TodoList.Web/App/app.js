'use strict';

var TasksApp = TasksApp || {};

TasksApp.Constants = angular.module('tasks.constants', []);
TasksApp.Utils = angular.module('tasks.utils', []);
TasksApp.Services = angular.module('tasks.services', []);
TasksApp.Controllers = angular.module('tasks.controllers', []);
TasksApp.Filters = angular.module('tasks.filters', []);
TasksApp.Directives = angular.module('tasks.directives', []);

angular.module('tasks', ['ui.bootstrap',
                         'ngCookies',
                         'tasks.utils',
                         'tasks.filters',
                         'tasks.services',
                         'tasks.directives',
                         'tasks.constants',
                         'tasks.controllers'])
    .config(
        ['$routeProvider', '$httpProvider', '$config',
            function ($routeProvider, $httpProvider, $config) {
                $routeProvider
                    .when('/', {
                        templateUrl: $config.viewsRoot + 'tasks/index.html',
                        controller: 'tasks.index',
                        authorize: true
                    })
                    .when('/account/login', {
                        templateUrl: $config.viewsRoot + 'account/login.html',
                        controller: 'account.login',
                        authorize: false
                    })
                    .when('/account/register', {
                        templateUrl: $config.viewsRoot + 'account/register.html',
                        controller: 'account.register',
                        authorize: false
                    })
                    .otherwise({
                        redirectTo: '/account/login'
                    });

                $httpProvider.responseInterceptors.push(['$q', '$location', function ($q, $location) {
                    return function (promise) {
                        return promise.then(function (response) {
                            return response;
                        }, function (response) {

                            switch (response.status) {
                                case 401:
                                    $location.path('/account/login');
                                    break;
                                case 404:
                                    toastr.error('Resource was not found.');
                                    break;
                                default:
                                    if (response.data.errors) {
                                        var message = '';
                                        for (var i = 0; i < response.data.errors.length; i++) {
                                            message += response.data.errors[i] + '<br />';
                                        }

                                        toastr.error(message);
                                    }
                                    else
                                        toastr.error('There was a server error while issuing the request.');
                            }

                            return $q.reject(response);
                        });
                    };
                }]);
            }])
     .run(['$rootScope', '$location', 'service.auth', function ($rootScope, $location, serviceAuth) {
         $rootScope.$on("$routeChangeStart", function (event, next) {
             if (next.authorize && !serviceAuth.isLoggedIn())
                 $location.path('/account/login');
         });
     }]);

TasksApp.Constants.constant('$config', {
    serviceRoot: 'api/',
    viewsRoot: 'app/views/',
    templatesRoot: 'app/templates/'
});