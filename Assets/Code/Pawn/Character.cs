using System.Collections.Generic;
using ARPG.Enums;
using UnityEngine;

namespace ARPG.Pawn
{
    public class Character : MonoBehaviour
    {
        public Dictionary<StatName, StatScore> stats = new Dictionary<StatName, StatScore>();
        [SerializeField] private Dictionary<Resource, ResourceScore> resources = new Dictionary<Resource, ResourceScore>();

        private void Awake()
        {
            // foreach (KeyValuePair<StatName, StatScore> stat in stats)
            //     stat.Value.RecalculateValues();
        }
    }
}