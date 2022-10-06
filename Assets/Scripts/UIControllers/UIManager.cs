using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] 
    public Text _collectedAppleScreen;
    [SerializeField] 
    private Text _lastLevel;
    [SerializeField] 
    private TextMeshProUGUI _resultLastLevelScreen;
    [SerializeField] 
    private TextMeshProUGUI _resultEverLastMode;
    [SerializeField]
    private Button _openShop;
    [SerializeField]
    private Button _exitShop;
    [SerializeField]
    private Button _openShopBack;
    [SerializeField]
    private Button _exitShopBack;
    [SerializeField]
    private Button _openLevelsList;
    [SerializeField]
    private Button _exitLevelsList;
    [SerializeField]
    private Button _startGame;
    [SerializeField]
    private Button _startEverlastGame;
    [SerializeField]
    private Button _exitGame;
    [SerializeField]
    private GameObject _shop;
    [SerializeField]
    private GameObject _shopBack;
    [SerializeField]
    private Button _openSettings;
    [SerializeField]
    private GameObject _settings;
    [SerializeField]
    private GameObject _levelsList;
    private int _indexSettingsButton=0;
    // [SerializeField]
    // private CanvasGroup _settings;
    // [SerializeField]
    //private CanvasGroup _shop;

    private void Awake()
    {
        _openShop.onClick.AddListener(OpenShop);
        _exitShop.onClick.AddListener(CloseShop);
        _openShopBack.onClick.AddListener(OpenShopBack);
        _exitShopBack.onClick.AddListener(CloseShopBack);
        _startGame.onClick.AddListener(PlayMode);
        _startEverlastGame.onClick.AddListener(EverlastPlayMode);
        _exitGame.onClick.AddListener(ExitGame);
        _openSettings.onClick.AddListener(OpenCloseSettings);
        _openLevelsList.onClick.AddListener(OpenLevelsList);
        _exitLevelsList.onClick.AddListener(CloseLevelsList);
        _collectedAppleScreen.text = GlobalScore.GlobalApple.ToString();
        _resultLastLevelScreen.text = GlobalScore.LastLevel.ToString();
        _resultEverLastMode.text = GlobalScore.LastEverLastMode.ToString();
        _lastLevel.text = GlobalScore.GlobalLevel.ToString();
    }
    public void OpenShop()
    {
        AudioManager.PlayButton();
        _shop.SetActive(true);
    }
    public void CloseShop()
    {
        AudioManager.PlayButton();
        _shop.SetActive(false);
    }
    public void OpenShopBack()
    {
        AudioManager.PlayButton();
        _shopBack.SetActive(true);
    }
    public void CloseShopBack()
    {
        AudioManager.PlayButton();
        _shopBack.SetActive(false);
    }
    public void PlayMode()
    {
        AudioManager.PlayButton();
        SceneManager.LoadScene("LevelGameScene");
        EndLevel.GameModeStatic = false;
    }
    public void EverlastPlayMode()
    {
        AudioManager.PlayButton();
        SceneManager.LoadScene("LevelGameScene");
        EndLevel.GameModeStatic = true;
    }
    public void ExitGame()
    {
        AudioManager.PlayButton();
        Application.Quit();
    }
    public void OpenCloseSettings()
    {
        AudioManager.PlayButton();
        if (_indexSettingsButton == 0)
        {
            _settings.SetActive(true);
            _indexSettingsButton = 1;
        }
        else
        {
            _settings.SetActive(false);
            _indexSettingsButton = 0;
        }
    }
    public void OpenLevelsList()
    {
        AudioManager.PlayButton();
        _levelsList.SetActive(true);
    }
    public void CloseLevelsList()
    {
        AudioManager.PlayButton();
        _levelsList.SetActive(false);
        
    }
}
