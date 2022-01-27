using ARPG.Combat;
using UnityEngine;

namespace ARPG.Container
{
    [CreateAssetMenu(fileName = "New Spawn Behaviour", menuName = "Inventory System/Items/SkillBehaviour/SpawnBehaviour")]
    public class SpawnData : ScriptableObject
    {
        [SerializeField]
        private bool spawnAtCursor = false;
        public bool SpawnAtCursor { get => spawnAtCursor; }

        [Range(1u, 24u)]
        [SerializeField]
        private uint amountToSpawn = 1u;
        public uint AmountToSpawn { get => amountToSpawn; }

        [Range(1u, 360u)]
        [SerializeField]
        private uint fullAngle = 360u;
        public float FullAngle { get => fullAngle; }

        [Range(0f, 180f)]
        [SerializeField]
        private float minDist = 0f;
        public float MinDistance { get => minDist; }

        [Range(0u, 30u)]
        [SerializeField]
        private uint projectileSpeed = 3u;
        public float ProjectileSpeed { get => projectileSpeed; }

        [Range(1, 15)]
        [SerializeField]
        private float maxDist = 5;
        public float MaxDistance { get => maxDist; }

        [Range(0, 10)]
        [SerializeField]
        private float projectileDiameter = 1;
        public float ProjectileRadius { get => projectileDiameter / 2; }
        [Range(0, 300)]
        [SerializeField]
        private uint resourceCost = 10;
        public uint ResourceCost { get => resourceCost; }

        [SerializeField]
        private DamageShape damageShape;
        public DamageShape DamageShape { get => damageShape; }
    }
}