using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class CameraColorChild : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    private ColorGrading colorGrading;

    private Color red = new Color(1.0f, 0.5f, 0.5f);   // Rojo claro
    private Color green = new Color(0.5f, 1.0f, 0.5f); // Verde claro
    private Color yellow = new Color(1.0f, 1.0f, 0.5f); // Amarillo claro

    public float redDuration = 3.5f;   // Duración en rojo
    public float greenDuration = 2.5f; // Duración en verde
    public float yellowDuration = 1.0f; // Duración en amarillo

    private float timer = 0.0f;
    private string currentState = "Red"; // Estado inicial en rojo

    public GameObject player;  // Asigna al jugador en el Inspector
    private bool canMove = true; // Controla si el jugador puede moverse

    private Vector3 lastPosition; // Última posición del jugador antes de cada cambio de color
    private bool canCheckMovement = false; // Si se debe verificar el movimiento del jugador
    private Vector3 startPositionGreenYellow; // Posición cuando está en verde o amarillo
    private bool checkingMovement = false; // Indica si se debe verificar el movimiento

    void Start()
    {
        // Asegúrate de que el Post Process Volume tenga Color Grading
        if (postProcessVolume.profile.TryGetSettings(out colorGrading))
        {
            colorGrading.colorFilter.value = red; // Comenzar en rojo
        }

        lastPosition = player.transform.position; // Guardamos la posición inicial
    }

    void Update()
    {
        timer += Time.deltaTime;

        switch (currentState)
        {
            case "Red":
                if (timer >= redDuration)
                {
                    ChangeState("Green", green);
                    canMove = false;  // El jugador no puede moverse en verde
                    checkingMovement = true; // Empezamos a comprobar el movimiento
                    startPositionGreenYellow = player.transform.position; // Guardamos la posición al empezar verde
                }
                break;

            case "Green":
                if (timer >= greenDuration)
                {
                    ChangeState("Yellow", yellow);
                    checkingMovement = true; // Empezamos a comprobar el movimiento también en amarillo
                    startPositionGreenYellow = player.transform.position; // Guardamos la posición al cambiar a amarillo
                    canMove = false;  // El jugador no puede moverse en amarillo
                }
                break;

            case "Yellow":
                if (timer >= yellowDuration)
                {
                    ChangeState("Red", red);
                    checkingMovement = false; // Detenemos la verificación de movimiento al cambiar a rojo
                    canMove = true; // El jugador puede moverse nuevamente en rojo
                }
                break;
        }

        // Verifica si el jugador intentó moverse durante verde o amarillo
        if (checkingMovement && HasPlayerMoved(startPositionGreenYellow))
        {
            Debug.Log("¡Movimiento no permitido! Reiniciando...");
            RestartLevel();
        }
    }

    private void ChangeState(string newState, Color newColor)
    {
        timer = 0.0f;
        currentState = newState;
        colorGrading.colorFilter.value = newColor;
    }

    private bool HasPlayerMoved(Vector3 startPos)
    {
        // Verifica si el jugador se ha movido comparando su posición inicial con la actual
        if (Vector3.Distance(startPos, player.transform.position) > 0.1f)
        {
            return true; // El jugador se ha movido
        }

        return false; // El jugador no se ha movido
    }

    private void RestartLevel()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia el nivel actual

        // Reanudar el tiempo después del reinicio
        Time.timeScale = 1f;
    }
}
