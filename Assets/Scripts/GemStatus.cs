using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GemStatus : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI collectedCountText;

    private Sprite icon;
    private string name;
    private int collectedCount;

    public Sprite Icon
    {
        get { return icon; }
        set { 
            icon = value;
            iconImage.sprite = icon;
        }
    }

    public string Name
    {
        get { return name; }
        set { 
            name = value;
            nameText.text = "Name: " + name;
        }
    }

    public int CollectedCount
    {
        get { return collectedCount; }
        set { 
            collectedCount = value;
            collectedCountText.text = "Collected Count: " + collectedCount;
        }
    }
}
