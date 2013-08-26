TasksApp.Services.factory('service.auth', ['$config', '$http', '$cookies', function ($config, $http, $cookies) {
    // Tell the API that we want Forms authentication
    $http.defaults.headers.common['Authorization'] = 'Forms ';

    return {
        isLoggedIn: function () {
            return $cookies.auth !== undefined;
        },

        register: function (user, callback) {
            $http.put($config.serviceRoot + 'account/register', user).success(function (res) {
                callback();
            });
        },

        login: function (user, callback) {
            $http.post($config.serviceRoot + 'account/login', user).success(function () {
                $cookies.auth = user.username;

                callback();
            });
        },

        logout: function (callback) {
            $http.get($config.serviceRoot + 'account/logout').success(function () {
                $cookies.auth = undefined;
                callback();
            });
        }
    };
}]);
