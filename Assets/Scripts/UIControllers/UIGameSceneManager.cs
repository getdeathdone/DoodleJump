using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameSceneManager : MonoBehaviour
{
    private int _indexSettingsButton = 0;
    [SerializeField]
    private GameObject _settings;
    [SerializeField]
    private Button _openSettings;
    [SerializeField]
    private Button _restartGame;
    [SerializeField]
    private Button _backToMenu;
    [SerializeField]
    private Button _resumeGame;

    [SerializeField] private DoodleFall _doodleFallScript;

    private void Awake()
    {
        _openSettings.onClick.AddListener(OpenCloseSettings);
        _restartGame.onClick.AddListener(RestartGame);
        _backToMenu.onClick.AddListener(BackToMenu);
        _resumeGame.onClick.AddListener(BuyResumeGame);
    }
   
    public void OpenCloseSettings()
    {
        AudioManager.PlayButton();
        if (_indexSettingsButton == 0)
        {
            _settings.SetActive(true);
            _doodleFallScript.StopDoodle(false);
            DOTween.PauseAll();
            _indexSettingsButton = 1;
        }
        else
        {
            _settings.SetActive(false);
            _doodleFallScript.StopDoodle(true);
            DOTween.PlayAll();
            _indexSettingsButton = 0;
        }
    }
    public void BackToMenu()
    {
        AudioManager.PlayButton();
        SceneManager.LoadScene("MainMenuScene");
    }
    public void RestartGame()
    {   
        _doodleFallScript.RestartLevelPress();
    }
    public void BuyResumeGame()
    {   
        _doodleFallScript.BuyResumeLevelPress();
    }
}
