using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private SpaceCraft _target;

    [SerializeField] private Image _fillImage;

    [SerializeField] private Vector2 _positionOffset;
    private Vector3 _startedLocalSize;

    private void Awake()
    {
        _startedLocalSize = transform.localScale;
    }

    private void Start()
    {
        transform.localScale = _startedLocalSize;
    }

    private void Update()
    {
        if(_target)
            transform.position = new Vector3(_target.transform.position.x + _positionOffset.x, _target.transform.position.y + _positionOffset.y, 0);
    }

    public void Init(SpaceCraft enemySpacecraft)
    {
        _fillImage.fillAmount = 1;

        transform.localScale = _startedLocalSize;

        if (_target != null)
        {
            _target.OnDamage -= UpdateFill;
            _target.OnDie -= UpdateFill;
            _target.OnDie -= Disable;
        }

        _target = enemySpacecraft;
        _target.OnDamage += UpdateFill;
        _target.OnDie += UpdateFill;
        _target.OnDie += Disable;

        UpdateFill();
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
    private void UpdateFill()
    {
        _fillImage.fillAmount = _target.Health / _target.MaxHealth;

        if (_fillImage.fillAmount <= 0)
            Disable();
    }
}
