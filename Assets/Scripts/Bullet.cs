using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out EnemyKill enemyKill))
        {
            enemyKill._animatorPlatform.SetTrigger("kill");
            Debug.Log("if (col.transform.TryGetComponent(out EnemyKill enemyKill))");
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }


    
}
