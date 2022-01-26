using UnityEngine;

namespace ARPG.Container
{
    [CreateAssetMenu(fileName = "New Skill Object", menuName = "Inventory System/Items/Skill")]
    public class Skill : Entity
    {
        [SerializeField] private SpawnData spawnData;
        [Range(0f, 300f)]
        [SerializeField] private float cooldownDuration = 0f;

        public SpawnData SpawnData => spawnData;
        public float CooldownDuration => cooldownDuration;
    }
}
