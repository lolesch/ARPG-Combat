using ARPG.Enums;
using ARPG.Input;
using ARPG.Tools;
using TeppichsTools.Logging;
using UnityEngine;
using UnityEngine.AI;

namespace ARPG.Pawns.Movement
{
    public class PlayerMovement : PawnMovement
    {
        private NavMeshHit navMeshHit;

        private StepLock movementLocker = new();

        private bool hasMovementInput;

        private float movementSpeed;

        private void OnDestroy()
        {
            movementLocker.locked -= ForceStop;
            InputReceiver.Instance.OnMove -= SetMove;
            InputReceiver.Instance.OnForceStop -= SetForceStop;
        }

        protected void Awake()
        {
            movementLocker.locked += ForceStop;
            InputReceiver.Instance.OnMove += SetMove;
            InputReceiver.Instance.OnForceStop += SetForceStop;

            agent.updateRotation = false;
        }

        private void OnEnable()
        {
            if (pawn.stats.TryGetValue(StatName.MovementSpeed, out StatScore speed))
                movementSpeed = speed.baseValue;
        }

        private void Update()
        {
            if (hasMovementInput)
            {
                var hasGamepadMovementInput = 0.125f < InputReceiver.Instance.LeftStickVector.magnitude;

                Vector2 screenPoint = hasGamepadMovementInput ? Camera.main.WorldToScreenPoint(InputReceiver.Instance.LeftStickVector) : InputReceiver.Instance.PointerPosition;

                SetMovementTarget(screenPoint);
            }

            if (pawn.stats.TryGetValue(StatName.MovementSpeed, out StatScore speed) && 0 < speed.MaxValue)
                animator.SetFloat("Run Blend", agent.velocity.magnitude / speed.baseValue);

            // make pawnRotation a component that listens to several rotation invokers
            // casting a skill can rotate the pawn without the need of movement input
            SetRotation();
        }

        private void SetMove(bool clickMove) => hasMovementInput = clickMove;


        public void SetMovementTarget(Vector3 inputTarget)
        {
            Ray ray = Camera.main.ScreenPointToRay(inputTarget);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 3))
                inputTarget = hit.point;

            // switch case?
            if (Interactable.Current == null)
                SetDestination(FindNavigableLocationAt(inputTarget));

            else if (Interactable.Current.Interaction == InteractionType.Enemy || Interactable.Current.Interaction == InteractionType.Destroyable)
            {
                Vector3 target = Interactable.Current.transform.position;
                target = target - GetDirection(transform.position, target) * Mathf.Min(/* TODO: skill attack range or default meele attack range */20 + Interactable.Current.InteractionRange, Vector3.Distance(transform.position, target));
                SetDestination(FindNavigableLocationAt(target));
            }

            else if (Interactable.Current.Interaction == InteractionType.NPC || Interactable.Current.Interaction == InteractionType.Container)
            {
                Vector3 target = Interactable.Current.transform.position;
                target = target - GetDirection(transform.position, target) * Mathf.Min(Interactable.Current.InteractionRange, Vector3.Distance(transform.position, target));
                SetDestination(FindNavigableLocationAt(target));
            }
        }

        public void SetForceStop(bool stop)
        {
            if (stop)
                movementLocker.Add(this);
            else
                movementLocker.Remove(this);
        }

        private void ForceStop(bool locked)
        {
            agent.isStopped = locked;
            SetDestination(transform.position + transform.forward * agent.stoppingDistance);
        }

        Vector3 FindNavigableLocationAt(Vector3 input)
        {
            if (Interactable.Current)
                return Interactable.Current.transform.position -
                        GetDirection(transform.position, Interactable.Current.transform.position) *
                        Mathf.Min(Vector3.Distance(Interactable.Current.transform.position, transform.position), Interactable.Current.InteractionRange);

            ///check if hitResult.point is navigable
            if (SamplePosition(input))
                return navMeshHit.position;

            var lerpLocation = Vector3.zero;
            var from = transform.position;
            var to = input;

            ///calculate nearest navigable position
            for (int i = 0; i < 5; i++)
            {
                lerpLocation = Vector3.Lerp(from, to, .5f);

                if (SamplePosition(lerpLocation))
                    from = lerpLocation;
                else
                    to = lerpLocation;
            }

            EditorDebug.DrawLine(transform.position, input, Color.red);

            if (SamplePosition(from))
                return navMeshHit.position;

            EditorDebug.LogError($"No navigable target after lerp at \t {lerpLocation}");
            return transform.position;
        }

        private void SetRotation()
        {
            var rotationTarget = agent.steeringTarget;
            rotationTarget.y = transform.position.y;

            var direction = GetDirection(transform.position, rotationTarget);

            if (direction == Vector3.zero)
                return;

            Quaternion desiredRotation = Quaternion.LookRotation(direction);

            if (transform.rotation != desiredRotation)
                transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
        }

        bool SamplePosition(Vector3 position) => NavMesh.SamplePosition(position, out navMeshHit, 1f, NavMesh.AllAreas);

        protected Vector3 GetDirection(Vector3 from, Vector3 to) => Vector3.Normalize(to - from);
    }
}