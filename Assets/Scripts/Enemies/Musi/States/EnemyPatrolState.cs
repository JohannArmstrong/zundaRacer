public class EnemyPatrolState : IEnemyState
{
    private EnemyStateMachine enemy;
    private float direction = -1f;

    public EnemyPatrolState(EnemyStateMachine enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        enemy.Visual.SetFacing(direction);
    }

    public void Tick()
    {
        if (!enemy.Sensors.IsGrounded)
        {
            enemy.Visual.SetIdle(true);
            return;
        }

        enemy.Visual.SetIdle(false);

        if (enemy.Sensors.WallAhead(direction))
        {
            direction *= -1f;
            enemy.Visual.SetFacing(direction);
        }
    }

    public void FixedTick()
    {
        enemy.Motor.Move(direction);
    }

    public void Exit()
    {
        enemy.Motor.Stop();
    }
}

