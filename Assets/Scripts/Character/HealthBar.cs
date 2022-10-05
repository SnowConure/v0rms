using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image _health;
    [SerializeField]
    private TMP_Text _ammo;
    [SerializeField]
    private Image _move;


    public void UpdateHealth(float value)
    {
        if(_health != null) 
        _health.fillAmount = value;
    }

    public void UpdateMove(float value)
    {
        if(_move != null) 
        _move.fillAmount = value;
    }

    public void UpdateAmmo(float value)
    {
        if (_ammo != null)
            _ammo.text = value.ToString();
    }

    public void HideAmmoUI(bool state)
    {
        if (_ammo != null)
            _ammo.gameObject.SetActive(state);
    }
}
