using ARPG.Tools;
using UnityEngine;

namespace ARPG.Container
{
    [CreateAssetMenu(fileName = "New Skill Object", menuName = "Inventory System/Items/Skill")]
    public class Skill : Entity
    {
        [SerializeField] private SpawnData spawnData;
        public SpawnData SpawnData => spawnData;
    }
}
