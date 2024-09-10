using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager manager;

    public string currentLevel;

    private void Awake()
    {

        // Singleton
        // Tarkistetaan onko olemassa manageria
        if (manager == null)
        {
            // Jos ei ole, niin t�m� on manager
            // Ei tuhota manageria, kun vaihdetaan scene�
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else
        {
            // Jos on, niin tuhotaan t�m�
            // Koska manager on jo olemassa
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("MainMenu");
        }

    }
}
