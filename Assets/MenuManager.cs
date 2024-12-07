using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject creditsUI;
    public GameObject instructionsUI;  // Added instructions UI reference

    public void ShowCredits()
    {
        Debug.Log("Showing credits");
        mainMenuUI.SetActive(false);
        creditsUI.SetActive(true);
    }

    public void ShowInstructions()  // New method to show instructions
    {
        Debug.Log("Showing instructions");
        mainMenuUI.SetActive(false);
        instructionsUI.SetActive(true);
    }

    public void ShowMainMenu()
    {
        Debug.Log("Showing main menu");
        creditsUI.SetActive(false);
        instructionsUI.SetActive(false);  // Added to handle instructions panel
        mainMenuUI.SetActive(true);
    }
}