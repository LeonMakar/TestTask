using System.Collections;
using TestTask.InputSystem;
using UnityEngine;
using Zenject;

namespace TestTask.PlayerSystems
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _speed = 3;
        [SerializeField] private float _leftBoarder;
        [SerializeField] private float _rightBoarder;

        private Coroutine _cachedCoroutine;
        private EventBus _eventBus;

        [Inject]
        private void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;

            _eventBus.OnHorizontalKeyPressed += StartMoveHorizontal;
            _eventBus.OnHorizontalKeyReleased += StopMoveHorizontal;
        }

        private void StopMoveHorizontal()
        {
            if (_cachedCoroutine != null)
                StopCoroutine(_cachedCoroutine);
        }

        private void StartMoveHorizontal(MovementType movementType)
        {
            if (_cachedCoroutine != null)
                StopCoroutine(_cachedCoroutine);
            _cachedCoroutine = StartCoroutine(StartMovingCoroutine(movementType));
        }

        private IEnumerator StartMovingCoroutine(MovementType movementType)
        {
            while (true)
            {
                if (movementType == MovementType.Left && transform.TransformPoint(transform.position).x > _leftBoarder)
                    _characterController.Move(Vector3.left * _speed * Time.fixedDeltaTime);
                else if (movementType == MovementType.Right && transform.TransformPoint(transform.position).x < _rightBoarder)
                    _characterController.Move(Vector3.right * _speed * Time.fixedDeltaTime);
                yield return null;
            }
        }
    }
}
