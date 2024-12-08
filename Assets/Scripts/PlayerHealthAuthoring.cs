using Unity.Entities;
using UnityEngine;

public class PlayerHealthAuthoring : MonoBehaviour
{
    public float Value = 100f;

    class Baker : Baker<PlayerHealthAuthoring>
    {
        public override void Bake(PlayerHealthAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PlayerHealthComponent { Value = authoring.Value });
        }
    }
}
