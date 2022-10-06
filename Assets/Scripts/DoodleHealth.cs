using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoodleHealth : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _doodleHealthStatus;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private int _doodleFullHealth=2;
    private int _doodleHealth;
    private DoodleFall _doodleFall;
    private DoodleDestanetion _doodleDestanetion;
    private void Awake()
    {
        _doodleDestanetion = gameObject.GetComponent<DoodleDestanetion>();
        _doodleFall = gameObject.GetComponent<DoodleFall>();
        _doodleHealth = _doodleFullHealth;
        _doodleHealthStatus.text = _doodleHealth.ToString();
    }
    public void RestartHealth()
    {
        _doodleHealth = _doodleFullHealth;
        _doodleHealthStatus.text = _doodleHealth.ToString();
    }
    public void Heal(int heal)
    {
        _doodleHealth += heal;
        _doodleHealthStatus.text = _doodleHealth.ToString();
    }
    public void Damage(int _damage)
    {
        Instantiate(_particle,transform.position, _particle.transform.rotation, transform);
        if(_doodleHealth>_damage)
        {
            _doodleDestanetion._animatorDoodle.SetTrigger("damage");
            _doodleHealth -= _damage;
            _doodleHealthStatus.text = _doodleHealth.ToString();
        }
        else if(_doodleHealth==_damage)
        {   
            _doodleHealth -= _damage;
            _doodleHealthStatus.text = _doodleHealth.ToString();
            _doodleFall.DeadDoodle();
        }else if(_doodleHealth<_damage)
        {   
            _doodleHealth = 0;
            _doodleHealthStatus.text = _doodleHealth.ToString();
            _doodleFall.DeadDoodle();
        }
    }
}
