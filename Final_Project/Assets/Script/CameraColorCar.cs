using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class CameraColorCar : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    private ColorGrading colorGrading;

    private Color green = new Color(0.5f, 1.0f, 0.5f); // Verde claro
    private Color yellow = new Color(1.0f, 1.0f, 0.5f); // Amarillo claro
    private Color red = new Color(1.0f, 0.5f, 0.5f);   // Rojo claro

    public float greenDuration = 3.0f; // Duración en verde
    public float yellowDuration = 1.5f; // Duración en amarillo
    public float redDuration = 3.5f; // Duración en rojo

    private float timer = 0.0f;
    private string currentState = "Green"; // Estado inicial

    public GameObject player; // Asignar al jugador en el Inspector
    private bool isRed = false; // Indica si el color actual es rojo

    private Vector3 lastPosition; // Última posición del jugador antes de entrar en rojo
    private bool canCheckMovement = false; // Controla si se debe verificar el movimiento del jugador

    void Start()
    {
        // Asegúrate de que el Post Process Volume tenga Color Grading
        if (postProcessVolume.profile.TryGetSettings(out colorGrading))
        {
            colorGrading.colorFilter.value = green; // Comenzar en verde
        }

        lastPosition = player.transform.position; // Guardamos la posición inicial
    }

    void Update()
    {
        timer += Time.deltaTime;

        switch (currentState)
        {
            case "Green":
                if (timer >= greenDuration)
                {
                    ChangeState("Yellow", yellow);
                }
                break;

            case "Yellow":
                if (timer >= yellowDuration)
                {
                    ChangeState("Red", red);
                    isRed = true; // Ahora estamos en rojo
                    canCheckMovement = true; // Ahora podemos empezar a verificar movimiento
                    lastPosition = player.transform.position; // Guardamos la posición al cambiar a rojo
                }
                break;

            case "Red":
                if (timer >= redDuration)
                {
                    ChangeState("Green", green);
                    isRed = false; // Volver al estado no rojo
                    canCheckMovement = false; // Detener la verificación de movimiento
                }

                // Verifica si el jugador se mueve mientras está en rojo
                if (canCheckMovement && HasPlayerMoved())
                {
                    Debug.Log("¡Movimiento en rojo! Reiniciando...");
                    RestartLevel();
                }
                break;
        }
    }

    private void ChangeState(string newState, Color newColor)
    {
        timer = 0.0f;
        currentState = newState;
        colorGrading.colorFilter.value = newColor;
    }

    private bool HasPlayerMoved()
    {
        // Verifica si el jugador se ha movido comparando su posición actual con la anterior
        if (Vector3.Distance(lastPosition, player.transform.position) > 0.1f)
        {
            lastPosition = player.transform.position; // Actualiza la última posición
            return true; // El jugador se ha movido
        }

        return false; // El jugador no se ha movido
    }

    private void RestartLevel()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia el nivel actual
        Time.timeScale = 1f;
    }
}
