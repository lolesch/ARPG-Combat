using System;
using UnityEngine;

namespace ARPG.Tools
{
    public class RotationAnchor : MonoBehaviour
    {
        public Action<float> onRotation;

        [SerializeField] float startOffset = 3f;

        private void OnEnable()
        {
            onRotation += Rotate;
            transform.localRotation = Quaternion.Euler(0, startOffset, 0);
        }

        private void OnDisable() => onRotation -= Rotate;

        void Rotate(float speed) => transform.localRotation *= Quaternion.Euler(0, speed, 0);
    }
}