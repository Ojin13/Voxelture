using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameMenu : MonoBehaviour
{

    public string activeItem;
    public GameObject Settings;
    public GameObject SaveGameDialog;
    public GameObject AreYouSureDialog;
    
    
    public void requestMainMenu()
    {
        Settings.SetActive(false);
        AreYouSureDialog.SetActive(false);
        
        if (activeItem == "MENU_request")
        {
            activeItem = "";
            SaveGameDialog.SetActive(false);
        }
        else
        {
            activeItem = "MENU_request";
            SaveGameDialog.SetActive(true);
        }
    }
    
    public void requestGameEnd()
    {
        Settings.SetActive(false);
        SaveGameDialog.SetActive(false);
        
        if (activeItem == "END_request" || activeItem == "SAVE_BEFORE_END")
        {
            activeItem = "";
            AreYouSureDialog.SetActive(false);
        }
        else
        {
            activeItem = "END_request";
            AreYouSureDialog.SetActive(true);
        }
    }
    
    
    public void requestControlls()
    {
        SaveGameDialog.SetActive(false);
        AreYouSureDialog.SetActive(false);

        if (activeItem == "SETTINGS")
        {
            activeItem = "";
            Settings.SetActive(false);
        }
        else
        {
            activeItem = "SETTINGS";
            Settings.SetActive(true);
        }
    }
    
    public void requestSave()
    {
        activeItem = "";
        Settings.SetActive(false);
        SaveGameDialog.SetActive(false);
        AreYouSureDialog.SetActive(false);
        UIController.UIControl.deactivateInGameMenu();
        SaveGameController.GameSaver.SaveGameData();
    }
    
    
    
    
    
    
    public void EndGame()
    {
        Application.Quit();
    }

    
    
    
    
    
    
    
    
    public void yes()
    {
        if (activeItem == "END_request")
        {
            AreYouSureDialog.SetActive(false);
            SaveGameDialog.SetActive(true);
            activeItem = "SAVE_BEFORE_END";
        }
        else if (activeItem == "SAVE_BEFORE_END")
        {
            //save game and than end the game
            SaveGameController.GameSaver.SaveGameData("endGame");
        }
        else if (activeItem == "MENU_request")
        {
            //save game and go to the menu
            SaveGameController.GameSaver.SaveGameData("goToMenu");
        }
    }

    public void no()
    {
        if (activeItem == "END_request")
        {
            activeItem = "";
            AreYouSureDialog.SetActive(false);
        }
        else if (activeItem == "SAVE_BEFORE_END")
        {
            //end without saving
            EndGame();
        }
        else if (activeItem == "MENU_request")
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
