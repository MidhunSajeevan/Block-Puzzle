using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    Button[] buttons;
    public GameObject LevelButtons;
    private void Awake()
    {
        Time.timeScale = 1f;
        ButtonsToArray();

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }
       
    }
    public void OpenNewLevel(int levelId)
    {
        Debug.Log("Button Clicked");
        string levelname = GameMenuStrings.AllLevel + levelId;
        SceneManager.LoadScene(levelname);
    }

    void ButtonsToArray()
    {
        int childcount = LevelButtons.transform.childCount;
        buttons = new Button[childcount];
        for (int i = 0; i < childcount; i++)
        {
            buttons[i] = LevelButtons.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}
