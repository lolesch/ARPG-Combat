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
        [SerializeField] private LayerMask layerMaskToIgnore;

        [SerializeField][Range(1f, 20f)] private float gamepadMovementRadius = 15f; // maybe use the selected skill's range

        private RaycastHit hit;

        public Vector2 PointerPosition;
        public Vector2 LeftStickVector;
        public Vector2 RightStickVector;

        [Header("Observation")]
        [SerializeField] private bool hasClickedOnUI;
        [SerializeField] private bool isCrouching;
        [SerializeField] private bool hasMovementInput;
        [SerializeField] private bool isForcingStop;

        public event Action<int> OnCast;
        public event Action<bool> OnMove;
        public event Action<bool> OnForceStop;

        public bool IsPointerOutsideOfScreen() => IsOutsideOfScreen(PointerPosition);

        /// <summary>
        /// Returns true if the passed position extends the screen's current resolution
        /// </summary>
        /// <param name="position"></param>
        /// <remarks>
        /// Make sure that the position is calculated with the transform's lossyScale in mind.
        /// </remarks>
        public bool IsOutsideOfScreen(Vector2 position)
        {
            return
                position.x < 0 ||
                position.x > Screen.currentResolution.width ||
                position.y < 0 ||
                position.y > Screen.currentResolution.height;
        }

        private void Update()
        {
            if (Pointer.current != null)
                PointerPosition = Pointer.current.position.ReadValue();

            if (Gamepad.current != null)
            {
                LeftStickVector = Gamepad.current.leftStick.ReadValue();
                /// stick sensitivity adjustment
                LeftStickVector *= LeftStickVector.magnitude;
                /// range factor
                LeftStickVector *= gamepadMovementRadius;
                /// project the Vector2 into the isometric plane (forward vector is rotated by 45 degrees)
                //LeftStickVector = XZPlane.IsometricForward(LeftStickVector) + transform.position; // TODO: why transform.position? this is not the player 
            }
        }

        #region Interaction

        public void LeftClick(InputAction.CallbackContext ctx)
        {
            if (IsPointerOutsideOfScreen())
                return;

            if (ctx.started)
                //if (!EventSystem.current.IsPointerOverGameObject()) // some check for IsOverUI here => no movement when interacting with UI
                SetCurrentInteractable(PointerPosition);

            if (isForcingStop || Interactable.Current && Interactable.Current.Interaction == InteractionType.Enemy || Interactable.Current && Interactable.Current.Interaction == InteractionType.Destroyable)
                CastSkill0(ctx);

            OnMove?.Invoke(ctx.performed);

            // TODO move into interactable / listen to event action
            void SetCurrentInteractable(Vector2 pointerPosition)
            {
                var ray = Camera.main.ScreenPointToRay(pointerPosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~layerMaskToIgnore))
                {
                    Interactable.Current = hit.collider.TryGetComponent(out Interactable interactable) ? interactable : null;
                    EditorDebug.LogWarning($"{hit.collider.gameObject.name} | isInteractable {null != interactable} | {~layerMaskToIgnore}");
                }
                else if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMaskToIgnore))
                {
                    Interactable.Current = hit.collider.TryGetComponent(out Interactable interactable) ? interactable : null;
                    EditorDebug.LogWarning($"{hit.collider.gameObject.name} | isInteractable {null != interactable} | {layerMaskToIgnore}");
                }
                else
                    EditorDebug.LogError("raycast \t no collider found");
            }
        }

        public void RightClick(InputAction.CallbackContext ctx)
        {
            if (IsPointerOutsideOfScreen())
                return;

            if (ctx.started)
                //if (!EventSystem.current.IsPointerOverGameObject()) // some check for IsOverUI here => no movement when interacting with UI
                CastSkill1(ctx);
        }

        public void LeftStick(InputAction.CallbackContext ctx)
        {
            OnMove?.Invoke(ctx.performed);
        }

        public void ForceStop(InputAction.CallbackContext ctx)
        {
            if (ctx.started && hasMovementInput)
                CastSkill0(ctx);

            isForcingStop = ctx.performed;
            OnForceStop?.Invoke(ctx.performed);
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

        private void CastSkill(int index)
        {
            // TODO: rotate towards target and cast in that directions
            OnCast?.Invoke(index);
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
    }
}