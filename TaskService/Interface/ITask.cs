using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskService.Interface
{
    public interface ITaskinterface
    {
        Task<IEnumerable<Tasks>> GetTasks();
        Task<int> CreateTasks(Tasks input);
        Task<int> EditTasks(Tasks input);
        Task DeleteTasks(int taskId);
    }
}
