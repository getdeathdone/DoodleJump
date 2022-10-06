using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameOverSceneController : MonoBehaviour
{
    [SerializeField]
    private Button _startGame;
    [SerializeField]
    private Button _backToMenu;
    private void Awake()
    {
        _startGame.onClick.AddListener(PlayMode);
        _backToMenu.onClick.AddListener(BackToMenu);
    }
    public void PlayMode()
    {
        AudioManager.PlayButton();
        SceneManager.LoadScene("LevelGameScene");
    }
    public void BackToMenu()
    {
        AudioManager.PlayButton();
        SceneManager.LoadScene("MainMenuScene");
    }
}
