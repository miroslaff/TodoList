TasksApp.Services.factory('service.todoList', ['$config', '$http', function ($config, $http) {
    return {
        getAll: function (callback) {
            return $http.get($config.serviceRoot + 'todoList').success(callback);
        },

        get: function (id, callback) {
            return $http.get($config.serviceRoot + 'todoList/' + id).success(callback);
        },

        add: function (todo, callback) {
            return $http.post($config.serviceRoot + 'todoList', todo).success(callback);
        },

        update: function (todo, callback) {
            return $http.put($config.serviceRoot + 'todoList', todo).success(callback);
        },

        remove: function (id, callback) {
            return $http['delete']($config.serviceRoot + 'todoList/' + id).success(callback);
        }

    };
}]);
