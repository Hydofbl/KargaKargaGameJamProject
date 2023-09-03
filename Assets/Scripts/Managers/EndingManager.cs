using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    [SerializeField] private TMP_Text endingName;
    [SerializeField] private TMP_Text endingText;
    [SerializeField] private Image endingImage;

    [SerializeField] private GameObject endingPage;

    private static EndingManager _instance;
    public static EndingManager Instance { get => _instance; }

    private void Start()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    public void SetEndingPage(EndingsScriptableObject ending)
    {
        endingName.text = ending.name;
        endingText.text = ending.EndingText;
        endingImage.sprite = ending.EndingSprite;
    }

    public void ActivateEnding()
    {
        endingPage.SetActive(true);
    }
}
