 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _score;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private GameObject _gameOverText;
    private GameManager _gameManager;

   void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(_gameManager == null)
        {
            Debug.LogError("GameManager is null");
        }

        _score.text = "SCORE: " + 0;
        _gameOverText.SetActive(false);
    }
    
    public void UpdateScore(int playerScore)
    {
        _score.text = "SCORE: " + playerScore;
    }
    public void UpdateLives(int currentlives)
    {
                _livesImage.sprite = _liveSprites[currentlives];
    }
    public void GameOverSequence()
    {
        _gameOverText.SetActive(true);
        _gameManager.GameIsOver();
        StartCoroutine(FlickerGameOver());
    }
    IEnumerator FlickerGameOver()
    {
        if (_gameOverText.activeSelf)
        {
            while (true)
            {
                _gameOverText.GetComponent<Text>().color = Random.ColorHSV();
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

}
