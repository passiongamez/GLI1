using GameDevHQ.FileBase.Plugins.FPS_Character_Controller;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioSource _audio;
    [SerializeField] AudioClip _trackCompleted;

    [SerializeField] bool _isGameOver = false;
    [SerializeField] bool _hasWon;

    SpawnManager _spawnManager;
    UIManager _uiManager;
    FPS_Controller _controller;


    private void Start()
    {
        _spawnManager = FindFirstObjectByType<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.Log("spawn manager is null");

        }

        _uiManager = FindFirstObjectByType<UIManager>();
        if (_uiManager == null)
        {
            Debug.Log("ui manager is null");
        }

        _controller = FindFirstObjectByType<FPS_Controller>();
        if (_controller == null)
        {
            Debug.Log("controller is null");
        }
    }

    private void Update()
    {
        if(_isGameOver == false)
        {
            GameStart();
        }
        
        if(_isGameOver == true && _hasWon == false)
        {
            GameOver();
        }
        else if(_isGameOver == true && _hasWon == true)
        {
            VictoryFunction();
        }
    }

    void GameStart()
    {
        _spawnManager.Spawner();
    }

    public void TrackFinish()
    {
        _audio.PlayOneShot(_trackCompleted);
    }

    void VictoryFunction()
    {
        _spawnManager.StopEnemyRoutine();
        _controller.enabled = false;
        _uiManager.VictoryTextOn();
        Time.timeScale = 0;
    }

    void GameOver()
    {
        _spawnManager.StopEnemyRoutine();
        _controller.enabled = false;
        _uiManager.GameOverText();
        Time.timeScale = 0;
    }

    public void VictoryEndGame()
    {
        _isGameOver = true;
        _hasWon = true;
    }

    public void LossEndGame()
    {
        _isGameOver = true;
        _hasWon = false;
    }
}
