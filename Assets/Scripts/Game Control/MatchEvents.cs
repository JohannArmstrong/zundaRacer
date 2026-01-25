using System;

public static class MatchEvents
{
    public static event Action<MatchState> OnStateChanged;
    public static event Action<int> OnCountdownTick;

    public static void RaiseStateChanged(MatchState state)
    {
        OnStateChanged?.Invoke(state);
    }

    public static void RaiseCountdown(int seconds)
    {
        OnCountdownTick?.Invoke(seconds);
    }
}
