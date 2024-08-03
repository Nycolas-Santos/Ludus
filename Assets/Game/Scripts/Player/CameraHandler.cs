using UnityEngine;

namespace Game.Scripts.Player
{
    public class CameraHandler : MonoBehaviour
    {
        public Vector3 Offset;
        public float SmoothSpeed = 0.125f;

        private Transform PlayerTransform => GameManager.Instance.Player.transform;

        void LateUpdate()
        {
            var desiredPosition = PlayerTransform.position + Offset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(PlayerTransform);
        }
    }
}
