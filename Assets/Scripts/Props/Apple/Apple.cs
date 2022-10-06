using UnityEngine;
using Random = UnityEngine.Random;

public class Apple : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out EndLevel _endLevel))
        {
            _endLevel.MissedCollectedApple(1);
            
            if(DoodleMoney.DisactiveRespawn!=true)
            {

                float randomX = Random.Range(-2.3f, 2.3f);
                float randomY = Random.Range(transform.position.y + 25f + _endLevel.HardAppleRespawnValue(),
                    transform.position.y + 50f + _endLevel.HardAppleRespawnValue());

                transform.position = new Vector3(randomX, randomY, transform.position.z);
            }
            else
            {
                DoodleMoney.CountDisactiveRespawn += 1;
                Destroy(gameObject);
            }
            
        }
    }
}
