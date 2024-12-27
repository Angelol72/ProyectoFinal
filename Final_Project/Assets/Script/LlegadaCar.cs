using UnityEngine;
using UnityEngine.SceneManagement;  // Necesario para cargar escenas

public class Llegada : MonoBehaviour
{
    // Nombre de la escena que quieres cargar al chocar con el bloque
    public string sceneToLoad;

    // Este método se llama cuando otro objeto con un Collider choca con el bloque
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto que colisionó es el jugador
        if (collision.gameObject.CompareTag("Player")) // Asegúrate de que el jugador tenga el tag "Player"
        {
            // Cargar la nueva escena
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
