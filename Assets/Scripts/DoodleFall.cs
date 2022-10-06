using System.Collections;
using TMPro;
using UnityEngine;

public class DoodleFall : MonoBehaviour
{   
    private DoodleDestanetion _doodleDestanetion;
    private DoodleMoney _doodleMoney;
    private DoodleHealth _doodleHealth;
    private bool _deadDoodle; 
    private int _maxRestartSaveLevel; 
    private int _restartLevelValue=1000;
    [SerializeField] private int _priceResume;
    [SerializeField] private int _maxRestart=10;
    [SerializeField] private float _fallJumpSpeed;
    [SerializeField] private GameObject _levelPlatformGenerator;
    [SerializeField] private GameObject _otherPlatformGenerator;
    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private GameObject _noMoneyText;
    [SerializeField] private GameObject _noRestartText;
    [SerializeField] private GameObject _gameReturnButton;
    [SerializeField] private GameObject _gameRestartButton;
    [SerializeField] private GameObject _hideAppleCount;
    [SerializeField] private TextMeshProUGUI _countRestartText;

    private void Awake()
    {
        _doodleDestanetion =gameObject.GetComponent<DoodleDestanetion>();
        _doodleMoney = gameObject.GetComponent<DoodleMoney>();
        _doodleHealth = gameObject.GetComponent<DoodleHealth>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.TryGetComponent(out EndLevel _endLevelScript))
        {   
            _doodleDestanetion._rb.velocity = Vector2.up * _fallJumpSpeed;
            
            if ((_doodleDestanetion._animatorDoodle)&(_deadDoodle!=true))
                _doodleDestanetion._animatorDoodle.SetBool("fell",true);
            
            StartCoroutine(DelayToStopAndWindowResult(_endLevelScript));
        }
    }

    public void StopDoodle(bool status)
    {
        _doodleDestanetion._rb.simulated = status;
    }
    public void DeadDoodle()
    {
        _deadDoodle = true;
        if (_doodleDestanetion._animatorDoodle)
            _doodleDestanetion._animatorDoodle.SetBool("fell",true);
        _doodleDestanetion._colliderDoodle.isTrigger = true;
    }
    public void RestartLevelPress()
    {
        if (EndLevel.GameModeStatic)
            _noMoneyText.SetActive(false);
        
        _doodleMoney.ChangeScoreValueEndLevelScript(0);
        
        transform.position = new Vector3(0, transform.position.y + 100, transform.position.z);
        
        foreach (Transform child in _otherPlatformGenerator.transform)
        {
            if (child.TryGetComponent(out Generator _generator))
            {
               _generator.ReGenerate(0.36f*_maxRestartSaveLevel,0.33f*_maxRestartSaveLevel,transform.position.y-4);
            }
        }

        _doodleDestanetion._killSnarePlatform = 0;
        if (transform.position.y > _restartLevelValue)
        {
            _restartLevelValue += 1000;
            foreach (Transform child in _levelPlatformGenerator.transform)
            {
                if (child.TryGetComponent(out Generator _generator))
                {
                    _generator.ReGenerateDefault(transform.position.y-4);
                }
            }
        }
        
        _doodleHealth.RestartHealth();
        _doodleMoney.GenerateAppleAfterRestart(_maxRestartSaveLevel+2f,_maxRestartSaveLevel+4f,transform.position.y-4);
        LevelResumeFunction();
    }
    public void BuyResumeLevelPress()
    {  
        _gameReturnButton.SetActive(false);
        _doodleMoney.BuyResume(-_priceResume);
        LevelResumeFunction();
    }

    private void LevelResumeFunction()
    {
        _gameOverCanvas.SetActive(false);
        _hideAppleCount.SetActive(true);
        
        if(_deadDoodle)
        {
            _doodleDestanetion._colliderDoodle.isTrigger = false;
            _deadDoodle = false;
        }
        
        if (_doodleDestanetion._animatorDoodle)
            _doodleDestanetion._animatorDoodle.SetBool("fell",false);
        _doodleDestanetion._rb.simulated = true;
        _doodleDestanetion._rb.velocity = Vector2.up * (_fallJumpSpeed / 2);
    }

    private IEnumerator DelayToStopAndWindowResult(EndLevel _endLevelScript)
    {   
        yield return new WaitForSeconds(1f);
        AudioManager.PlayVibration();
        _doodleDestanetion._rb.simulated = false;
        
        yield return new WaitForSeconds(1.5f);
        _hideAppleCount.SetActive(false);
        _countRestartText.text = _endLevelScript._everlastMode ? "" : (_maxRestart - _maxRestartSaveLevel).ToString();
        
        _endLevelScript.ResultScreenView();
        
        if (_endLevelScript._everlastMode)
        {
            if (GlobalScore.GlobalApple>= _priceResume)
                _gameReturnButton.SetActive(true);
            else if(GlobalScore.GlobalApple < _priceResume)
                _noMoneyText.SetActive(true);
        }else if(_endLevelScript._everlastMode!=true)
        {
            if (_maxRestartSaveLevel == _maxRestart)
            {
                _gameRestartButton.SetActive(false);
                _noRestartText.SetActive(true);
            }
            else if (_maxRestartSaveLevel != _maxRestart)
                _maxRestartSaveLevel +=1;
        }
        
        _gameOverCanvas.SetActive(true);
    }
}
