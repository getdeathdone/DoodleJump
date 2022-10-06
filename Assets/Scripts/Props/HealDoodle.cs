using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealDoodle : MonoBehaviour
{
    [SerializeField] private int _heal;
    [SerializeField] private ParticleSystem _particle;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.TryGetComponent(out DoodleHealth _doodleHealth))
        {
            var particle = Instantiate(_particle, col.transform.position, _particle.transform.rotation);
            particle.transform.SetParent(col.transform);
            _doodleHealth.Heal(_heal);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
