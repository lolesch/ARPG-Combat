using TeppichsTools.Logging;
using UnityEditor;
using UnityEngine;

namespace ARPG.Input
{
    [RequireComponent(typeof(Collider))]
    public abstract class Interactable : MonoBehaviour
    {
        public static Interactable current;

        protected Collider interactionCollider;

        [SerializeField]
        protected float interactionRange = 2f;
        public float InteractionRange => interactionRange;

        [SerializeField]
        private InteractionType interaction = InteractionType.NONE;
        public InteractionType Interaction => interaction;

        protected virtual void Interact() => EditorDebug.Log($"interacting with {current}");

        protected virtual void Awake()
        {
            interactionCollider = GetComponent<Collider>();

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

    public enum InteractionType
    {
        NONE = 0,
        Enemy = 1,
        NPC = 2,
        Player = 3,
        Container = 4,
        Destroyable = 5,
    }
}