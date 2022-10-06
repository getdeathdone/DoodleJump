using System.Collections;
using UnityEngine;

public class BigApple : MonoBehaviour
{
    public static int AmountAppleInBigApple=30;
    [SerializeField] private Animator _animatorBigApple;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out DoodleMoney _doodleMoney))
        {
            _doodleMoney.GenerateAppleAfterCollectBigApple(transform.position.y-4,AmountAppleInBigApple);
            _animatorBigApple.SetTrigger("Big");
            StartCoroutine(DelayToDestroy(1f));
        }
        Destroy(gameObject);
    }
    
    private IEnumerator DelayToDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
