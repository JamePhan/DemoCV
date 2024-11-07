using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseCharacter : MonoBehaviour
{
    public Sprite nonSelected;
    public Sprite selected;
    public List<CharacterDisplay> charDisplayList;

    public Image charDisplay1;
    public Image charDisplay2;
    public Image charDisplay3;
    public Image img1;
    public Image img2;
    public Image img3;
    public TextMeshProUGUI name1;
    public TextMeshProUGUI name2;
    public TextMeshProUGUI name3;
    public TextMeshProUGUI des1;
    public TextMeshProUGUI des2;
    public TextMeshProUGUI des3;

    public Button btnCharDisplay1;
    public Button btnCharDisplay2;
    public Button btnCharDisplay3;

    public Button btnLeft;
    public Button btnRight;
    public Button btnClose;
    public Button btnStart;

    public int startPosition;
    public int endPosition;
    private int count;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        charDisplayList = new List<CharacterDisplay>();
        startPosition = 0;
        endPosition = 2;
        LoadListCharacter();
        LoadPanel();
        btnLeft.onClick.AddListener(LoadPrevListCharacter);
        btnRight.onClick.AddListener(LoadNextListCharacter);
        btnClose.onClick.AddListener(Close);
        btnCharDisplay1.onClick.AddListener(SelectCharacter1);
        btnCharDisplay2.onClick.AddListener(SelectCharacter2);
        btnCharDisplay3.onClick.AddListener(SelectCharacter3);
        btnStart.onClick.AddListener(StartGame);
    }

    public void LoadListCharacter()
    {
        CharacterDisplaySO[] arrCharDis = Resources.LoadAll<CharacterDisplaySO>("CharacterDisplays");
        foreach (CharacterDisplaySO so in arrCharDis)
        {
            CharacterDisplay charDis = new CharacterDisplay(so.Avatar, so.Name, so.Description, so.IsUnlock);
            charDisplayList.Add(charDis);
        }
        count = charDisplayList.Count;
    }

    public void LoadPanel()
    {
        img1.sprite = charDisplayList[startPosition].Avatar;
        name1.text = charDisplayList[startPosition].Name;
        des1.text = charDisplayList[startPosition].Description;

        img2.sprite = charDisplayList[startPosition + 1].Avatar;
        name2.text = charDisplayList[startPosition + 1].Name;
        des2.text = charDisplayList[startPosition + 1].Description;

        img3.sprite = charDisplayList[endPosition].Avatar;
        name3.text = charDisplayList[endPosition].Name;
        des3.text = charDisplayList[endPosition].Description;

        charDisplay1.sprite = nonSelected;
        charDisplay2.sprite = nonSelected;
        charDisplay3.sprite = nonSelected;
        btnStart.interactable = false;
    }

    public void LoadPrevListCharacter()
    {
        startPosition = startPosition - 3;
        if (startPosition - count < 0)
        {
            startPosition = 0;
        }
        endPosition = startPosition + 2;
        LoadPanel();
    }

    public void LoadNextListCharacter()
    {
        endPosition = endPosition + 3;
        if(endPosition - count > 0)
        {
            endPosition = charDisplayList.Count - 1;
        }
        startPosition = endPosition - 2;
        LoadPanel();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void SelectCharacter1()
    {
        charDisplay1.sprite = selected;
        charDisplay2.sprite = nonSelected;
        charDisplay3.sprite = nonSelected;
        string characterName = charDisplayList[startPosition].Name.Replace(" ", "");
        PlayerPrefs.SetString("CharacterName", characterName);
        btnStart.interactable = true;
    }

    public void SelectCharacter2()
    {
        charDisplay2.sprite = selected;
        charDisplay1.sprite = nonSelected;
        charDisplay3.sprite = nonSelected;
        string characterName = charDisplayList[startPosition + 1].Name.Replace(" ", "");
        PlayerPrefs.SetString("CharacterName", characterName);
        btnStart.interactable = true;
    }

    public void SelectCharacter3()
    {
        charDisplay3.sprite = selected;
        charDisplay2.sprite = nonSelected;
        charDisplay1.sprite = nonSelected;
        string characterName = charDisplayList[endPosition].Name.Replace(" ", "");
        PlayerPrefs.SetString("CharacterName", characterName);
        btnStart.interactable = true;
    }

}
