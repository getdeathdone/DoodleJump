using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKill : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _damageJump=8;
    [SerializeField] public Animator _animatorPlatform;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.transform.TryGetComponent(out DoodleDestanetion _doodleDestanetion))&(collision.transform.TryGetComponent(out DoodleHealth _doodleHealth)))
        {
            if (collision.relativeVelocity.y <= 0)
            {
                _doodleDestanetion._rb.velocity = Vector2.up * _damageJump;
                if (_animatorPlatform)
                {
                    _animatorPlatform.SetTrigger("kill");
                }
                Destroy(gameObject);
            }else
                _doodleHealth.Damage(_damage);
        }
    }
}

