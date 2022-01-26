using System.Collections.Generic;
using ARPG.Enums;
using UnityEngine;

namespace ARPG.Pawn
{
    public class Character : MonoBehaviour
    {
        public Dictionary<StatName, StatScore> stats = new Dictionary<StatName, StatScore>();
        [SerializeField] protected Dictionary<Resource, ResourceScore> resources = new Dictionary<Resource, ResourceScore>();
    }
}