using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public readonly int defaultLastLevel = 1;
    private int _levelIndex;
    private static bool _loaded = false;
    [SerializeField] private TextMeshProUGUI levelText;
    private static int _currentLevel=1;

    private void Awake()
    {
        _levelIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void Start()
    {
        levelText.text = "Level:" + _currentLevel.ToString();
        LoadGame();
        SaveGame();
        PlayerPrefs.SetInt("CurrentLevel", _currentLevel);
    }

    public void RestartGame()
    {
        //Restart level.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextLevel(string newGameLevel)
    {
        //Save game and level text for next level.
        _currentLevel++;
 
        SceneManager.LoadScene(newGameLevel);
        _levelIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("GameSaved");
    }
    public void LoadGame()
    {
        //Load game on startup.
        if (!_loaded)
        {
            _loaded = true;
            _levelIndex = PlayerPrefs.GetInt("SavedScene", defaultLastLevel);
            SceneManager.LoadScene(_levelIndex);
            _currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        }
    }
    public void SaveGame()
    {
        //Save game.
        PlayerPrefs.SetInt("SavedScene", _levelIndex);
    }
    public void ResetWholeGame()
    {
        SceneManager.LoadScene("Level1");
        _currentLevel = 1;
    }
}
