using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoodleDestanetion : MonoBehaviour,IPointerDownHandler
{
    public bool _rocketActive;
    public int _killSnarePlatform;
    [SerializeField] public Animator _animatorDoodle;
    [SerializeField] public Rigidbody2D _rb;
    [SerializeField] public Collider2D _colliderDoodle;
    [SerializeField] private Animator[] _animatorArray;
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _speed;

    [SerializeField] private float _bulletSpeed=20;

    [SerializeField] private Generator _snarePlatformGenerator;

    private bool _shot;

    private void Start()
    {
        _animatorDoodle.runtimeAnimatorController = _animatorArray[GlobalScore.AnimatorNumberHero].runtimeAnimatorController;
    }

    private void FixedUpdate()
    {
        var _horizontalDirection = Input.GetAxisRaw("Horizontal") + Input.acceleration.x;
        _rb.velocity = new Vector2(_horizontalDirection * _speed, _rb.velocity.y);
        
        if (_horizontalDirection > 0)
            _sr.flipX = false;
        else if (_horizontalDirection < 0)
            _sr.flipX = true;

        if (_animatorDoodle)
        {
            _animatorDoodle.SetFloat("velocity",_rb.velocity.y);
        }

        if ((Input.GetMouseButtonDown(0)))
        {
            Shot();
        }

        
    }

    private void Update()
    {
        if ((_rocketActive)&(_rb.velocity.y <= 0))
        {
            _rocketActive = false;
            _colliderDoodle.isTrigger = false;
        }

        if (_killSnarePlatform >= 6)
        {
            _snarePlatformGenerator.ReGenerateDefault(transform.position.y);
            _killSnarePlatform = 0;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Shot();
    }

    private void Shot()
    {
        if (_shot!=true)
        {
            GameObject _bulletNew = Instantiate(_bullet,
                new Vector3(transform.position.x, transform.position.y + 2.2f, transform.position.z),
                Quaternion.identity);
            if (_bulletNew.gameObject.TryGetComponent(out Rigidbody2D _rbBullet))
                _rbBullet.velocity = Vector2.up*_bulletSpeed;
            
            _shot = true;
            StartCoroutine(DelayToNextShot(_bulletNew));
        }
    }

    private IEnumerator DelayToNextShot(GameObject _bulletNew)
    {   
        yield return new WaitForSeconds(1.5f);
        Destroy(_bulletNew);
            
        yield return new WaitForSeconds(3f);
        _shot = false;

    }
}
