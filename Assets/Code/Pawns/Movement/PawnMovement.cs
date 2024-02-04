using ARPG.Enums;
using TeppichsTools.Logging;
using UnityEngine;
using UnityEngine.AI;

namespace ARPG.Pawns.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class PawnMovement : MonoBehaviour
    {
        [SerializeField] protected NavMeshAgent agent;
        [SerializeField] protected Pawn pawn;
        [SerializeField] protected Animator animator;

        [Header("Rotation")]
        [Range(1f, 40f)]
        [SerializeField] protected float rotationSpeed = 17f;

        private void OnValidate()
        {
            agent = GetComponent<NavMeshAgent>();
            if (!agent)
                EditorDebug.LogError($"Missing component of type {nameof(NavMeshAgent)} on {gameObject.name}");

            pawn = GetComponent<Pawn>();
            if (!pawn)
                EditorDebug.LogError($"Missing component of type {nameof(Pawn)} on {gameObject.name}");

            animator = GetComponentInChildren<Animator>();
            if (!animator)
                EditorDebug.LogError($"Missing component of type {nameof(Animator)} on {gameObject.name}");
        }

        private void OnEnable()
        {
            if (pawn.stats.TryGetValue(StatName.MovementSpeed, out StatScore speed))
            {
                speed.maxHasChanged += SetSpeed;

                SetSpeed(speed);
            }
        }

        private void OnDisable()
        {
            if (pawn.stats.TryGetValue(StatName.MovementSpeed, out StatScore speed))
                speed.maxHasChanged -= SetSpeed;
        }

        protected void SetSpeed(StatScore speed) => agent.speed = speed.MaxValue;

        protected void SetDestination(Vector3 target)
        {
            if (target != agent.pathEndPosition)
                agent.SetDestination(target);
            EditorDebug.DrawLine(transform.position, agent.destination);
        }
    }
}