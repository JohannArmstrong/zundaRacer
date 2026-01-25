using Mirror;

public interface IGoalCondition
{
    void OnGoalReached(NetworkIdentity playerIdentity);
}
