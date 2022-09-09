using ARPG.Enums;
using System;
using TeppichsTools.Creation;
using TeppichsTools.Logging;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ARPG.Input
{
    /// <summary>
    /// The InputReceiver takes all the player input - converts it into actions and invokes them so others can listen to these events.
    /// This way the InputReceiver doesn't have to know other classes but can be referenced as instance.
    /// </summary>
    public class InputReceiver : Monoton<InputReceiver>
    {
        [SerializeField] private LayerMask layerMask = 1;

        [Range(1f, 20f)]
        [SerializeField] private float gamepadMovementRadius = 15f; // maybe use the selected skill's range

        private Vector2 leftStickPosition;
        private RaycastHit hit;

        [Header("Observation")]
        [SerializeField] private bool hasClickedOnUI;
        [SerializeField] private bool isCrouching;
        [SerializeField] private bool hasMovementInput;
        [SerializeField] private bool isLeftSticking;
        [SerializeField] private bool isForcingStop;

        public event Action<int> OnSetCasting;
        public event Action<bool> OnSetMoving;
        public event Action<bool> OnSetForceStop;

        /// <summary>
        /// Returns true if the passed position extends the screen's current resolution
        /// </summary>
        /// <param name="position"></param>
        /// <remarks>
        /// Make sure that the position is calculated with the transform's lossyScale in mind.
        /// </remarks>
        public static bool IsOutsideOfScreen(Vector2 position)
        {
            return
                position.x < 0 ||
                position.x > Screen.currentResolution.width ||
                position.y < 0 ||
                position.y > Screen.currentResolution.height;
        }

        #region Interaction

        public void LeftClick(InputAction.CallbackContext ctx)
        {
            var pointerPosition = Pointer.current.position.ReadValue();

            if (IsOutsideOfScreen(pointerPosition))
                return;

            if (ctx.started)
                //if (!EventSystem.current.IsPointerOverGameObject()) // some check for IsOverUI here => no movement when interacting with UI
                SetCurrentInteractable(pointerPosition);

            if (isForcingStop || Interactable.current && Interactable.current.Interaction == InteractionType.Enemy || Interactable.current && Interactable.current.Interaction == InteractionType.Destroyable)
                CastSkill0(ctx);
            else
                OnSetMoving?.Invoke(ctx.performed); // TODO: rework movement to calculate the target position here
                                                    // this alowes different calculation for mouse/gamepad input 

            void SetCurrentInteractable(Vector2 pointerPosition)
            {
                Ray ray = Camera.main.ScreenPointToRay(pointerPosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~layerMask))
                {
                    Interactable.current = hit.collider.TryGetComponent(out Interactable interactable) ? interactable : null;
                    EditorDebug.LogWarning($"{hit.collider.gameObject.name} | isInteractable {null != interactable} | {~layerMask}");
                }
                else if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    Interactable.current = hit.collider.TryGetComponent(out Interactable interactable) ? interactable : null;
                    EditorDebug.LogWarning($"{hit.collider.gameObject.name} | isInteractable {null != interactable} | {layerMask}");
                }
                else
                    EditorDebug.LogError("raycast \t no collider found");
            }
        }

        public void RightClick(InputAction.CallbackContext ctx)
        {
            if (IsOutsideOfScreen(Pointer.current.position.ReadValue()))
                return;

            if (ctx.started)
                //if (!EventSystem.current.IsPointerOverGameObject()) // some check for IsOverUI here => no movement when interacting with UI
                CastSkill1(ctx);
        }

        public void LeftStick(InputAction.CallbackContext ctx)
        {
            leftStickPosition = ctx.ReadValue<Vector2>();
            /// stick sensitivity adjustment
            leftStickPosition *= leftStickPosition.magnitude;
            /// range factor
            leftStickPosition *= gamepadMovementRadius;

            OnSetMoving?.Invoke(ctx.performed);

            //TODO: set Interactable in close range

            //Vector3 XZVector(Vector2 target)
            //{
            //    // this should be a cross product, no?
            //    return Quaternion.AngleAxis(45f, Vector3.up) * XZPlane.Vector(target);
            //}

            //screenPoint = Camera.main.WorldToScreenPoint(XZVector(leftStickPosition) + transform.position); // why transform.position? this is not the player 

            //private Vector3 HitPosition(Ray ray)
            //{
            //    if (!Physics.Raycast(ray, out hit, Mathf.Infinity, 3))
            //        EditorDebug.LogError("raycast \t no collider found");
            //
            //    return hit.point;
            //}
        }

        public void ForceStop(InputAction.CallbackContext ctx)
        {
            if (ctx.started && hasMovementInput)
                CastSkill(0);

            isForcingStop = ctx.performed;
            OnSetForceStop?.Invoke(isForcingStop);
        }

        //public void Crouch(InputAction.CallbackContext ctx)
        //{
        //    if (isHoldToCrouch)
        //        isCrouching = ctx.performed;
        //    else
        //    {
        //        if (ctx.started)
        //            isCrouching = !isCrouching;
        //    }
        //    SetCrouching(isCrouching);
        //}

        #endregion

        #region Skills

        private void CastSkill(int index)
        {
            // TODO: rotate towards target and cast in that directions
            OnSetCasting?.Invoke(index);
        }

        public void CastSkill0(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
                CastSkill(0);
        }

        public void CastSkill1(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
                CastSkill(1);
        }

        public void CastSkill2(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
                CastSkill(2);
        }

        public void CastSkill3(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
                CastSkill(3);
        }

        public void CastSkill4(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
                CastSkill(4);
        }

        public void CastSkill5(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
                CastSkill(5);
        }

        #endregion

        #region Menus
        public static Action toggleInGameMenu;
        public void ToggleInGameMenu(InputAction.CallbackContext ctx) { if (ctx.started) toggleInGameMenu?.Invoke(); }

        public static Action toggleInventory;
        public void ToggleInventory(InputAction.CallbackContext ctx) { if (ctx.started) toggleInventory?.Invoke(); }

        public static Action toggleSkills;
        public void ToggleSkills(InputAction.CallbackContext ctx) { if (ctx.started) toggleSkills?.Invoke(); }

        public static Action toggleProfile;
        public void ToggleProfile(InputAction.CallbackContext ctx) { if (ctx.started) toggleProfile?.Invoke(); }

        public static Action toggleOptions;
        public void ToggleOptions(InputAction.CallbackContext ctx) { if (ctx.started) toggleOptions?.Invoke(); }

        #endregion

        ///// <summary>
        ///// Projects the point on the xzPlane around the objectPosition from a cameras perspective
        ///// </summary>
        //Vector3 TargetOnMovementPlane(Vector3 point, Vector3 objectPos, Vector3 cameraPos)
        //{
        //    return cameraPos + (point - cameraPos) * ((cameraPos.y - objectPos.y) / (cameraPos.y - point.y));
        //}
    }
}