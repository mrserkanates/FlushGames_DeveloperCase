using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsUI : MonoBehaviour
{
    public GameObject gemStatusPrefab;
    public Transform gemStatusListParent;

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
        foreach (Transform child in gemStatusListParent)
        {
            Destroy(child.gameObject);
        }

        PlayerController playerController;
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (!player.TryGetComponent<PlayerController>(out playerController))
            return;

        for (int i = 0; i < playerController.PlayerStats.collectedGems.Count; i++)
        {
            GameObject goGemStatus = Instantiate(gemStatusPrefab, Vector3.zero, Quaternion.identity, gemStatusListParent);
            if (goGemStatus.TryGetComponent<GemStatusUI>(out GemStatusUI gemStatusUI))
            {
                gemStatusUI.Icon = playerController.PlayerStats.collectedGems[i].GetIconSprite();
                gemStatusUI.Name = playerController.PlayerStats.collectedGems[i].name;
                gemStatusUI.CollectedCount = playerController.PlayerStats.collectedGems[i].count;
            }
        }
    }
}
