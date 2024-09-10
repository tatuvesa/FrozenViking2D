using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string levelToLoad;

    void Start()
    {
        // katsotaan aina map scene avattaessa, ett‰ onko gamemanagerissa tietosiit‰, ett‰ jokin taso on p‰‰sty l‰pi
        // jos on, niin ajetaan levelcleared funktio
        if (GameManager.manager.GetType().GetField(levelToLoad).GetValue(GameManager.manager).ToString() == "True")
        {
            //T‰m‰ taso on p‰‰sty l‰pi
            Cleared(true);
        }
    }

    public void Cleared(bool isClear)
    {
        if (isClear)
        {
            // asetetaan gamemanageriin tiedot, ett‰ t‰m‰ taso on p‰‰sty l‰pi esim level 1 = true
            GameManager.manager.GetType().GetField(levelToLoad).SetValue(GameManager.manager, true);
            // laitetaan levelcleared kyltti n‰kyviin
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
            // taso p‰‰sty l‰pi laitetaan circle collider pois p‰‰lt‰
            GetComponent<CircleCollider2D>().enabled = false;

        }
    }
}
