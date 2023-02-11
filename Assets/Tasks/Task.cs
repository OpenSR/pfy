using Leopotam.EcsLite;

namespace PFY.Tasks
{
    public abstract class Task
    {
        public abstract bool IsFinish { get; }

        public abstract void Run(EcsWorld world);
        public abstract void Update(EcsWorld world);
        public abstract void Destroy();
    }
}