using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] private Transform _destinationTransform;

    private void Teleporting(GameObject target)
    {   if(target.transform.position.x>0)
            target.transform.position = new Vector3(_destinationTransform.position.x+1,target.transform.position.y,transform.position.z);
        else if(target.transform.position.x<0)
            target.transform.position = new Vector3(_destinationTransform.position.x-1,target.transform.position.y,transform.position.z);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
            Teleporting(other.gameObject);
    }
}
