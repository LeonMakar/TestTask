using System;
using TestTask.InputSystem;
using UnityEngine;

public class EventBus
{
    public Action<MovementType> OnHorizontalKeyPressed;
    public Action OnHorizontalKeyReleased;
    public Action<RaycastHit2D[]> OnEnemyFinded;
    public Action<bool> OnGameActivityChanged;

    public Action OnHealthChanged;
    public Action OnEnemyKilled;
}
