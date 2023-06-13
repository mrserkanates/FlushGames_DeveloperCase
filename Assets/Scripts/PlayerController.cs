using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerStats playerStats = new PlayerStats();

    public PlayerStats PlayerStats
    {
        get { return playerStats; }
        set { playerStats = value; }
    }
}
