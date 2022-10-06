using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float _rocketJump = 20; 
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.TryGetComponent(out DoodleDestanetion _doodleDestanetion))
        {
            _doodleDestanetion._animatorDoodle.SetTrigger("rocket");
            _doodleDestanetion._rb.velocity = Vector2.up * _rocketJump;
            _doodleDestanetion._rocketActive = true;
            _doodleDestanetion._colliderDoodle.isTrigger = true;
            /*_doodleDestanetion._rocketActive=true;
            _doodleDestanetion._colliderDoodle.isTrigger = false;*/
        }
        Destroy(gameObject);
    }
}
