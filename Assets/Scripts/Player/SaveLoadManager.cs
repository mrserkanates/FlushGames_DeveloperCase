using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager instance;

    private string saveFileName = "playerStats.dat";

    private void OnEnable()
    {
        EventManager.StartListening(Events.OnCollectGem, SavePlayerStats);
        EventManager.StartListening(Events.OnSellGem, SavePlayerStats);
    }

    private void OnDisable()
    {
        EventManager.StopListening(Events.OnCollectGem, SavePlayerStats);
        EventManager.StopListening(Events.OnSellGem, SavePlayerStats);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            LoadPlayerStats();
        }
        else
        {
            Destroy(this);
        }
    }

    private void SavePlayerStats(Dictionary<string, object> message)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerController;

        if (!player.TryGetComponent<PlayerController>(out playerController))
            return;

        PlayerStats playerStats = playerController.PlayerStats;
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(Path.Combine(Application.persistentDataPath, saveFileName));
        binaryFormatter.Serialize(fileStream, playerStats);
        fileStream.Close();

        Debug.Log("Player stats saved.");
    }

    private void LoadPlayerStats()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerController;

        if (!player.TryGetComponent<PlayerController>(out playerController))
            return;

        PlayerStats playerStats;

        if (File.Exists(Path.Combine(Application.persistentDataPath, saveFileName)))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(Path.Combine(Application.persistentDataPath, saveFileName), FileMode.Open);
            playerStats = (PlayerStats)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            Debug.Log("Player stats loaded.");
        }
        else
        {
            Debug.Log("Save file not found. Creating new player stats.");

            // default values
            playerStats = new PlayerStats();
            playerStats.golds = 0;
            playerStats.collectedGems = new List<CollectedGemInfo>();
        }
        playerController.PlayerStats = playerStats;
    }
}
