using Unity.Entities;

namespace Game.Health.CreateSystem
{
    public partial class MyUniqueCreatePlayerHealthSystem : SystemBase
    {
        protected override void OnCreate()
        {
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var playerEntity = entityManager.CreateEntity();
            entityManager.AddComponentData(playerEntity, new PlayerHealthComponent { Value = 100f });
        }

        protected override void OnUpdate()
        {
            // Bu sistem sadece baþlangýçta çalýþýr.
        }
    }
}
