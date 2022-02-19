using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameController Instance;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void newGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("level1");
    }

    public void gameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("gameOver");
    }

    public void win()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            UnityEngine.SceneManagement.SceneManager.LoadScene("win");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
