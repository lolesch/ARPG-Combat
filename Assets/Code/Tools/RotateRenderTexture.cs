using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using ARPG.Tools;

namespace ARPG.GUI
{
    public class RotateRenderTexture : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        [SerializeField] private RotationAnchor anchor;
        [SerializeField] float speed = 1f;

        private bool getButton = false;

        private void Update()
        {
            if (getButton)
                anchor?.onRotation.Invoke(speed);
        }

        public void OnPointerDown(PointerEventData eventData) => getButton = true;
        public void OnPointerUp(PointerEventData eventData) => getButton = false;
        public void OnPointerExit(PointerEventData eventData) => getButton = false;
    }
}