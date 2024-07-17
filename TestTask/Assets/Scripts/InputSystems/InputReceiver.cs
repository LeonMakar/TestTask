using UnityEngine.InputSystem;
using Zenject;

namespace TestTask.InputSystem
{
    public class InputReceiver : IInitializable
    {
        private EventBus _eventBus;
        private StandartInputMap _inputMap;

        #region Init
        public void Initialize()
        {
            _inputMap.Ground.HorizontalMovement.started += callBackContext => SendHorizontalInputSignalsPressed(callBackContext);
            _inputMap.Ground.HorizontalMovement.canceled += callbackContext => SendHorizontalInputSignalsReleased(callbackContext);
        }


        [Inject]
        private void Construct(EventBus eventBus, StandartInputMap inputMap)
        {
            _eventBus = eventBus;
            _inputMap = inputMap;

            _inputMap.Ground.Enable();
        }
        #endregion

        private void SendHorizontalInputSignalsReleased(InputAction.CallbackContext context) => _eventBus.OnHorizontalKeyReleased?.Invoke();

        private void SendHorizontalInputSignalsPressed(InputAction.CallbackContext context)
        {
            switch (context.ReadValue<float>())
            {
                case 1:
                    _eventBus.OnHorizontalKeyPressed?.Invoke(MovementType.Right);
                    break;
                case -1:
                    _eventBus.OnHorizontalKeyPressed?.Invoke(MovementType.Left);
                    break;
            }
        }
    } 
}
