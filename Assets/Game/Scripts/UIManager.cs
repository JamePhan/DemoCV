using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button btn_menu;
    [SerializeField] private Button btn_resume;
    [SerializeField] private Button btn_playAgain;
    [SerializeField] private Button btn_quit;

    [Header("Panel")]
    [SerializeField] private GameObject panel_menu;

    public delegate void GamePauseDelegate();
    public static event GamePauseDelegate gamePauseDelegate;
    public delegate void GameResumeDelegate();
    public static event GameResumeDelegate gameResumeDelegate;

    void Start()
    {
        btn_menu.onClick.AddListener(GamePause);
        btn_resume.onClick.AddListener(GameResume);
        btn_playAgain.onClick.AddListener(PlayAgain);
        btn_quit.onClick.AddListener(QuitGame);
    }

    public void GamePause()
    {
        gamePauseDelegate?.Invoke();
        panel_menu.SetActive(true);
        Time.timeScale = 0;
    }

    public void GameResume()
    {
        gameResumeDelegate?.Invoke();
        panel_menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("StartScene");
    }
}
