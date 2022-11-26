using Microsoft.EntityFrameworkCore;
using TaskWebApi.EFCore;
using Task = TaskWebApi.EFCore.Task;

namespace TaskWebApi.Model
{
    public class DbHelper
    {
        private EF_DataContext _context;
        public DbHelper(EF_DataContext context)
        {
            _context = context;
        }

        //GET
        public List<TaskModel> GetTasks()
        {
            List<TaskModel> response = new List<TaskModel>();
            //var dataList = _context.Tasks.ToList();
            var dataList = _context.Tasks.FromSql($"SELECT * FROM task").ToList();
            dataList.ForEach(x => response.Add(new TaskModel()
            {
                EmployeeId = x.EmployeeId,
                EmployeeName = x.EmployeeName,
                Breakfast = x.Breakfast,
                Brunch = x.Brunch,
                Lunch = x.Lunch,
                Supper = x.Supper,
                AssignedTo = x.AssignedTo,
            }));
            return response;
        }
        public List<LogModel> GetRequestedTaskByName(string name)
        {
            List<LogModel> response = new List<LogModel>();
            var dataList = _context.Logs.FromSql($"SELECT * FROM log").Where(n => n.AssignedTo.Equals(name)).ToList();
            dataList.ForEach(x => response.Add(new LogModel()
            {
                Id = x.Id,
                AssignedTo = x.AssignedTo,
                EmployeeName = x.EmployeeName,
                MealTime = x.MealTime,
                RequestedMeal = x.RequestedMeal,
                Status = x.Status
            }));
            return response;
        }

        //Unneccessary***************************
        public TaskModel GetTaskById(int id)
        {
            TaskModel response = new TaskModel();
            var x = _context.Tasks.Where(d => d.EmployeeId.Equals(id)).FirstOrDefault();
            return new TaskModel()
            {
                EmployeeId = x.EmployeeId,
                EmployeeName = x.EmployeeName,
                Breakfast = x.Breakfast,
                Brunch = x.Brunch,
                Lunch = x.Lunch,
                Supper = x.Supper,
                AssignedTo = x.AssignedTo,
            };
        }

        //POST/PUT/PATCH
        public void UpdateTask(TaskModel taskModel)
        {
            Task dbTable = new Task();
            if (taskModel.EmployeeId > 0)
            {
                dbTable = _context.Tasks.Where(d => d.EmployeeId.Equals(taskModel.EmployeeId)).FirstOrDefault();
                //PUT
                if (dbTable != null)
                {
                    dbTable.Breakfast = taskModel.Breakfast;
                    dbTable.Brunch = taskModel.Brunch;
                    dbTable.Lunch = taskModel.Lunch;
                    dbTable.Supper = taskModel.Supper;
                    dbTable.AssignedTo = taskModel.AssignedTo;
                }
            }
            else
            {
                //POST
                dbTable.EmployeeId = taskModel.EmployeeId;
                dbTable.EmployeeName = taskModel.EmployeeName;
                dbTable.Breakfast = taskModel.Breakfast;
                dbTable.Brunch = taskModel.Brunch;
                dbTable.Lunch = taskModel.Lunch;
                dbTable.Supper = taskModel.Supper;
                dbTable.AssignedTo = taskModel.AssignedTo;
                _context.Tasks.Add(dbTable);
            }
            _context.SaveChanges();
        }

        public void UpdateRequest(LogModel logModel)
        {
            Log dbTable = new Log();
            if (logModel.Id > 0)
            {
                dbTable = _context.Logs.Where(d => d.Id.Equals(logModel.Id)).FirstOrDefault();
                //PUT
                if (dbTable != null)
                {
                    dbTable.AssignedTo = logModel.AssignedTo;
                    dbTable.RequestedMeal = logModel.RequestedMeal;
                }
            }
            else
            {
                //POST
                dbTable.Id = logModel.Id;
                dbTable.AssignedTo = logModel.AssignedTo;
                dbTable.EmployeeName = logModel.EmployeeName;
                dbTable.MealTime = logModel.MealTime;
                dbTable.RequestedMeal = logModel.RequestedMeal;
                dbTable.Status = logModel.Status;
                _context.Logs.Add(dbTable);
            }
            _context.SaveChanges();
        }
        //DELETE
        public void DeleteTask(int id)
        {
            var task = _context.Tasks.Where(d => d.EmployeeId.Equals(id)).FirstOrDefault();
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }
        public void DeleteRequest(int id)
        {
            var log = _context.Logs.Where(d => d.Id.Equals(id)).FirstOrDefault();
            if (log != null)
            {
                _context.Logs.Remove(log);
                _context.SaveChanges();
            }
        }
    }
}
