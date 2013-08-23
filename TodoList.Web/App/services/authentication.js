TasksApp.Services.factory('service.auth', function ($http, $cookieStore) {
    //var currentUser = $cookieStore.get('user') || { username: '', role: userRoles.public };

    $cookieStore.remove('user');

    function changeUser(user) {
        _.extend(currentUser, user);
    };

    return {
        authorize: function (accessLevel, role) {
            if (role === undefined)
                role = currentUser.role;

            return accessLevel.bitMask & role.bitMask;
        },
        isLoggedIn: function (user) {
            return false;
            /*if (user === undefined)
                user = currentUser;
            return user.role.title == userRoles.user.title || user.role.title == userRoles.admin.title;*/
        },
        register: function (user, success, error) {
            $http.post('/register', user).success(function (res) {
                changeUser(res);
                success();
            }).error(error);
        },
        login: function (user, success) {
            $http.post('Account/Login', { model: user }).success(function (user) {
                changeUser(user);
                success(user);
            });
        },
        logout: function (success, error) {
            $http.post('/logout').success(function () {
                changeUser({
                    username: '',
                    role: userRoles.public
                });
                success();
            }).error(error);
        }/*,
        accessLevels: accessLevels,
        userRoles: userRoles,
        user: currentUser*/
    };
});
