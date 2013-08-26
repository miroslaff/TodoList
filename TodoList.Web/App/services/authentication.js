TasksApp.Services.factory('service.auth', ['$config', '$http', '$cookieStore', 'service.base64', function ($config, $http, $cookieStore, base64) {
    // initialize to whatever is in the cookie, if anything
    //$http.defaults.headers.common['Authorization'] = 'Basic ' + ($cookieStore.get('authdata') || '');

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
                /*var encoded = base64.encode(user.username + ':' + user.password);
                $http.defaults.headers.common.Authorization = 'Basic ' + encoded;
                $cookieStore.put('authdata', encoded);*/
                currentUser.isLoggedIn = true;
                currentUser.name = user.username;

                callback();
            });
        },

        logout: function (callback) {
            $http.get($config.serviceRoot + 'account/logout').success(function () {
                /*document.execCommand("ClearAuthenticationCache");
                $cookieStore.remove('authdata');
                $http.defaults.headers.common.Authorization = 'Basic ';*/

                callback();
            });
        }
    };
}]);
