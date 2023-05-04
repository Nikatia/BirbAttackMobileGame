using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private GameObject saveData;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);        
    }

    public void Restart()
    {
        saveData = GameObject.Find("SaveData");
        Destroy(saveData);
        SceneManager.LoadScene("1stChallenge");
    }

    public void doExitGame()
    {
        Application.Quit();
    }
}