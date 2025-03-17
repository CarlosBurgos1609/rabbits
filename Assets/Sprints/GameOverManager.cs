using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance; // Singleton para acceso global
    public Font customFont; // Fuente personalizada para los textos

    private GameObject gameOverPanel;
    private Button restartButton;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        CreateGameOverUI(); // Crear el panel de Game Over en tiempo de ejecución
    }

    void CreateGameOverUI()
    {
        // Crear el Canvas
        GameObject canvasGO = new GameObject("GameOverCanvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasGO.AddComponent<GraphicRaycaster>();

        // Crear el Panel de Game Over
        gameOverPanel = new GameObject("GameOverPanel");
        gameOverPanel.transform.SetParent(canvasGO.transform, false);
        Image panelImage = gameOverPanel.AddComponent<Image>();
        panelImage.color = new Color(0, 0, 0, 0.7f); // Fondo semi-transparente

        RectTransform panelRT = gameOverPanel.GetComponent<RectTransform>();
        panelRT.sizeDelta = new Vector2(600, 400);
        panelRT.anchoredPosition = Vector2.zero;

        // Crear el Texto de "Game Over"
        GameObject textGO = new GameObject("GameOverText");
        textGO.transform.SetParent(gameOverPanel.transform, false);
        Text gameOverText = textGO.AddComponent<Text>();
        gameOverText.text = "GAME OVER";
        gameOverText.font = customFont != null ? customFont : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        gameOverText.fontSize = 50;
        gameOverText.alignment = TextAnchor.MiddleCenter;
        gameOverText.color = Color.red;

        RectTransform textRT = textGO.GetComponent<RectTransform>();
        textRT.sizeDelta = new Vector2(400, 100);
        textRT.anchoredPosition = new Vector2(0, 100);

        // Crear el botón de "Reintentar"
        GameObject buttonGO = new GameObject("RestartButton");
        buttonGO.transform.SetParent(gameOverPanel.transform, false);
        restartButton = buttonGO.AddComponent<Button>();
        Image buttonImage = buttonGO.AddComponent<Image>();
        buttonImage.color = Color.white;

        RectTransform buttonRT = buttonGO.GetComponent<RectTransform>();
        buttonRT.sizeDelta = new Vector2(200, 50);
        buttonRT.anchoredPosition = new Vector2(0, -50);

        // Crear el Texto del botón
        GameObject buttonTextGO = new GameObject("RestartButtonText");
        buttonTextGO.transform.SetParent(buttonGO.transform, false);
        Text buttonText = buttonTextGO.AddComponent<Text>();
        buttonText.text = "Reintentar";
        buttonText.font = customFont != null ? customFont : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        buttonText.fontSize = 30;
        buttonText.alignment = TextAnchor.MiddleCenter;
        buttonText.color = Color.black;

        RectTransform buttonTextRT = buttonTextGO.GetComponent<RectTransform>();
        buttonTextRT.sizeDelta = new Vector2(200, 50);
        buttonTextRT.anchoredPosition = Vector2.zero;

        // Asignar la función de reinicio al botón
        restartButton.onClick.AddListener(RestartGame);

        // Ocultar el panel al inicio
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reiniciar escena
    }
}
