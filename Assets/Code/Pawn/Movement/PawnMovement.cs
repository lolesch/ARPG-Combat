using ARPG.Enums;
using TeppichsTools.Logging;
using UnityEngine;
using UnityEngine.AI;

namespace ARPG.Pawn.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class PawnMovement : MonoBehaviour
    {
        [Header("Movement")]
        [Range(.01f, .99f)]
        [SerializeField] protected float crouchFactor = .5f;

        protected float maxSpeed = 5f;

        protected NavMeshAgent agent;
        private Character character;

        [Header("Rotation")]
        [Range(1f, 40f)]
        [SerializeField] protected float rotationSpeed = 17f;

        protected Vector3 forward = Vector3.forward;

        protected virtual void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            if (!agent)
                EditorDebug.LogError($"Missing component of type {nameof(NavMeshAgent)} on {gameObject.name}");

            character = GetComponentInParent<Character>();
            if (!character)
                EditorDebug.LogError($"Missing component of type {nameof(Character)} on {gameObject.name}");
            else if (character.stats.TryGetValue(StatName.MovementSpeed, out StatScore speed))
            {
                speed.maxHasChanged += SetSpeed;
                maxSpeed = speed.MaxValue;
            }

            agent.speed = maxSpeed;
        }

        private void OnDestroy()
        {
            if (character.stats.TryGetValue(StatName.MovementSpeed, out StatScore speed))
                speed.maxHasChanged -= SetSpeed;
        }

        protected void SetSpeed(float speed)
        {
            maxSpeed = speed;
            agent.speed = maxSpeed;
        }

        protected void SetCrouchSpeed(bool isCrouching) => agent.speed = isCrouching ? maxSpeed * crouchFactor : maxSpeed;

        protected void SetDestination(Vector3 target)
        {
            if (target != agent.pathEndPosition)
                agent.SetDestination(target);
            EditorDebug.DrawLine(transform.position, agent.destination);
        }
    }
}