using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsUI : MonoBehaviour
{
    public GameObject gemStatusPrefab;
    public Transform gemStatusListParent;

    private void OnEnable()
    {
        EventManager.StartListening(Events.OnCollectGem, UpdateUI);
    }

    private void OnDisable()
    {
        EventManager.StopListening(Events.OnCollectGem, UpdateUI);
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
            if (goGemStatus.TryGetComponent<GemStatus>(out GemStatus gemStatus))
            {
                gemStatus.Icon = playerController.PlayerStats.collectedGems[i].GetIconSprite();
                gemStatus.Name = playerController.PlayerStats.collectedGems[i].name;
                gemStatus.CollectedCount = playerController.PlayerStats.collectedGems[i].count;
            }
        }
    }
}
