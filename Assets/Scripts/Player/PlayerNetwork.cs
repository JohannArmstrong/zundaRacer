using Mirror;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerNetwork : NetworkBehaviour
{
    private PlayerHealth health;
    private PlayerInputHandler input;

    void Awake()
    {
        health = GetComponent<PlayerHealth>();
        input = GetComponent<PlayerInputHandler>();

        // MUY IMPORTANTE: desactivar input por defecto
        input.enabled = false;
    }

    public override void OnStartAuthority()
    {
        SetupLocalPlayer();
    }

    public override void OnStopAuthority()
    {
        CleanupLocalPlayer();
    }

    private void SetupLocalPlayer()
    {
        // 1️⃣ Activar input SOLO para el jugador local
        input.enabled = true;

        // 2️⃣ Registrar en cámara
        CameraManager cam = FindFirstObjectByType<CameraManager>();
        if (cam != null)
        {
            cam.RegisterLocalPlayer(health);
        }

        // 3️⃣ Registrar en MainManager
        MainManager manager = FindFirstObjectByType<MainManager>();
        if (manager != null)
        {
            manager.RegisterLocalPlayer(health);
        }

        // 4️⃣ Inicializar UI de HP
        PlayerHPUI ui = FindFirstObjectByType<PlayerHPUI>();
        if (ui != null)
        {
            ui.Init(health);
        }

        Debug.Log($"[PlayerNetwork] Local player initialized: {netId}");
    }

    private void CleanupLocalPlayer()
    {
        // Acá podrías limpiar referencias si hacés disconnect
    }
}
