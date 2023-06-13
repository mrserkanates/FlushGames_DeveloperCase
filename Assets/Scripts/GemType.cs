using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GemType", menuName = "ScriptableObjects/GemType", order = 1)]
public class GemType : ScriptableObject
{
    public string name;
    public int initialSalePrice;
    public Sprite icon;
    public GameObject prefab;
}