using TestTask.InputSystem;
using UnityEngine;
using Zenject;

namespace TestTask.PlayerSystems
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _moveAnimator;


        private EventBus _eventBus;

        private int _isMooving = Animator.StringToHash("isMooving");


        [Inject]
        private void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;




            _eventBus.OnHorizontalKeyPressed += StartPlayerMovementAnimation;
            _eventBus.OnHorizontalKeyReleased += StopPlayerMovementAnimation;
        }


        private void StopPlayerMovementAnimation() => _moveAnimator.SetBool(_isMooving, false);

        private void StartPlayerMovementAnimation(MovementType type) => _moveAnimator.SetBool(_isMooving, true);

    }
}
