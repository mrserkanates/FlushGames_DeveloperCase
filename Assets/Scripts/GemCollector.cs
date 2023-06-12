using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollector : MonoBehaviour
{
    [SerializeField] private GameObject stackParent;
    [SerializeField] private float gemInterval;
    [SerializeField] private float collectedGemScale;

    private Stack<Gem> gemStack;

    private void Awake()
    {
        gemStack = new Stack<Gem>();
    }

    public void Collect(Gem gem)
    {
        Vector3 lastStackPos;

        if (gemStack.Count == 0)
            lastStackPos = stackParent.transform.position;
        else
            lastStackPos = gemStack.Peek().transform.position;

        gem.GetCollected();

        gem.transform.position = lastStackPos + Vector3.up * gemInterval;
        gem.transform.localScale = new Vector3(collectedGemScale, collectedGemScale, collectedGemScale);

        gem.transform.parent = stackParent.transform;
        gemStack.Push(gem);

    }

    public void Sell()
    {
        if (gemStack.Count == 0)
            return;

        Gem lastGem = gemStack.Peek();
        gemStack.Pop();
        Destroy(lastGem.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Gem"))
        {
            if (other.gameObject.TryGetComponent<Gem>(out Gem gem))
            {
                if (gem.IsCollectable())
                    Collect(other.gameObject.GetComponent<Gem>());
            }
        }
    }

}
