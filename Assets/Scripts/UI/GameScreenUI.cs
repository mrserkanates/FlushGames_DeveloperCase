using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScreenUI : MonoBehaviour
{
    public TextMeshProUGUI goldAmountText;

    private void OnEnable()
    {
        EventManager.StartListening(Events.OnSellGem, UpdateUI);
    }

    private void OnDisable()
    {
        EventManager.StopListening(Events.OnSellGem, UpdateUI);
    }

    private void Start()
    {
        UpdateUI(null);
    }

    public void UpdateUI(Dictionary<string, object> message)
    {
        PlayerController playerController;
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (!player.TryGetComponent<PlayerController>(out playerController))
            return;

        goldAmountText.text = playerController.PlayerStats.golds.ToString();

    }
}
