using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState
    {
        Gameplay
    }

    public GameState estadoAtual;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        IniciarJogo();
    }

    public void IniciarJogo()
    {
        estadoAtual = GameState.Gameplay;

        SceneManager.LoadScene("GUI", LoadSceneMode.Additive);

        Debug.Log("Gameplay iniciada");
    }
    
}