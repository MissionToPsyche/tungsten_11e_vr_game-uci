using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public Transform attachPoint;

    private void Update() {
        Vector3 offset = attachPoint.position + new Vector3(0, 0.2f, 0); ;
        transform.position = offset;
    }

    public void Exit()
    {
        SceneManager.LoadScene("Game Over");
    }
    public void ReloadScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
