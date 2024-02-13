using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  
    public static GameManager Instance;

    private void Awake()
    {
    
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return; 
        }
        else
        {
            Destroy(Instance);
        }
    }


    #region On ButtonCliks
    public void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene(GameMenuStrings.MainMenu);
    }

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(GameMenuStrings.Level_1);
    }
    public void LoadNextScene(int scene)
    {
        SceneManager.LoadScene(GameMenuStrings.AllLevel+scene.ToString());
    }
    #endregion
}
