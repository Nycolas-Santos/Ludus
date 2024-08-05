using System;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class AnimationHandler : MonoBehaviour
    {
        [SerializeField] private Animator animatorController;
        
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Update()
        {
            UpdateAnimations();
        }

        private void UpdateAnimations()
        {
            animatorController.SetFloat(Speed, Math.Abs(Input.GetAxisRaw("Horizontal")) + Math.Abs(Input.GetAxisRaw("Vertical")));
        }
    }
}
