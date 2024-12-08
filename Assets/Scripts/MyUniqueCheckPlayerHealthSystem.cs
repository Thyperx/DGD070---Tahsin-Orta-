using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using UnityEngine;

[BurstCompile]
public partial struct MyUniqueCheckPlayerHealthJob : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter ECB;

    [ReadOnly] public NativeArray<Entity> EntitiesWithDamagedComponent;
    [ReadOnly] public NativeArray<Entity> EntitiesWithHealedComponent;

    public void Execute(Entity entity, [EntityIndexInQuery] int index, ref PlayerHealthComponent health)
    {
        // PlayerDamagedComponent kontrolü
        if (EntitiesWithDamagedComponent.Contains(entity))
        {
            health.Value = Mathf.Max(0, health.Value - 10f);
            ECB.RemoveComponent<PlayerDamagedComponent>(index, entity);
        }

        // PlayerHealedComponent kontrolü
        if (EntitiesWithHealedComponent.Contains(entity))
        {
            health.Value = Mathf.Min(100, health.Value + 10f);
            ECB.RemoveComponent<PlayerHealedComponent>(index, entity);
        }
    }
}

namespace Game.Health.CheckSystem
{
    public partial class MyUniqueCheckPlayerHealthSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.TempJob);

            // EntityQuery ile NativeArray oluþturma
            var queryDamaged = GetEntityQuery(ComponentType.ReadOnly<PlayerDamagedComponent>());
            var queryHealed = GetEntityQuery(ComponentType.ReadOnly<PlayerHealedComponent>());

            var entitiesWithDamagedComponent = queryDamaged.ToEntityArray(Allocator.TempJob);
            var entitiesWithHealedComponent = queryHealed.ToEntityArray(Allocator.TempJob);

            try
            {
                var job = new MyUniqueCheckPlayerHealthJob
                {
                    ECB = ecb.AsParallelWriter(),
                    EntitiesWithDamagedComponent = entitiesWithDamagedComponent,
                    EntitiesWithHealedComponent = entitiesWithHealedComponent
                };

                // Paralel iþ planlama
                Dependency = job.ScheduleParallel(Dependency);
                Dependency.Complete(); // Ýþlerin tamamlanmasýný bekle

                ecb.Playback(EntityManager); // Ýþlemleri uygula
            }
            finally
            {
                // Kaynaklarý serbest býrak
                entitiesWithDamagedComponent.Dispose();
                entitiesWithHealedComponent.Dispose();
                ecb.Dispose();
            }
        }
    }
}
