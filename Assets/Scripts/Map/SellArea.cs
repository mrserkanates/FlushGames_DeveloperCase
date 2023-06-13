using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellArea : MonoBehaviour
{

    [SerializeField] private float sellDuration; // time needed for selling a single gem
    private float elapsedTime; // elapsed time before a gem is selled

    private void OnTriggerStay(Collider other)
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime < sellDuration)
            return;

        if (other.gameObject.tag.Equals("Player"))
        {
            if (other.gameObject.TryGetComponent<GemCollector>(out GemCollector gemCollector))
            {
                gemCollector.Sell();
                elapsedTime = 0; // reset elapsed time
            }
        }
    }
}
