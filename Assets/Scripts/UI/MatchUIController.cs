using UnityEngine;
using TMPro;

public class MatchUIController : MonoBehaviour
{
    [SerializeField] private Canvas resultsCanvas;
    [SerializeField] private Canvas countdownCanvas;
    [SerializeField] private GameObject lifeCanvas;
    [SerializeField] private Canvas lobbyCanvas;

    private TMP_Text countdownText;



    void Awake()
    {
        countdownText = countdownCanvas.GetComponentInChildren<TMP_Text>();

        // Estado inicial seguro
        if (countdownCanvas != null)
            countdownCanvas.gameObject.SetActive(false);

        if (resultsCanvas != null)
            resultsCanvas.gameObject.SetActive(false);
    }

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
        countdownCanvas.gameObject.SetActive(true);
        countdownText.text = seconds.ToString();
        countdownText.gameObject.SetActive(true);
    }

    void OnStateChanged(MatchState state)
    {
        switch (state)
        {
            case MatchState.Countdown:
                // Se activa por los ticks
                lobbyCanvas.gameObject.SetActive(false);
                break;

            case MatchState.Playing:
                if (countdownCanvas != null)
                    countdownCanvas.gameObject.SetActive(false);
                    lifeCanvas.gameObject.SetActive(true);
                break;

            case MatchState.Finished:
                if (resultsCanvas != null)
                    resultsCanvas.gameObject.SetActive(true);
                break;
        }
    }
}
