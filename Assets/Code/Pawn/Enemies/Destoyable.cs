using ARPG.Combat;
using TeppichsTools.Logging;
using UnityEngine;

namespace ARPG.Pawns.Enemy
{
    public class Destoyable : MonoBehaviour, IDamageTaker
    {
        public void SetCurrentHealth() => throw new System.NotImplementedException();

        public void TakeDamage(float damage) => Kill();

        protected void Kill()
        {
            DropLoot();

            Destroy(gameObject);

            void DropLoot() => EditorDebug.Log($"{name} is dropping Loot");
        }
    }
}