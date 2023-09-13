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

    [Header("Be Choose")]
    [SerializeField] private GameObject newGameBeChoose;
    [SerializeField] private GameObject continueGameBeChoose;
    [SerializeField] private GameObject loadGameBeChoose;
    [SerializeField] private GameObject quitGameBeChoose;

    private void Start()
    {
        DisableButtonsDependingOnData();
    }

    private void DisableButtonsDependingOnData()
    {
        if (!DataPersistenceManager.Instance.HasGameData())
        {
            continueGameButton.interactable = false;
            loadGameButton.interactable = false;
        }
    }

    public void OnNewGameClicked()
    {
        saveSlotsMenu.ActivateMenu(false);
        this.DeactivateMenu();
    }

    public void OnLoadGameClicked()
    {
        saveSlotsMenu.ActivateMenu(true);
        this.DeactivateMenu();
    }

    public void OnContinueGameClicked()
    {
        DisableMenuButtons();
        DataPersistenceManager.Instance.SaveGame();
        SceneManager.LoadSceneAsync("1F");
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
        loadGameBeChoose.SetActive(true);
    }

    public void OnPointEnterContinueGame()
    {
        continueGameBeChoose.SetActive(true);
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
