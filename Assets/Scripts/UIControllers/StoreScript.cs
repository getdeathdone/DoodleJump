using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StoreScript : MonoBehaviour
{
    [SerializeField] private Button _buyOne;
    [SerializeField] private Button _buyTwo;
    [SerializeField] private Button _buyThree;
    [SerializeField] private Button _buyFour;
    [SerializeField] private int _priceSkineTwo;
    [SerializeField] private int _priceSkineThree;
    [SerializeField] private int _priceSkineFour;
    [SerializeField] private Image _oneSkin;
    [SerializeField] private Image _twoSkin;
    [SerializeField] private Image _threeSkin;
    [SerializeField] private Image _fourSkin;
    [SerializeField] private GameObject _noMoneyTable;
    [SerializeField] private GameObject _character;
    [SerializeField] private UIManager _uiManager;
    private void Awake()
    {   
        if (_character.TryGetComponent(out Image _characterImage))
        {
            _characterImage.sprite = ChangeNumberImage(GlobalScore.AnimatorNumberHero).sprite;
        }
        _buyOne.onClick.AddListener(ChangeHeroSkinOne);
        _buyTwo.onClick.AddListener(ChangeHeroSkinTwo);
        _buyThree.onClick.AddListener(ChangeHeroSkinThree);
        _buyFour.onClick.AddListener(ChangeHeroSkinFour);
    }
    private void ChangeHeroSkinOne()
    {
        ChangeNumberSkine(0,0,_oneSkin);
    }
    private void ChangeHeroSkinTwo()
    {   
        ChangeNumberSkine(1,_priceSkineTwo,_twoSkin);
    }
    private void ChangeHeroSkinThree()
    {
        ChangeNumberSkine(2,_priceSkineThree,_threeSkin);
    }
    private void ChangeHeroSkinFour()
    {
        ChangeNumberSkine(3,_priceSkineFour,_fourSkin);
    }

    private Image ChangeNumberImage(int skinNumber)
    {
        Image image = null;
        if (skinNumber == 0) image = _oneSkin;
        if (skinNumber == 1) image = _twoSkin;
        if (skinNumber == 2) image = _threeSkin;
        if (skinNumber == 3) image = _fourSkin;
        return image;
    }
    private void ChangeNumberSkine(int skinNumber,int price, Image image)
    {
        if(GlobalScore.GlobalApple>=price)
        {
            GlobalScore.GlobalApple -= price;
            GlobalScore.AnimatorNumberHero = skinNumber;
            _uiManager._collectedAppleScreen.text = GlobalScore.GlobalApple.ToString();
            if (_character.TryGetComponent(out Image _characterImage))
            {
                _characterImage.sprite = image.sprite;
            }
        }else if (GlobalScore.GlobalApple < price)
        {
            _noMoneyTable.SetActive(true);
            StartCoroutine(DelayToDesactive(_noMoneyTable));
        }
    }
    
    private IEnumerator DelayToDesactive(GameObject gameObject)
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    } 
    
}
