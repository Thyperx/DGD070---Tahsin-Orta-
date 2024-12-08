using Unity.Entities;
using UnityEngine;

public class PlayerHealthAuthoring : MonoBehaviour
{
    public float Value = 100f; // Sa�l�k de�eri (default: 100)

    class Baker : Baker<PlayerHealthAuthoring>
    {
        public override void Bake(PlayerHealthAuthoring authoring)
        {
            AddComponent(new PlayerHealthComponent { Value = authoring.Value });
        }
    }
}
