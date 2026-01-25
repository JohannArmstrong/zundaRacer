using Mirror;
using UnityEngine;
using System.Collections.Generic;

public class RaceGameMode : NetworkBehaviour, IGoalCondition
{
    public enum RaceType
    {
        FreeForAll,
        Teams
    }

    [Header("Mode Settings")]
    [SerializeField] private RaceType raceType = RaceType.FreeForAll;

    private bool matchFinished = false;

    // Para evitar múltiples triggers del mismo jugador
    private HashSet<uint> finishedPlayers = new HashSet<uint>();

    // Para equipos
    private HashSet<int> finishedTeams = new HashSet<int>();

    [Server]
    public void OnGoalReached(NetworkIdentity playerIdentity)
    {
        if (matchFinished) return;
        if (playerIdentity == null) return;

        uint netId = playerIdentity.netId;

        // Evitar repeticiones por múltiples metas
        if (finishedPlayers.Contains(netId))
            return;

        finishedPlayers.Add(netId);

        if (raceType == RaceType.FreeForAll)
        {
            FinishMatch(playerIdentity, null);
        }
        else if (raceType == RaceType.Teams)
        {
            PlayerTeam team = playerIdentity.GetComponent<PlayerTeam>();
            if (team == null) return;

            int teamId = team.TeamId;

            if (finishedTeams.Contains(teamId))
                return;

            finishedTeams.Add(teamId);

            FinishMatch(null, teamId);
        }
    }

    [Server]
    private void FinishMatch(NetworkIdentity winnerPlayer, int? winnerTeam)
    {
        matchFinished = true;

        if (winnerPlayer != null)
        {
            // modo individual
            GameFlowManager.Instance.FinishMatch(winnerPlayer.netId);
        }
        else if (winnerTeam.HasValue)
        {
            // modo equipos
            GameFlowManager.Instance.FinishMatchTeam(winnerTeam.Value);
        }
    }
}
