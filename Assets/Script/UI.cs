using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;

public class UI : MonoBehaviour
{
    [Header("Start Panel")]
    [Space]
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private Button _startButton;

    [Header("Game Panel")]
    [Space]
    [SerializeField] private GameObject _gamePanel;    
    [SerializeField] private GameObject _spawnButtons;
    public GameObject spawnButtons => _spawnButtons;
    [SerializeField] private ParticleSystem _confetti;


    [Header("Win Panel")]
    [Space]
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private Button _nextLevelButton;


    private void Awake()
    {
        _startButton.onClick.AddListener(StartGame);
        _nextLevelButton.onClick.AddListener(NextLevel);
    }
    private void StartGame()
    {
        _startPanel.SetActive(false);
        _gamePanel.SetActive(true);
        _gamePanel.transform.GetChild(1).DOScale(0.53f, 0.5f).From(0);
    }

    private IEnumerator WinGame()
    {
        Sequence sequence = DOTween.Sequence();
        GameObject grater = GameObject.Find("Grater");
        GameObject bowl = GameObject.Find("Bowl");
        Instantiate(_confetti, new Vector3(0, 5.6f, 0), Quaternion.identity);

        sequence.Append(grater.transform.DOMove(new Vector3(2.41f, 0.22f, 5.85f), 3))
                .Insert(0, grater.transform.DORotate(new Vector3(0, -38.5f, -61.8f), 3))
                .Insert(0, grater.transform.DOScale(new Vector3(0.236f, 0.236f, 0.236f), 3))
                .Insert(3, grater.transform.DOMoveY(0, 1));

        sequence.Append(bowl.transform.DOMove(new Vector3(0, 0.3f, -2.77f), 1))
                .PrependInterval(0.2f)
                .Append(bowl.transform.DORotate(new Vector3(-50, 0, 0), 2));
       
        _gamePanel.SetActive(false);
        yield return new WaitForSeconds(7f);
        _winPanel.SetActive(true);
    }

    public IEnumerator UIWinGame() => WinGame();

    private void NextLevel()
    {
        SceneManager.LoadScene("Game");
    }

}
