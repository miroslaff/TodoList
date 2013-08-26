TasksApp.Controllers.controller('tasks.index', ['$scope', 'service.todoList', function ($scope, todoList) {

    $scope.newTodo = createTodo();


    todoList.getAll(function (data) {
        $scope.list = data;

        toastr.success(data.length + ' Todos Loaded');
    });

    $scope.clear = function () {
        _.each($scope.list, function (item) {
            if (!item.isDone) return;

            $scope.removeTodo(item);
        });
    };

    $scope.addTodo = function () {
        todoList.add($scope.newTodo, function (data) {
            $scope.list.push(data);
            $scope.newTodo = createTodo();

            toastr.success('Todo Added');
        });
    };

    $scope.removeTodo = function (todo) {
        todoList.remove(todo.id, function () {
            var i = _.indexOf($scope.list, todo);

            if (i < 0) return;

            $scope.list.splice(i, 1);

            toastr.success('Todo Removed');
        });
    };

    $scope.count = function () {
        return _.filter($scope.list, function (item) { return !item.isDone; }).length;
    };

    function createTodo() {
        return {
            title: null,
            dueDate: null,
            priority: 2
        };
    }

}]);
