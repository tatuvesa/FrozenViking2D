using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string levelToLoad;

    void Start()
    {
        
    }

    public void Cleared(bool isClear)
    {
        if (isClear)
        {
            // laitetaan levelcleared kyltti n‰kyviin
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
            // taso p‰‰sty l‰pi laitetaan circle collider pois p‰‰lt‰
            GetComponent<CircleCollider2D>().enabled = false;

        }
    }
}
