using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (GameManager.instance != null) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponSprices;

    public Player player;

    public int gold;
    public int xp;

    public void SaveState()
    {
        string state = "";

        state += "0|";
        state += gold.ToString() + "|";
        state += xp.ToString() + "|";
        state += "0";

        PlayerPrefs.SetString("SaveState", state);
    }

    public void LoadState(Scene scene, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState")) {
            return;
        }

        string[] state = PlayerPrefs.GetString("SaveState").Split('|');

        gold = int.Parse(state[1]);
        xp = int.Parse(state[2]);
    }
}
