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
        enemy.Visual.SetIdle(false);
    }

    // DECISIONES (Update)
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
        if (!enemy.Sensors.IsGrounded)
        {
            enemy.Motor.Stop(); // deja Y libre
            return;
        }

        enemy.Motor.Move(direction);
        enemy.Visual.SetFacing(direction);
    }

    public void Exit()
    {
        enemy.Motor.Stop();
    }
}
