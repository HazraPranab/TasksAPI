using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskService;
using TaskService.Interface;

namespace TasksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskinterface _taskinterface;
        public TasksController(ITaskinterface taskinterface)
        {
            _taskinterface = taskinterface;
        }

        [HttpGet("GetTasks")]
        public async Task<IEnumerable<Tasks>> Get()
        {
            return await _taskinterface.GetTasks();
        }

        [HttpPost("CreateTasks")]
        public async Task<int> Post([FromBody] Tasks body)
        {
            return await _taskinterface.CreateTasks(body);
        }

        [HttpPut("EditTasks")]
        public async Task<int> Put([FromBody] Tasks tasklinput)
        {
            return await _taskinterface.EditTasks(tasklinput);
        }

        [HttpDelete("DeleteTasks/{taskid}")]
        public async Task Delete(int taskid)
        {
            await _taskinterface.DeleteTasks(taskid);
        }
    }
}
