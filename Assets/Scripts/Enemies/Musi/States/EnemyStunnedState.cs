using UnityEngine;

public class EnemyStunnedState : IEnemyState
{
    private Enemy enemy;
    private float stunTime;
    private float timer;

    public EnemyStunnedState(Enemy enemy, float duration)
    {
        this.enemy = enemy;
        stunTime = duration;
    }

    public void Enter()
    {

    }

    public void Tick() { }

    public void FixedTick() { }

    public void Exit() { }
}
