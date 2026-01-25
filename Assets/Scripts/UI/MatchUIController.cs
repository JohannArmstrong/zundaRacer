using UnityEngine;
using TMPro;

public class MatchUIController : MonoBehaviour
{
    [SerializeField] private Canvas resultsCanvas;
    [SerializeField] private TMP_Text countdownText;



    void OnEnable()
    {
        MatchEvents.OnCountdownTick += ShowCountdown;
        MatchEvents.OnStateChanged += OnStateChanged;
    }

    void OnDisable()
    {
        MatchEvents.OnCountdownTick -= ShowCountdown;
        MatchEvents.OnStateChanged -= OnStateChanged;
    }

    void ShowCountdown(int seconds)
    {
        countdownText.text = seconds.ToString();
        countdownText.gameObject.SetActive(true);
    }

    void OnStateChanged(MatchState state)
    {
        if (state == MatchState.Playing)
            countdownText.gameObject.SetActive(false);

        if (state == MatchState.Finished)
            resultsCanvas.gameObject.SetActive(true);
    }
}
