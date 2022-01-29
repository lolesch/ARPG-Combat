using ARPG.Enums;
using TeppichsTools.Logging;
using UnityEditor;
using UnityEngine;

namespace ARPG.Input
{
    public abstract class Interactable : MonoBehaviour
    {
        public static Interactable current;

        [SerializeField]
        protected float interactionRange = 2f;
        public float InteractionRange => interactionRange;

        [SerializeField]
        protected InteractionType interaction = InteractionType.NONE;
        public InteractionType Interaction => interaction;

        protected abstract void Interact();

        private void Awake()
        {
            if (Interaction == InteractionType.NONE)
                EditorDebug.LogWarning($"Interaction \t {name} is missing an interaction type");
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, transform.up, InteractionRange);
        }
#endif
    }
}