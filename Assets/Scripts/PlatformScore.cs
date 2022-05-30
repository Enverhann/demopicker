using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlatformScore : MonoBehaviour
{
    public static PlatformScore Instance;

    [SerializeField] private TextMeshPro scoreText;
    private int _currentScore;
    private PlayerController _playerController;
    [SerializeField] private GameObject armLeft;
    [SerializeField] private GameObject armRight;
    public ScriptableScore _scriptable;
    private Buttons _buttons;
    private bool _isFailed=true;
    [SerializeField] private GameObject platformBorders;
    private BoxCollider _boxCollider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _buttons = GameObject.FindGameObjectWithTag("Buttons").GetComponent<Buttons>();
        _boxCollider = GetComponent<BoxCollider>();
        scoreText.text = $"{_currentScore}/{_scriptable.necessaryScore}";
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball")) CountBalls();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            _playerController._canMove=false;
            StartCoroutine(ZeroBalls());
        }
    }
    private void CountBalls()
    {
        _currentScore++;
        scoreText.text = $"{_currentScore}/{_scriptable.necessaryScore}";
        if (_currentScore >= _scriptable.necessaryScore && _isFailed)
        {
            _isFailed = false;
            StartCoroutine(ContinueLevel());
        }else if(_isFailed)  
        {
            StartCoroutine(LevelFailed());
        }
    }

    public IEnumerator ContinueLevel()
    {
        yield return new WaitForSeconds(2.5f);
        transform.DOMoveY(0, 1.5f);
        platformBorders.SetActive(false);
        armLeft.transform.DORotate(new Vector3(0, 0, 90), 2);
        armRight.transform.DORotate(new Vector3(0, 0, -90), 2);
        _boxCollider.enabled = false;
        _isFailed = true;
        _playerController._canMove = true;
    }
    public IEnumerator LevelFailed()
    {
        yield return new WaitForSeconds(2.5f);
        if (_isFailed) {
            _buttons.restartButton.SetActive(true);
            _playerController._canMove = false;
        }     
    }
    public IEnumerator ZeroBalls()
    {
        yield return new WaitForSeconds(2.5f);
        if (_currentScore == 0)
        {
            _buttons.restartButton.SetActive(true);
        }
    }
}

