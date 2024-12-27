using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void EscenaAdulto()
    {
        SceneManager.LoadScene("NombreDeLaEscenaAdulto");
    }

    public void EscenaChild()
    {
        SceneManager.LoadScene("MenuChild");
    }

    public void Salir()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Solo para el editor de Unity
        #endif
    }
}
