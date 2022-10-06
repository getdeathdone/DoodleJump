using System;
using UnityEngine;

public class DoodleMoney : MonoBehaviour
{
    public static bool DisactiveRespawn;
    public static int CountBigAppleCollect;
    public static int CountDisactiveRespawn;

    [SerializeField] private EndLevel _endLevel;
    [SerializeField] private Generator _appleGenerator;
    private float _hardAppleRespawn;
    private float _offsetEveryAppleRespawn;
    private int _collectedApple;
    private int _updateHardAppleRespawn=10;
    private int _collectedAfterBigApple;

    private void Awake()
    {
        _endLevel.ChangeAppleScreenView(_collectedApple);
    }
    private void Update()
    {
        if ((DisactiveRespawn) & ((CountDisactiveRespawn + _collectedAfterBigApple)==(BigApple.AmountAppleInBigApple*CountBigAppleCollect)))
        {
            _updateHardAppleRespawn -= CountDisactiveRespawn;
            DisactiveRespawn = false;
            CountBigAppleCollect = CountDisactiveRespawn = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.TryGetComponent(out Apple appleScript))
        {
            _collectedApple += 1;
            GlobalScore.GlobalApple += 1;
            _endLevel.ChangeAppleScreenView(_collectedApple);
            Destroy(appleScript.gameObject);
            
            if (DisactiveRespawn)
                _collectedAfterBigApple += 1;

            if(_collectedApple == _updateHardAppleRespawn)
            {
                Debug.Log("CollectedApple == _updateHardAppleRespawn");
                _updateHardAppleRespawn += 10;
                _offsetEveryAppleRespawn += 1;
                var hardValue = ChangeHardLevelApple(2);
                _appleGenerator.ReGenerate(hardValue,hardValue,transform.position.y, _offsetEveryAppleRespawn);
            }
        }
        
        if (collider.transform.TryGetComponent(out EndLevel endLevel))
        {
            if(endLevel.missedApple==0)
            {
                Debug.Log("endLevel.missedApple == 0");
                endLevel.MissedCollectedAppleToView(0,_collectedApple);
            }else if (endLevel.missedApple > _collectedApple)
            {
                Debug.Log("endLevel.missedApple > CollectedApple");
                GlobalScore.GlobalApple-=_collectedApple;
                _collectedApple = 0;
                endLevel.MissedCollectedAppleToView(-1,_collectedApple);
            }
            else if(endLevel.missedApple <= _collectedApple)
            {
                Debug.Log("endLevel.missedApple <= CollectedApple");
                GlobalScore.GlobalApple-=endLevel.missedApple;
                _collectedApple-=endLevel.missedApple;
                endLevel.MissedCollectedAppleToView(1,_collectedApple);
            }
        }
    }

    public void GenerateAppleAfterRestart(float offsetMinY,float offsetMaxY,float height)
    {
        _appleGenerator.ReGenerate(offsetMinY,offsetMaxY,height);
        DisactiveRespawn = false;
        CountBigAppleCollect = CountDisactiveRespawn = 0;
    }

    public void GenerateAppleAfterCollectBigApple(float height,int amount)
    {
        _appleGenerator.ReGenerate( 1,2,height,0,amount);
        DisactiveRespawn = true;
        CountBigAppleCollect += 1;
        _updateHardAppleRespawn += amount;
    }

    public void ChangeScoreValueEndLevelScript(int score)
    {
        _endLevel.ChangeScoreValue(score);
    }

    public void BuyResume(int price)
    {
        GlobalScore.GlobalApple+=price;
        
        if (_collectedApple >= price)
            _collectedApple += price;
        else if (_collectedApple < price)
            _collectedApple = 0;
        
        _endLevel.ChangeAppleScreenView(_collectedApple);
    }

    private float ChangeHardLevelApple(float value)
    {
        _hardAppleRespawn += value;
        _endLevel.UpdateHardAppleRespawn(_hardAppleRespawn);
        return _hardAppleRespawn;
    }
}
