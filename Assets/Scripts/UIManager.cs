using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManager;
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _enemiesRemaining;
    [SerializeField] TextMeshProUGUI _timeRemaining;
    [SerializeField] TextMeshProUGUI _ammoRemaining;

    float _timer = 300;
    public int _enemies = 0;
    public int _ammo = 10;
    public int _score = 0;

    WaitForSeconds _reloadSpeed = new WaitForSeconds(3f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (uiManager == null)
        {
            uiManager = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        TimeRemaining();
        SubtractAmmo();
    }

    public void AddScore(int score)
    {
        _score += score;
        _scoreText.text = _score.ToString();
    }

    public void EnemiesRemainingAdd(int enemies)
    {
        _enemies += enemies;
        _enemiesRemaining.text = _enemies.ToString();
    }

    public void EnemiesRemainingMinus(int enemies)
    {
        _enemies -= enemies;
        _enemiesRemaining.text = _enemies.ToString();
    }

    void TimeRemaining()
    {
        float newTimer = _timer -= Time.deltaTime;
        newTimer = Mathf.Round(newTimer);
        _timeRemaining.text = newTimer.ToString();
        if (_timer <= 0)
        {
            _timer = 0;
        }
    }

    public void SubtractAmmo()
    {
        _ammoRemaining.text = _ammo.ToString();
        if(_ammo <= 0)
        {
            _ammo = 0;
            StartCoroutine(Reloading());
        }
    }

    IEnumerator Reloading()
    {
        yield return _reloadSpeed;
        _ammo = 10;
    }
}
