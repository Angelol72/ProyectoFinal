using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoMenu : MonoBehaviour
{
    public GameObject canvas;

    void Start()
    {
        Time.timeScale = 0f;
        canvas.SetActive(true);
    }

    public void StartLevel()
    {
        Time.timeScale = 1f;
        canvas.SetActive(false);
    }

    public void ExitLevel()
    {
        SceneManager.LoadScene("MenuChild");
    }
}