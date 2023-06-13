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
    [SerializeField] private float minScaleToCollect = 0.25f; // minimum scale needed for collecting this gem
    [SerializeField] private Vector3 minScale;
    [SerializeField] private Vector3 maxScale;

    private Tweener scaleTweener;

    private void Start()
    {
        transform.localScale = minScale; // set scale to minimum scale at beginning
        scaleTweener = transform.DOScale(maxScale, scalingDuration); // save tweener to be able to kill it afterwards
    }

    public bool IsCollectable()
    {
        if (GetNormalizedScale() >= minScaleToCollect)
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
            parentTile.SpawnRandomGem(); // spawn new gem instead of this collected gem
            scaleTweener.Kill(); // stop scaling since it is collected
        }
    }

    private float GetNormalizedScale()
    {
        // normalizes current scale according to min and max scale
        // returns average of normalized scale axises.
        Vector3 currentScale = transform.localScale;
        float normalizedX = Mathf.InverseLerp(minScale.x, maxScale.x, currentScale.x);
        float normalizedY = Mathf.InverseLerp(minScale.y, maxScale.y, currentScale.y);
        float normalizedZ = Mathf.InverseLerp(minScale.z, maxScale.z, currentScale.z);

        float averageNormalizedScale = (normalizedX + normalizedY + normalizedZ) / 3f;
        return averageNormalizedScale;
    }

    public int GetGemValue()
    {
        // returns the gold value of the gem
        return InitialSalePrice + (int)(GetNormalizedScale() * 100);
    }

    public byte[] GetIconByteArray()
    {
        // converts sprite into byte array
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
