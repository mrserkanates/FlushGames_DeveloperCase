using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CollectedGemInfo
{
    public byte[] iconData;
    public string name;
    public int count;

    public CollectedGemInfo(byte[] iconData, string name, int count)
    {
        this.iconData = iconData;
        this.name = name;
        this.count = count;
    }

    public Sprite GetIconSprite()
    {
        Texture2D texture = new Texture2D(512, 512);
        texture.LoadImage(iconData);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
        return sprite;
    }
}

[Serializable]
public class PlayerStats
{
    public int golds;
    public List<CollectedGemInfo> collectedGems;

    public PlayerStats()
    {
        collectedGems = new List<CollectedGemInfo>();
    }

    public void AddGolds(int amount)
    {
        golds += amount;
    }

    public void AddCollectedGem(Gem gem)
    {
        for(int i = 0; i < collectedGems.Count; i++)
        {
            if (collectedGems[i].name.Equals(gem.Name))
            {
                collectedGems[i].count += 1;
                return;
            }
        }
        collectedGems.Add(new CollectedGemInfo(gem.GetIconByteArray(), gem.Name, 1));
    }
}