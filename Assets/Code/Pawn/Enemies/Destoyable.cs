using ARPG.Combat;
using ARPG.Input;
using TeppichsTools.Logging;
using UnityEngine;

namespace ARPG.Pawns.Enemy
{
    public class Destoyable : Interactable, IDamageTaker
    {
        public void SetCurrentHealth() => throw new System.NotImplementedException();

        public void TakeDamage(float damage) => Kill();

        protected override void Interact()
        {
            throw new System.NotImplementedException();
        }

        protected void Kill()
        {
            DropLoot();

            Destroy(gameObject);

            void DropLoot() => EditorDebug.Log($"{name} is dropping Loot");
        }
    }
}