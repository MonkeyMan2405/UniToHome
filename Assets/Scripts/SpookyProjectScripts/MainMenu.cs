using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject MenuToDisable;

    public void PlayGame()
    {
        SceneManager.LoadScene("MonsterGame", LoadSceneMode.Single);
        SceneManager.SetActiveScene(SceneManager.GetActiveScene());
        Debug.Log("Clicked");


    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MonsterGameMenu", LoadSceneMode.Single);
        SceneManager.SetActiveScene(SceneManager.GetActiveScene());
    }

    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        MenuToDisable.SetActive(false);
    }

}
