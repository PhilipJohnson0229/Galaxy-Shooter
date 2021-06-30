using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Image _livesImage;

    [SerializeField]
    private Sprite[] _livesSprites;

    [SerializeField]
    private GameObject _gameOverText;

    [SerializeField]
    private GameObject _resetText;

    [SerializeField]
    private GameManager _gm;

    public static UIManager instance;
    void Start()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (instance == null)
        {
            instance = this;
        }
        _gameOverText.SetActive(false);
        _resetText.SetActive(false);

    }

    public void UpdateScore(int _scoreChange)
    {
        _scoreText.text = "SCORE: " + _scoreChange.ToString();

 
   }

    public void UpdateLives(int _currentLives)
    {
        _livesImage.sprite = _livesSprites[_currentLives];
    }

    public void ShowGameOver()
    {
        _gm.GameOVer();
        _gameOverText.SetActive(true);
        _resetText.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());

    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.SetActive(false);
            yield return new WaitForSeconds(0.5f);

        }
    }
}
