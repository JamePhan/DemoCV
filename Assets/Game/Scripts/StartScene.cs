using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    public Button ButtonStart;
    //public Button ButtonSettings;
    public Button ButtonQuit;

    public GameObject Panel_ChooseCharacter;

    void Start()
    {
        ButtonStart.onClick.AddListener(ChooseCharacter);
        ButtonQuit.onClick.AddListener(Quit);
    }

    public void ChooseCharacter()
    {
        Panel_ChooseCharacter.SetActive(true);
    }

    public void StartGame()
    {
        //SceneManager.LoadScene()
    }

    public void Quit()
    {
        Application.Quit();
    }
}
