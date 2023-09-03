using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        InputManager.Instance.OnPassButtonPressed += Instance_OnPassButtonPressed;
    }

    private void Instance_OnPassButtonPressed()
    {
        if(endingPage.activeSelf)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void SetEndingPage(EndingsScriptableObject ending)
    {
        endingName.text = ending.name;
        endingText.text = ending.EndingText;
        endingImage.sprite = ending.EndingSprite;

        ending.isGathered = true;
    }

    public void ActivateEnding()
    {
        endingPage.SetActive(true);
    }
}
