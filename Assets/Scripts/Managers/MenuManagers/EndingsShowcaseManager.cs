using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingsShowcaseManager : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
