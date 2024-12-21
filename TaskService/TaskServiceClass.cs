using System.Diagnostics;
using TaskService.Interface;

namespace TaskService
{
    public class TaskServiceClass : ITaskinterface
    {
        private readonly List<Tasks> tasksList= [];
        public TaskServiceClass()
        {
            LoadModelData(tasksList);
        }
        public async Task<int> CreateTasks(Tasks input)
        {
            int getMaxid= tasksList.Select(task => task.Id).Max();

            await Task.Run(()=> tasksList.Add(new Tasks() 
            { 
                Id = getMaxid+1,
                Name = input.Name,
                Priority = input.Priority,
                Status = input.Status 
            }));

            return getMaxid+1;
        }

        public async Task DeleteTasks(int taskInput)
        {
            var tasksItem= await Task.Run(()=> tasksList.Where(x=> x.Id == taskInput).FirstOrDefault());
            if (tasksItem != null)
            {
                tasksList.Remove(tasksItem);
            }
        }

        public async Task<int> EditTasks(Tasks input)
        {
            var tasksItem = await Task.Run(() => tasksList.Where(x => x.Id == input.Id).FirstOrDefault());
            if (tasksItem != null)
            {
                tasksItem.Status = input.Status;
                tasksItem.Priority = input.Priority;
                tasksItem.Name = input.Name;
            }
            
            return input.Id;

        }

        public async Task<IEnumerable<Tasks>> GetTasks()
        {
            return await Task.Run(()=> tasksList);
        }

        private static void LoadModelData(List<Tasks> taskList)
        {
            taskList.Add(new Tasks() { Id = 1, Name = "Crate Functonality for Login", Priority = 1, Status = "In Progress" });
            taskList.Add(new Tasks() { Id = 2, Name = "Crate Functonality for Logout", Priority = 2, Status = "Not Started" });

        }
    }
}
