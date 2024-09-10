using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{

    public void LoadMap()
    {
        SceneManager.LoadScene("Map");
    }

    public void Save()
    {
        Debug.Log("Saved");
        GameManager.manager.Save(); 
    }

    public void Load()
    {
        Debug.Log("Loaded");
        GameManager.manager.Load();
    }
}