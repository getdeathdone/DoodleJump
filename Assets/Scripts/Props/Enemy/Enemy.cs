using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _damage=1;
    [SerializeField] private bool _destroyEnemy;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.TryGetComponent(out DoodleHealth _doodleHealth))
        {
            _doodleHealth.Damage(_damage);
        }
        if(_destroyEnemy)
            Destroy(gameObject);
    }
}
