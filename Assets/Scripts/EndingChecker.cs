using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingChecker : MonoBehaviour
{
    public List<EndingsScriptableObject> EndingsScriptableObjectList;

    public Transform EndingsParent;

    public GameObject EndingPrefab;

    private void Start()
    {
        foreach (var ending in EndingsScriptableObjectList)
        {
            Transform instantiatedEndingTransform = Instantiate(EndingPrefab, EndingsParent, false).transform;

            instantiatedEndingTransform.GetChild(1).GetComponent<Image>().sprite = ending.EndingSprite;
            instantiatedEndingTransform.GetChild(2).GetComponent<TMP_Text>().text = ending.EndingName;
            
            if(ending.isGathered)
            {
                instantiatedEndingTransform.GetChild(3).gameObject.SetActive(true);
            }
            else
            {
                instantiatedEndingTransform.GetChild(3).gameObject.SetActive(false);
            }
        }
    }
}
