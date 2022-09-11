using ARPG.Enums;
using System.Collections;
using System.Linq;
using TeppichsTools.Logging;
using UnityEngine;

namespace ARPG.Pawns.Enemy
{
    public class EnemyBase : Pawn
    {
        protected override void Kill()
        {
            DropLoot();

            //Destroy(gameObject);
            StartCoroutine(RespawnWithDelay(2f));

            if (resources.TryGetValue(ResourceName.HealthCurrent, out ResourceScore health))
                health.RefillCurrent();

            void DropLoot() => EditorDebug.Log($"{name} is dropping Loot");
        }

        IEnumerator RespawnWithDelay(float delay)
        {
            var GO = gameObject.GetComponentsInChildren<Transform>().FirstOrDefault(x => x != null && x.gameObject != gameObject).gameObject;
            GO.SetActive(false);
            yield return new WaitForSeconds(delay);
            GO.SetActive(true);
        }
    }
}