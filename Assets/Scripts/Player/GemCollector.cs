using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollector : MonoBehaviour
{
    [SerializeField] private GameObject stackParent; // parent game object of gems that will be stacked
    [SerializeField] private float gemInterval; // vertical space between stacked gems

    private Stack<Gem> gemStack; // holds stacked gems

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

        gem.transform.position = lastStackPos + Vector3.up *
            (gem.transform.localScale.y + gemInterval); // move the gem onto last gem

        gem.transform.parent = stackParent.transform;
        gemStack.Push(gem);

        EventManager.TriggerEvent(Events.OnCollectGem, null); // call OnCollectGem event
    }

    public void Sell()
    {
        if (gemStack.Count == 0)
            return;

        Gem lastGem = gemStack.Peek();

        PlayerController playerController;

        if (!gameObject.TryGetComponent<PlayerController>(out playerController))
            return;

        // update player stats
        playerController.PlayerStats.AddGolds(lastGem.GetGemValue());
        playerController.PlayerStats.AddCollectedGem(lastGem);

        gemStack.Pop(); // remove the gem from stack
        Destroy(lastGem.gameObject); // destroy the gem

        // call OnSellGem event
        EventManager.TriggerEvent(Events.OnSellGem, new Dictionary<string, object> { {"gem", lastGem} });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Gem"))
        {
            if (other.gameObject.TryGetComponent<Gem>(out Gem gem))
            {
                if (gem.IsCollectable())
                {
                    Collect(other.gameObject.GetComponent<Gem>());
                }
            }
        }
    }

}
