
namespace Spike.Providers.Common
{
    using System;
    
    public class WorkTask
    {
        public WorkTask(int id, Func<int, string> work)
        {
            this.Id = id;
            this.Work = work;
        }

        public int Id { get; set; }

        private Func<int, string> Work { get; set; }

        public void Execute()
        {
            Work(Id);
        }
    }
}
