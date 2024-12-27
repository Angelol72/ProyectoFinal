using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuChildManager : MonoBehaviour
{
    public void EscenaPeaton()
    {
        SceneManager.LoadScene("EscenaChild");
    }

    public void EscenaConduccion()
    {
        SceneManager.LoadScene("EscenaCar");
    }

    public void Salir()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
