using Game.Scripts.Interaction;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class InteractionHandler : MonoBehaviour
    {
        public float InteractionRadius = 3f;
        public Transform PlayerTransform;
        private bool _canInteract = false;

        private void Update()
        {
            _canInteract = false; // Reset canInteract flag each frame

            if (Input.GetKeyDown(KeyCode.E)) // Assuming 'E' is the interact key
            {
                HandleInteraction();
            }
            else
            {
                CheckForInteractables();
            }
        }

        private void HandleInteraction()
        {
            var hitColliders = Physics.OverlapSphere(PlayerTransform.position, InteractionRadius);
            IInteractable closestInteractable = null;
            var closestDistance = InteractionRadius;

            foreach (var hitCollider in hitColliders)
            {
                var interactable = hitCollider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    Vector3 directionToInteractable = (hitCollider.transform.position - PlayerTransform.position).normalized;
                    float distanceToInteractable = Vector3.Distance(PlayerTransform.position, hitCollider.transform.position);

                    if (distanceToInteractable < closestDistance && IsFacingInteractable(directionToInteractable))
                    {
                        closestInteractable = interactable;
                        closestDistance = distanceToInteractable;
                    }
                }
            }

            closestInteractable?.Interact();
        }

        private void CheckForInteractables()
        {
            Collider[] hitColliders = Physics.OverlapSphere(PlayerTransform.position, InteractionRadius);

            foreach (var hitCollider in hitColliders)
            {
                IInteractable interactable = hitCollider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    Vector3 directionToInteractable = (hitCollider.transform.position - PlayerTransform.position).normalized;

                    if (IsFacingInteractable(directionToInteractable))
                    {
                        _canInteract = true;
                        break;
                    }
                }
            }
        }

        private bool IsFacingInteractable(Vector3 directionToInteractable)
        {
            float dotProduct = Vector3.Dot(PlayerTransform.forward, directionToInteractable);
            return dotProduct > 0.5f; // Adjust this threshold as needed
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(PlayerTransform.position, InteractionRadius);
        }

        private void OnGUI()
        {
            if (!_canInteract) return;
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = 24;
            style.normal.textColor = Color.white;

            Rect rect = new Rect(Screen.width / 2 - 100, Screen.height - 100, 200, 50);
            GUI.Label(rect, "Press E to interact", style);
        }
    }
}
