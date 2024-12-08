using Unity.Entities;
using Unity.Burst;
using UnityEngine;

[BurstCompile]
public partial struct MyUniqueChangePlayerHealthJob : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter ECB;
    public bool DamageKeyPressed;
    public bool HealKeyPressed;

    public void Execute(Entity entity, [EntityIndexInQuery] int index)
    {
        if (DamageKeyPressed)
        {
            ECB.AddComponent<PlayerDamagedComponent>(index, entity);
        }

        if (HealKeyPressed)
        {
            ECB.AddComponent<PlayerHealedComponent>(index, entity);
        }
    }
}

namespace Game.Health.ChangeSystem
{
    public partial class MyUniqueChangePlayerHealthSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.TempJob);

            var job = new MyUniqueChangePlayerHealthJob
            {
                ECB = ecb.AsParallelWriter(),
                DamageKeyPressed = Input.GetKeyDown(KeyCode.D),
                HealKeyPressed = Input.GetKeyDown(KeyCode.H)
            };

            Dependency = job.ScheduleParallel(Dependency);
            Dependency.Complete(); // Paralel iþlemleri tamamla

            ecb.Playback(EntityManager); // Playback EntityManager üzerinden yapýlýr
            ecb.Dispose(); // Dispose EntityCommandBuffer üzerinden yapýlýr
        }
    }
}
