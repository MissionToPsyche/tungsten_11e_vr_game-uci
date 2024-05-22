using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public Transform attachPoint;
    public GameObject settingsMenu;

    private void Update() {
        Vector3 offset = attachPoint.position + new Vector3(0, 0.2f, 0); ;
        transform.position = offset;
    }

    public void OpenSettings() {
        settingsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void ReloadScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
