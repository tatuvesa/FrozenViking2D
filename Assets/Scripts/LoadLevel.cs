using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LevelToLoad()
    {
        SceneManager.LoadScene("Level1");
    }
}
