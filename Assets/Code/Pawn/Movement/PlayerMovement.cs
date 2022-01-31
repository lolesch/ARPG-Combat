using ARPG.Enums;
using ARPG.Input;
using ARPG.Tools;
using TeppichsTools.Logging;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace ARPG.Pawns.Movement
{
    public class PlayerMovement : PawnMovement
    {
        private NavMeshHit navMeshHit;

        private StepLock movementLocker = new();

        private bool isLeftClicking;

        private void OnDestroy()
        {
            movementLocker.locked -= ForceStop;
            InputTranslator.Instance.setLeftClick -= SetLeftClick;
            InputTranslator.Instance.setForceStop -= SetForceStop;
            InputTranslator.Instance.setCrouching -= SetCrouchSpeed;
        }

        protected override void Awake()
        {
            base.Awake();

            movementLocker.locked += ForceStop;
            InputTranslator.Instance.setLeftClick += SetLeftClick;
            InputTranslator.Instance.setForceStop += SetForceStop;
            InputTranslator.Instance.setCrouching += SetCrouchSpeed;

            agent.updateRotation = false;
        }

        private void Update()
        {
            if (isLeftClicking)
                CalculatePointerMovementTarget();

            //animationSpeed?.Invoke(agent.velocity.magnitude / 6f); // walk speed adjustment to not slide
            SetRotation();
        }

        private void CalculatePointerMovementTarget()
        {
            var screenPoint = Pointer.current.position.ReadValue();

            Ray ray = Camera.main.ScreenPointToRay(screenPoint);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 3))
                SetMovementTarget(hit.point);
        }

        private void SetLeftClick(bool isLeftClicking) => this.isLeftClicking = isLeftClicking;

        public void SetMovementTarget(Vector3 inputTarget)
        {
            if (movementLocker.Unlocked)
            {
                // switch case?
                if (Interactable.current == null)
                    SetDestination(FindNavigableLocationAt(inputTarget));

                else if (Interactable.current.Interaction == InteractionType.Enemy || Interactable.current.Interaction == InteractionType.Destroyable)
                {
                    Vector3 target = Interactable.current.transform.position;
                    target = target - GetDirection(transform.position, target) * Mathf.Min(/* TODO: skill attack range or default meele attack range */20 + Interactable.current.InteractionRange, Vector3.Distance(transform.position, target));
                    SetDestination(FindNavigableLocationAt(target));
                }

                else if (Interactable.current.Interaction == InteractionType.NPC || Interactable.current.Interaction == InteractionType.Container)
                {
                    Vector3 target = Interactable.current.transform.position;
                    target = target - GetDirection(transform.position, target) * Mathf.Min(Interactable.current.InteractionRange, Vector3.Distance(transform.position, target));
                    SetDestination(FindNavigableLocationAt(target));
                }
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
            if (Interactable.current)
                return Interactable.current.transform.position -
                        GetDirection(transform.position, Interactable.current.transform.position) *
                        Mathf.Min(Vector3.Distance(Interactable.current.transform.position, transform.position), Interactable.current.InteractionRange);

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

        public void SetRotation()
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