using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TodoList.Web.Models;

namespace TodoList.Web.Controllers
{
    [Authorize]
    public class TodoListController : ApiController
    {
        private TodoListContext db = new TodoListContext();

        // GET api/TodoList
        public IEnumerable<TodoItem> GetTodoItems()
        {
            var todoitems = db.TodoItems.Include(t => t.User);
            return todoitems.AsEnumerable();
        }

        // GET api/TodoList/5
        public TodoItem GetTodoItem(int id)
        {
            TodoItem todoitem = db.TodoItems.Find(id);
            if (todoitem == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return todoitem;
        }

        // PUT api/TodoList/5
        public HttpResponseMessage PutTodoItem(int id, TodoItem todoitem)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != todoitem.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(todoitem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/TodoList
        public HttpResponseMessage PostTodoItem(TodoItem todoitem)
        {
            if (ModelState.IsValid)
            {
                db.TodoItems.Add(todoitem);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, todoitem);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = todoitem.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/TodoList/5
        public HttpResponseMessage DeleteTodoItem(int id)
        {
            TodoItem todoitem = db.TodoItems.Find(id);
            if (todoitem == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.TodoItems.Remove(todoitem);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, todoitem);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}