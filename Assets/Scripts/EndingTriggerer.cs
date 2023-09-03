using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTriggerer : MonoBehaviour
{
    public EndingsScriptableObject Ending;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            EndingManager.Instance.SetEndingPage(Ending);
            EndingManager.Instance.ActivateEnding();
        }
    }
}
