using Mirror;
using UnityEngine;
using System.Linq;

public class GoalTrigger : NetworkBehaviour
{
    private IGoalCondition goalCondition;

    public override void OnStartServer()
    {
        var behaviours = Object.FindObjectsByType<NetworkBehaviour>(
            FindObjectsSortMode.None
        );

        goalCondition = behaviours
            .OfType<IGoalCondition>()
            .FirstOrDefault();

        if (goalCondition == null)
        {
            Debug.LogError(
                "No IGoalCondition found in scene. " +
                "Add a GameMode that implements IGoalCondition."
            );
        }
    }

    [ServerCallback]
    void OnTriggerEnter2D(Collider2D other)
    {
        if (goalCondition == null) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;

        NetworkIdentity identity = player.netIdentity;
        if (identity == null) return;

        goalCondition.OnGoalReached(identity);
    }
}
