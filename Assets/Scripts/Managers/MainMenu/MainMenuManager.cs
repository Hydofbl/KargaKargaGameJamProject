using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private GameObject _optionsUI;

    //Starts New Game
    public void StartNewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Opens Options Menu
    public void OpenOptions()
    {
        _optionsUI.SetActive(true);
    }

    //Closes Game
    public void ExitGame()
    {
        Application.Quit();
    }
}
