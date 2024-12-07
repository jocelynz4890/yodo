using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject creditsUI;
    public GameObject instructionsUI;  // Added instructions UI reference

    public void ShowCredits()
    {
        mainMenuUI.SetActive(false);
        creditsUI.SetActive(true);
    }

    public void ShowInstructions()  // New method to show instructions
    {
        mainMenuUI.SetActive(false);
        instructionsUI.SetActive(true);
    }

    public void ShowMainMenu()
    {
        creditsUI.SetActive(false);
        instructionsUI.SetActive(false);  // Added to handle instructions panel
        mainMenuUI.SetActive(true);
    }
}