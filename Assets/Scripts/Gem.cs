using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gem : MonoBehaviour
{

    private string name;
    private float initialSalePrice;
    private Sprite icon;

    [SerializeField] private float scalingDuration;
    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;

    private void Start()
    {
        transform.localScale = new Vector3(minScale, minScale, minScale);
        transform.DOScale(new Vector3(maxScale, maxScale, maxScale), scalingDuration);
    }

    public bool IsCollectable()
    {
        if (transform.localScale.x >= maxScale * 0.25f)
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
            Debug.Log("waakingu");
            parentTile.SpawnRandomGem();
            DOTween.Kill(this);
        }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public float InitialSalePrice
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
