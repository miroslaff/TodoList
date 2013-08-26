TasksApp.Services.factory('service.auth', ['$config', '$http', '$cookieStore', function ($config, $http, $cookieStore) {
    // Tell the API that we want Forms authentication
    $http.defaults.headers.common['Authorization'] = 'Forms ';

    var currentUser = { isLoggedIn: false, name: '' };

    return {
        getCurrentUser: function () {
            return currentUser;
        },

        isLoggedIn: function () {
            return currentUser.isLoggedIn;
        },

        register: function (user, callback) {
            $http.put($config.serviceRoot + 'account/register', user).success(function (res) {
                callback();
            });
        },

        login: function (user, callback) {
            $http.post($config.serviceRoot + 'account/login', user).success(function () {
                currentUser.isLoggedIn = true;
                currentUser.name = user.username;

                callback();
            });
        },

        logout: function (callback) {
            $http.get($config.serviceRoot + 'account/logout').success(function () {
                callback();
            });
        }
    };
}]);
