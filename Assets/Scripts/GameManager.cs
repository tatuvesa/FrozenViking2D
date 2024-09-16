using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{

    public static GameManager manager;

    public string currentLevel;

    public float Health; // Hahmon health
    public float previousHealth; // Hahmon edellinen health
    public float maxHealth; // Hahmon maksimi health

    // Jokaista tasoa varten tarvitaan boolean muuttuja. Muuttujan nimi tulee olla sama kuin mapissa olevan trigger objektin nimi 
    // esim . Level1, Level2, Level3
    public bool Level1;
    public bool Level2;
    public bool Level3;
    public bool Level4;
    public bool Level5;

    private void Awake()
    {

        // Singleton
        // Tarkistetaan onko olemassa manageria
        if (manager == null)
        {
            // Jos ei ole, niin t‰m‰ on manager
            // Ei tuhota manageria, kun vaihdetaan scene‰
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else
        {
            // Jos on, niin tuhotaan t‰m‰
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

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
        // Siirret‰‰n tieto gamemanagerista dataan
        data.health = Health;
        data.previousHealth = previousHealth;
        data.maxHealth = maxHealth;
        data.currentLevel = currentLevel;
        data.Level1 = Level1;
        data.Level2 = Level2;
        data.Level3 = Level3;
        data.Level4 = Level4;
        data.Level5 = Level5;
        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        // tarkastetaan onko edes olemassa tiedostoa, josta voidaan ladata dataa
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            // Siirret‰‰n data gamemanageriin
            Health = data.health;
            previousHealth = data.previousHealth;
            maxHealth = data.maxHealth;
            currentLevel = data.currentLevel;
            Level1 = data.Level1;
            Level2 = data.Level2;
            Level3 = data.Level3;
            Level4 = data.Level4;
            Level5 = data.Level5;
        }
    }
}

// Uusi luokka, joka on serialisoitavissa. Pit‰‰ sis‰ll‰‰n vain sen datan mit‰ tallennetaan
[Serializable]
class PlayerData
{
    public string currentLevel;
    public float health;
    public float previousHealth;
    public float maxHealth;
    public bool Level1;
    public bool Level2;
    public bool Level3;
    public bool Level4;
    public bool Level5;
}
