using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [Header("MenuNavigation")]
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;

    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button quitGameButton;

    [Header("Color")]
    [SerializeField] private RawImage continueGameImage;
    [SerializeField] private RawImage loadGameImage;

    [Header("Be Choose")]
    [SerializeField] private GameObject newGameBeChoose;
    [SerializeField] private GameObject continueGameBeChoose;
    [SerializeField] private GameObject loadGameBeChoose;
    [SerializeField] private GameObject quitGameBeChoose;

    private float newAlpha = 0.5f;

    private void Start()
    {
        DisableButtonsDependingOnData();
    }

    private void DisableButtonsDependingOnData()
    {
        if (!DataPersistenceManager.Instance.HasGameData())
        {
            Color currentColor = continueGameImage.color;
            Color currentColor2 = loadGameImage.color;
            continueGameButton.interactable = false;
            currentColor.a = newAlpha;
            continueGameImage.color = currentColor;
            loadGameButton.interactable = false;
            currentColor2.a = newAlpha;
            loadGameImage.color = currentColor2;
        }
    }

    public void OnNewGameClicked()
    {
        saveSlotsMenu.ActivateMenu(false);
        this.DeactivateMenu();
        newGameBeChoose.SetActive(false);
    }

    public void OnLoadGameClicked()
    {
        saveSlotsMenu.ActivateMenu(true);
        this.DeactivateMenu();
        loadGameBeChoose.SetActive(false);
    }

    public void OnContinueGameClicked()
    {
        DisableMenuButtons();
        DataPersistenceManager.Instance.SaveGame();
        SceneManager.LoadSceneAsync("1-1");
        continueGameBeChoose.SetActive(false);
    }

    public void OnQuitGameClicked()
    {
        Application.Quit();
    }

    public void OnPointEnterNewGame()
    {
        newGameBeChoose.SetActive(true);
    }

    public void OnPointEnterLoadGame()
    {
        if (!loadGameButton.interactable == false)
        {
            loadGameBeChoose.SetActive(true);
        }
    }

    public void OnPointEnterContinueGame()
    {
        if(!continueGameButton.interactable == false)
        {
            continueGameBeChoose.SetActive(true);
        }
    }

    public void OnPointEnterQuitGame()
    {
        quitGameBeChoose.SetActive(true);
    }

    public void OnPointExitNewGame()
    {
        newGameBeChoose.SetActive(false);
    }

    public void OnPointExitLoadGame()
    {
        loadGameBeChoose.SetActive(false);
    }

    public void OnPointExitContinueGame()
    {
        continueGameBeChoose.SetActive(false);
    }

    public void OnPointExitQuitGame()
    {
        quitGameBeChoose.SetActive(false);
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
        DisableButtonsDependingOnData();
    }
    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
