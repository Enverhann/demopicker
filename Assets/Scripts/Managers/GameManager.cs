using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerController _playerController;
    private Buttons _buttons;

    private void Awake()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _buttons = GameObject.FindGameObjectWithTag("Buttons").GetComponent<Buttons>();
    }

    void Update()
    {
        if (!_playerController._canMove && Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
    }
    public void StartGame()
    {
        _buttons.tap.SetActive(false);
        _playerController._canMove = true;
        this.gameObject.SetActive(false);
    }
}
