using ARPG.Combat;
using ARPG.Enums;
using ARPG.Input;
using TeppichsTools.Logging;

namespace ARPG.Pawns.Destroyables
{
    public class Destroyable : Interactable, IEffectReceiver
    {
        public void ReceiveResourceEffect(ResourceEffect effect)
        {
            switch (effect.ResourceName)
            {
                case ResourceEffectName.Damage:
                    Kill();
                    break;
                default:
                    break;
            }
        }
        public void RemoveResourceEffect(ResourceEffect effect) => throw new System.NotImplementedException();

        public void ReceiveResourceOverTimeEffect(ResourceOverTimeEffect effect)
        {
            switch (effect.ResourceName)
            {
                case ResourceEffectName.Damage:
                    Kill();
                    break;
                default:
                    break;
            }
        }
        public void RemoveResourceOverTimeEffect(ResourceOverTimeEffect effect) => throw new System.NotImplementedException();

        public void ReceiveConditionEffect(ConditionEffect effect) => throw new System.NotImplementedException();
        public void RemoveConditionEffect(ConditionEffect effect) => throw new System.NotImplementedException();

        public void ReceiveStatusEffect(StatusEffect effect) => throw new System.NotImplementedException();
        public void RemoveStatusEffect(StatusEffect effect) => throw new System.NotImplementedException();

        protected override void Interact() => throw new System.NotImplementedException();

        protected void Kill()
        {
            DropLoot();

            Destroy(gameObject);

            void DropLoot() => EditorDebug.Log($"{name} is dropping Loot");
        }
    }
}