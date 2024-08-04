using UnityEngine;

namespace Game.Scripts.Interaction
{
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        public abstract void Interact();
    }

}