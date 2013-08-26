TasksApp.Directives.directive('todo', ['$config', function factory($config) {
    return {
        templateUrl: $config.templatesRoot + 'todo.html',
        scope: {
            todo: '=model',
            deleteFn: '&'
        },
        replace: true,
        controller: ['$scope', '$location', 'service.todoList', function ($scope, $location, todoList) {

            $scope.isEditing = false;

            $scope.priorities = [undefined,
                { icon: 'default', name: 'Low' },
                { icon: 'info', name: 'Normal' },
                { icon: 'danger', name: 'High' }];

            //
            // Automatic saving changes

            var firstLoad = true;
            $scope.$watch('todo', function (value) {
                if (!value || $scope.todo.id == 0 || $scope.isEditing) return;

                if (firstLoad) {
                    firstLoad = false;
                    return;
                }

                todoList.update($scope.todo, function () {
                    toastr.success('Changes Saved');
                });
            }, true);

            //
            // Button actions

            $scope.save = function () {
                $scope.todo.title = $scope.clone.title;
                $scope.todo.priority = $scope.clone.priority;
                $scope.todo.dueDate = $scope.clone.dueDate;

                todoList.update($scope.todo, function () {
                    $scope.isEditing = false;

                    toastr.success('Changes Saved');
                });
            };

            $scope.edit = function () {
                $scope.clone = angular.copy($scope.todo);

                $scope.isEditing = true;
            };

            $scope.cancel = function () {
                $scope.isEditing = false;
            };

            $scope.remove = function () {
                $scope.deleteFn({ todo: $scope.todo });
            };
        }]
    };
}]);
