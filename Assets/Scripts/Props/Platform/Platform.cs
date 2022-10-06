using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Platform : MonoBehaviour
{
    public bool updateHardSwitch;
    public float _hardLevel;
    [SerializeField] private float _forceJump;
    [SerializeField] private Animator _animatorPlatform;
    [SerializeField] private SpriteRenderer _spritePlatform;
    [SerializeField] private bool _respawnPlatformsSwich;
    [SerializeField] private bool _movingPlatformsSwich;
    [SerializeField] private float _randomValueMovePlatform=0.3f;
    [SerializeField] private bool _snarePlatformsSwich;
    [SerializeField] private int _snareHealth ;
    [SerializeField] private int _playerDamage ;
    //[SerializeField] private Sprite _defaultSprite;
    private Color _defaultColor;
    private bool _movingPlatforms;


    private void Start()
    {
        if(_spritePlatform)
            _defaultColor = _spritePlatform.color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out DoodleDestanetion _doodleDestanetion))
        {
            if (collision.relativeVelocity.y <= 0)
            {
                _doodleDestanetion._rb.velocity = Vector2.up * _forceJump;
                if (_animatorPlatform)
                {
                    _animatorPlatform.SetTrigger("jumpTrampoline");

                    if (_snarePlatformsSwich)
                    {
                        _snareHealth -= _playerDamage;
                        if (_snareHealth <= 0)
                        {
                            _doodleDestanetion._killSnarePlatform += 1;
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if ((collider.transform.TryGetComponent(out EndLevel _endLevelScript))&(_respawnPlatformsSwich))
        {
            if (_movingPlatforms)
            {
                transform.DOKill();
                _spritePlatform.color = _defaultColor;
                _movingPlatforms = false;
            }
            
            if(updateHardSwitch)
            {
                if(_hardLevel<30)
                    _hardLevel = _endLevelScript.LevelPlatform();

                _hardLevel += 20 / _endLevelScript.PassedPlatform();
                updateHardSwitch = false;
            }
            
            float randomX = Random.Range(-2.75f, 2.75f);
            float randomY = Random.Range(transform.position.y + _hardLevel + 15f, transform.position.y + _hardLevel + 16.5f);
            
            
            transform.position = new Vector3(randomX,randomY,transform.position.z);
            
            if ((_movingPlatforms!=true)&(Random.value<_randomValueMovePlatform)&_movingPlatformsSwich)
            {
                var moveRandomX = 0f;
                if ((randomX > -2.75f)&(randomX < -0.6f))
                    moveRandomX = randomX + Random.Range(0.6f, 2.5f);
                else if ((randomX > 0.6f) & (randomX < 2.75f))
                    moveRandomX = randomX + Random.Range(-2.5f, -0.6f);
                else
                    moveRandomX = randomX > 0 ? Random.Range(1f, 2.5f) : Random.Range(-2.5f, -1f);
               
                transform.DOLocalMoveX(  moveRandomX, 1.2f).SetLoops(-1, LoopType.Yoyo);
                _spritePlatform.color=Color.blue;
                _movingPlatforms = true;
            }
        }
    }
}
