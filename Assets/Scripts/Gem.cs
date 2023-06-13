using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gem : MonoBehaviour
{
    private string name;
    private int initialSalePrice;
    private Sprite icon;

    [SerializeField] private float scalingDuration;
    [SerializeField] public Vector3 minScale;
    [SerializeField] public Vector3 maxScale;

    private void Start()
    {
        transform.localScale = minScale;
        transform.DOScale(maxScale, scalingDuration);
    }

    public bool IsCollectable()
    {
        if (GetNormalizedScale() >= 0.25f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GetCollected()
    {
        if (transform.parent.TryGetComponent<Tile>(out Tile parentTile))
        {
            parentTile.SpawnRandomGem();
            DOTween.Kill(this);
        }
    }

    private float NormalizeScale(Vector3 currentScale, Vector3 minScale, Vector3 maxScale)
    {
        float normalizedX = Mathf.InverseLerp(minScale.x, maxScale.x, currentScale.x);
        float normalizedY = Mathf.InverseLerp(minScale.y, maxScale.y, currentScale.y);
        float normalizedZ = Mathf.InverseLerp(minScale.z, maxScale.z, currentScale.z);

        float averageNormalizedScale = (normalizedX + normalizedY + normalizedZ) / 3f;
        return averageNormalizedScale;
    }

    public int GetGemValue()
    {
        return InitialSalePrice + (int)(GetNormalizedScale() * 100);
    }

    private float GetNormalizedScale()
    {
        Vector3 currentScale = transform.localScale;
        return NormalizeScale(currentScale, minScale, maxScale);
    }

    public byte[] GetIconByteArray()
    {
        Texture2D texture = icon.texture;
        byte[] textureData = texture.EncodeToPNG();
        return textureData;
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int InitialSalePrice
    {
        get { return initialSalePrice; }
        set { initialSalePrice = value; }
    }

    public Sprite Icon
    {
        get { return icon; }
        set { icon = value; }   
    }
}
