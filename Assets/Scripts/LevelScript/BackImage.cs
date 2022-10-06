using UnityEngine;

public class BackImage : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _back;
    [SerializeField] private Sprite[] _spriteBack;
    private void Awake()
    {
        _back.sprite = _spriteBack[GlobalScore.BackgroundNumber];
    }
}
