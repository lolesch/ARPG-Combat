using ARPG.Enums;
using TeppichsTools.Logging;
using UnityEditor;
using UnityEngine;

namespace ARPG.Input
{
    [RequireComponent(typeof(CapsuleCollider))]
    public abstract class Interactable : MonoBehaviour
    {
        public static Interactable Current;

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

            if (TryGetComponent(out Rigidbody rb) && rb.collisionDetectionMode == CollisionDetectionMode.Discrete)
                EditorDebug.LogWarning("input Raycasting might miss this rigidBodie's collider since input is not in the fixedUpdate loop");
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = Current == this ? Color.cyan : Color.grey;
            Handles.DrawWireDisc(transform.position, transform.up, InteractionRange);
        }
#endif
    }
}