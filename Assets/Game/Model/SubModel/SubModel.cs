using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace PFY.Game.Model.SubModel
{
    public abstract class SubModel
    {
        private EcsSystems _systems;
        
        protected SubModel(EcsWorld world)
        {
            _systems = new EcsSystems(world);
        }

        public void AddSystem(IEcsSystem system)
        {
            _systems.Add(system);
        }

        public void Inject(params object[] injects)
        {
            _systems.Inject(injects);
        }

        public void Init()
        {
            _systems.Init();
        }

        public void Update()
        {
            _systems.Run();
        }

        public void Destroy()
        {
            _systems.Destroy();
            _systems = null;
        }
    }
}