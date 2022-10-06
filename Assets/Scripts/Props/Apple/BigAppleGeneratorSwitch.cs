using UnityEngine;

public class BigAppleGeneratorSwitch : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(EndLevel.GameModeStatic);
    }
}
