using Mirror;
using UnityEngine;
using System.Collections;

public class GameFlowManager : NetworkBehaviour
{
    public static GameFlowManager Instance { get; private set; }

    [Header("Countdown")]
    [SerializeField] private float countdownTime = 3f;

    [SyncVar(hook = nameof(OnStateChanged))]
    private MatchState state = MatchState.Waiting;

    // single winner
    [SyncVar]
    private uint winnerNetId;

    // team winner (-1 = none)
    [SyncVar]
    private int winnerTeamId = -1;

    private bool matchFinished;

    public MatchState State => state;
    public uint WinnerNetId => winnerNetId;
    public int WinnerTeamId => winnerTeamId;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public override void OnStartServer()
    {
        state = MatchState.Waiting;
        winnerNetId = 0;
        winnerTeamId = -1;
        matchFinished = false;
    }

    // ============================
    // SERVER API
    // ============================

    [Server]
    public void StartMatch()
    {
        if (state != MatchState.Waiting) return;
        StartCoroutine(CountdownRoutine());
    }

    [Server]
    public void FinishMatch(uint winner)
    {
        if (matchFinished) return;

        matchFinished = true;
        winnerNetId = winner;
        winnerTeamId = -1;
        state = MatchState.Finished;
    }

    [Server]
    public void FinishMatchTeam(int teamId)
    {
        if (matchFinished) return;

        matchFinished = true;
        winnerTeamId = teamId;
        winnerNetId = 0;
        state = MatchState.Finished;
    }

    // ============================
    // COUNTDOWN
    // ============================

    [Server]
    IEnumerator CountdownRoutine()
    {
        state = MatchState.Countdown;

        float t = countdownTime;
        while (t > 0)
        {
            RpcCountdownTick(Mathf.CeilToInt(t));
            yield return new WaitForSeconds(1f);
            t--;
        }

        state = MatchState.Playing;
    }

    // ============================
    // CLIENT
    // ============================

    void OnStateChanged(MatchState oldState, MatchState newState)
    {
        MatchEvents.RaiseStateChanged(newState);
    }

    [ClientRpc]
    void RpcCountdownTick(int seconds)
    {
        MatchEvents.RaiseCountdown(seconds);
    }
}
