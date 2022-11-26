using Microsoft.AspNetCore.Mvc;
using TaskWebApi.EFCore;
using TaskWebApi.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskWebApi.Controllers
{
    [ApiController]
    public class TaskApiController : ControllerBase
    {
        private readonly DbHelper _db;
        public TaskApiController(EF_DataContext eF_DataContext)
        {
            _db = new DbHelper(eF_DataContext);
        }
        // GET: api/<TaskApiController>
        [HttpGet]
        [Route("api/[controller]/GetTasks")]
        public IActionResult Get()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<TaskModel> data = _db.GetTasks();
                if (!data.Any())
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetApiResponse(type, data));
            }
            catch(Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
        [HttpGet]
        [Route("api/[controller]/GetRequestedTaskByName/{name}")]
        public IActionResult Get(string name)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<LogModel> data = _db.GetRequestedTaskByName(name);
                if (!data.Any())
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetApiResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // GET api/<TaskApiController>/5
        [HttpGet]
        [Route("api/[controller]/GetTasksById/{id}")]
        public IActionResult Get(int id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                TaskModel data = _db.GetTaskById(id);
                if (data == null)
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetApiResponse(type, data));
            }
            catch(Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // POST api/<TaskApiController>
        [HttpPost]
        [Route("api/[controller]/UpdateTask")]
        public IActionResult Post([FromBody] TaskModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.UpdateTask(model);
                return Ok(ResponseHandler.GetApiResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost]
        [Route("api/[controller]/UpdateRequest")]
        public IActionResult Post([FromBody] LogModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.UpdateRequest(model);
                return Ok(ResponseHandler.GetApiResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // DELETE api/<TaskApiController>/5
        [HttpDelete]
        [Route("api/[controller]/DeleteTask/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.DeleteTask(id);
                return Ok(ResponseHandler.GetApiResponse(type, "Delete Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
        [HttpDelete]
        [Route("api/[controller]/DeleteRequest/{id}")]
        public IActionResult DeleteRequest(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.DeleteRequest(id);
                return Ok(ResponseHandler.GetApiResponse(type, "Delete Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
