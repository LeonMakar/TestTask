using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MainData", menuName = "MainData")]
public class MainData : ScriptableObject
{
    [Header("Enemy Settings")]
    [Range(0, 100)] public int EnemyKillToWinValue;
    public EnemySpawningTimeInterval EnemySpawningTimeInterval;
    public EnemySpeedValueInterval EnemySpeedValueInterval;
    public int EnemyHealth;

    [Space(10), Header("Player Settings")]
    public float ShootRange;
    public float ShootRate;
    public int ShootDamage;
    public float BulletSpeed;
    public int PlayerHealth;

    public bool GameIsActive = true;


}

[Serializable]
public struct EnemySpawningTimeInterval
{
    public int MinTime;
    public int MaxTime;
}
[Serializable]
public struct EnemySpeedValueInterval
{
    public float MinSpeed;
    public float MaxSpeed;
}
