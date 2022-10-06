using System.Collections;
using TMPro;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    /*public static int RemunerationLevel;*/
    public static bool GameModeStatic;
    public bool _everlastMode;
    public int missedApple;

    private float tempAppleHard;
    private int[] _levelPlatform;
    private int _numberOfGeneratedLevels=5;
    private int _quantityPassedPlatfotmLevel;
    private int _passedNumberOfGeneratedLevels;
    private int _lastResultLevel; 
    private int _lastResultLevelEverlastMode; 
    private int _currentLevel;
    private int _countPassedPlatformLevel;
    private int _countPassedPlatfotmEverlastMode;
    [SerializeField] private TextMeshProUGUI _collectedAppleScreen;
    [SerializeField] private TextMeshProUGUI _passedPlatformView;
    [SerializeField] private TextMeshProUGUI _resultPlatformView;
    [SerializeField] private TextMeshProUGUI _resultLevel;
    [SerializeField] private TextMeshProUGUI _resultBest;
    [SerializeField] private TextMeshProUGUI _resultCollectedApple;
    [SerializeField] private TextMeshProUGUI _missedAppleText;
    [SerializeField] private GameObject _missedAppleTable;
    [SerializeField] private GameObject _levelView;
    /*[SerializeField] private TextMeshProUGUI _platformPassedLevel;
    [SerializeField] private TextMeshProUGUI _platformNextLevel;
    [SerializeField] private GameObject _platformPassedLevelText;
    [SerializeField] private GameObject _platformNextLevelText;
    [SerializeField] private GameObject _addToRemunerationLevel;*/
    private void Awake()
    {
        _everlastMode = GameModeStatic;
        if (_everlastMode != true)
        {
            GenerateLevel(0);
            _resultLevel.text = (_currentLevel + 1 + _passedNumberOfGeneratedLevels).ToString();
        }
        else if (_everlastMode)
        {
            _levelView.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {   
        if(collider.gameObject.TryGetComponent(out Platform platform)) 
        {
            if (_everlastMode)
            {   
                _countPassedPlatfotmEverlastMode += 1;
                _passedPlatformView.text = _resultPlatformView.text = _countPassedPlatfotmEverlastMode.ToString();
                platform._hardLevel += 0.01f;
            }
            else if (_everlastMode == false)
            {
                _countPassedPlatformLevel += 1;
                _passedPlatformView.text = _resultPlatformView.text = _countPassedPlatformLevel.ToString();

                if (_countPassedPlatformLevel == _quantityPassedPlatfotmLevel)
                {
                    platform.updateHardSwitch = true;
                    //_platformPassedLevel.text = _quantityPassedPlatfotmLevel.ToString();
                    //_platformPassedLevelText.SetActive(true);
                    //DelayToDesactive(_platformPassedLevelText);

                    _currentLevel += 1;
                    CheckedLevel();
                    _resultLevel.text = (_currentLevel + 1 + _passedNumberOfGeneratedLevels).ToString();
                    _quantityPassedPlatfotmLevel = _levelPlatform[_currentLevel];
                    
                    //_platformNextLevel.text = _quantityPassedPlatfotmLevel.ToString();
                    //_platformNextLevel.SetActive(true);
                    //DelayToDesactive(_platformNextLevel);
                }
            }
        }
    }

    public void ResultScreenView()
    {
        if (_everlastMode) 
        {
            if(_countPassedPlatfotmEverlastMode > _lastResultLevelEverlastMode)
                _lastResultLevelEverlastMode = _countPassedPlatfotmEverlastMode;
            _resultBest.text = _lastResultLevelEverlastMode.ToString();
            
            if(_lastResultLevelEverlastMode>GlobalScore.LastEverLastMode)
                GlobalScore.LastEverLastMode = _lastResultLevelEverlastMode;
        }
        else if(_everlastMode==false)
        {
            if (_countPassedPlatformLevel > _lastResultLevel)
                _lastResultLevel = _countPassedPlatformLevel;
            _resultBest.text = _lastResultLevel.ToString();
            
            if(_lastResultLevel>GlobalScore.LastLevel)
                GlobalScore.LastLevel = _lastResultLevel;

            if (GlobalScore.GlobalLevel<(LevelPlatform()))
            {
                GlobalScore.GlobalLevel = LevelPlatform();
            } 
        }
    }
    public void ChangeScoreValue(int changeValue)
    {
        _passedPlatformView.text = changeValue.ToString();
        _resultPlatformView.text = changeValue.ToString();
        _countPassedPlatformLevel = changeValue;
        _countPassedPlatfotmEverlastMode = changeValue;
    }
    public void ChangeAppleScreenView(int appleAmount)
    {
        _collectedAppleScreen.text =  appleAmount.ToString();
        _resultCollectedApple.text = GlobalScore.GlobalApple.ToString();
    }
    public void MissedCollectedApple(int appleAmount) 
    {
        missedApple += appleAmount; 
    }
    public void MissedCollectedAppleToView(int number,int amount) 
    {
        if(number==0)
            _missedAppleText.text = "You didn't miss a single apple";
        else if (number==-1)
            _missedAppleText.text = "You missed all level apples";
        else if (number==1)
            _missedAppleText.text = "You missed " + missedApple + " apples";
        
        missedApple = 0;
        _missedAppleTable.SetActive(true);
        StartCoroutine(DelayToDesactive(_missedAppleTable,6f));
        StartCoroutine(DelayToView(amount,4f));
    }
    public void UpdateHardAppleRespawn(float hardAppleRespawn)
    {
        tempAppleHard = hardAppleRespawn;
    }
    public float HardAppleRespawnValue() 
    {
        return tempAppleHard; 
    }
    public int PassedPlatform()
    {
        return _everlastMode ? _countPassedPlatfotmEverlastMode : _countPassedPlatformLevel;
    }
    public int LevelPlatform()
    {
        return _currentLevel + 1 + _passedNumberOfGeneratedLevels;
    }
    
    private void GenerateLevel(int levelUp)
    {
        _levelPlatform = new int[_numberOfGeneratedLevels];
        
        if(levelUp==0)
        {
            for (int i = 0; i < _numberOfGeneratedLevels; i++)
            {
                if (i == 0)
                    _levelPlatform[i] = 20;
                else
                    _levelPlatform[i] = i * 50;
            }
        }else if(levelUp>0)
        {
            for (int i = 0; i < _numberOfGeneratedLevels; i++)
            {
                _levelPlatform[i] = (i+levelUp) * 50;
            }
        }
        _currentLevel = 0;
        _quantityPassedPlatfotmLevel = _levelPlatform[_currentLevel];
    }
    private void CheckedLevel()
    {
        if ((_currentLevel) == _numberOfGeneratedLevels)
        {   
            //AddReward
            //RemunerationLevel += 1;
            //_addToRemuneration.SetActive(true);
            //StartCoroutine(DelayToDesactive(_addToRemuneration));
            
            _passedNumberOfGeneratedLevels += 5;
            GenerateLevel(_passedNumberOfGeneratedLevels);
        }
    }
    
    private IEnumerator DelayToDesactive(GameObject gameObject,float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
    private IEnumerator DelayToView(int appleAmount,float time)
    {
        yield return new WaitForSeconds(time);
        _collectedAppleScreen.text =  appleAmount.ToString();
        _resultCollectedApple.text = GlobalScore.GlobalApple.ToString();
        
    }
}
