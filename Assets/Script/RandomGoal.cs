using UnityEngine;
using UnityEngine.UI;
using Lofelt.NiceVibrations;

public class RandomGoal : MonoBehaviour
{
    [Header("Vegan Icon")]
    [Space]
    [SerializeField] private Sprite[] _veganIcons;
    [SerializeField] private Image _goalNow;

    [Header("Colors")]
    [Space]
    [SerializeField] private Color[] _colors;

    private UI _gameUI;

    private Image[] _imageStart;
    private Image[] _imageGame;

    private bool _veganOneIsReady, _veganTwoIsReady, _veganThreeIsReady;
    private string _goalOne, _goalTwo, _goalThree;
    private int colorNum1, colorNum2, colorNum3;

    private void Awake()
    {
        _gameUI = FindObjectOfType<UI>();
    }

    private void Start()
    {
        FindImage();
        UpdateIcon();
    }

    private void FindImage()
    {
        _imageStart = new Image[_gameUI.goalBowlStart.Length];

        _imageGame = new Image[_gameUI.goalBowlGame.Length];

        for (int i = 0; i < _gameUI.goalBowlStart.Length; i++)
        {
            _imageStart[i] = _gameUI.goalBowlStart[i].GetComponent<Image>();
        }

        for (int i = 0; i < _gameUI.goalBowlGame.Length; i++)
        {
            _imageGame[i] = _gameUI.goalBowlGame[i].GetComponent<Image>();
        }

        SetColor();
    }
    private void SetColor()
    {
        colorNum1 = Random.Range(0, 3);
        colorNum2 = Random.Range(0, 3);
        colorNum3 = Random.Range(0, 3);

        _imageStart[0].color = _colors[colorNum1];
        _imageGame[0].color = _colors[colorNum1];

        _imageStart[1].color = _colors[colorNum2];
        _imageGame[1].color = _colors[colorNum2];

        _imageStart[2].color = _colors[colorNum3];
        _imageGame[2].color = _colors[colorNum3];

        SetImage();
    }

    private void SetImage()
    {
        _imageStart[0].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _veganIcons[colorNum1];
        _imageGame[0].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _veganIcons[colorNum1];

        _imageStart[1].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _veganIcons[colorNum2];
        _imageGame[1].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _veganIcons[colorNum2];

        _imageStart[2].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _veganIcons[colorNum3];
        _imageGame[2].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _veganIcons[colorNum3];

        SetGoal();
    }

    private void SetGoal()
    {

        switch (colorNum1)
        {
            case 0:
                _goalOne = "Carrot";
                break;
            case 1:
                _goalOne = "Cucumber";
                break;
            case 2:
                _goalOne = "Tomato";
                break;
            default:
                break;
        }

        switch (colorNum2)
        {
            case 0:
                _goalTwo = "Carrot";
                break;
            case 1:
                _goalTwo = "Cucumber";
                break;
            case 2:
                _goalTwo = "Tomato";
                break;
            default:
                break;
        }

        switch (colorNum3)
        {
            case 0:
                _goalThree = "Carrot";
                break;
            case 1:
                _goalThree = "Cucumber";
                break;
            case 2:
                _goalThree = "Tomato";
                break;
            default:
                break;
        }

    }

    public void CheckGoal(string nameVegan)
    {
        if (nameVegan == _goalOne && !_veganOneIsReady)
        {
            _veganOneIsReady = true;
        }
        else if (nameVegan == _goalTwo && _veganOneIsReady && !_veganTwoIsReady)
        {
            _veganTwoIsReady = true;
        }
        else if (nameVegan == _goalThree && _veganTwoIsReady)
        {
            _veganThreeIsReady = true;
        }

        if (_veganOneIsReady && _veganTwoIsReady && _veganThreeIsReady)
        {
            StartCoroutine(_gameUI.UIWinGame());
        }

        UpdateIcon();
    }

    private void UpdateIcon()
    {
        if (!_veganOneIsReady)
        {
            _goalNow.sprite = _veganIcons[colorNum1];
        }
        else if (_veganOneIsReady && !_veganTwoIsReady)
        {
            _imageGame[0].color = _colors[3];
            _goalNow.sprite = _veganIcons[colorNum2];
        }
        else if (_veganTwoIsReady && !_veganThreeIsReady)
        {
            _imageGame[1].color = _colors[3];
            _goalNow.sprite = _veganIcons[colorNum3];
        }
        else if (_veganThreeIsReady)
        {
            _imageGame[2].color = _colors[3];
        }
    }

}
