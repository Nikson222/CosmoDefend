using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private EnemySpacecraft _targetEnemy;

    [SerializeField] private Image _fillImage;

    [SerializeField] private Vector2 _positionOffset;

    private void Start()
    {
        print(transform.localScale);
    }
    private void Update()
    {
        if(_targetEnemy)
            transform.position = new Vector2(_targetEnemy.transform.position.x + _positionOffset.x, _targetEnemy.transform.position.y + _positionOffset.y);
    }

    public void Init(EnemySpacecraft enemySpacecraft)
    {
        _fillImage.fillAmount = 1;

        if (_targetEnemy != null)
        {
            _targetEnemy.OnDamage -= UpdateFill;
            _targetEnemy.OnDie -= UpdateFill;
            _targetEnemy.OnDie -= Disable;
        }

        _targetEnemy = enemySpacecraft;
        _targetEnemy.OnDamage += UpdateFill;
        _targetEnemy.OnDie += UpdateFill;
        _targetEnemy.OnDie += Disable;

        UpdateFill();
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
    private void UpdateFill()
    {
        _fillImage.fillAmount = _targetEnemy.Health / _targetEnemy.MaxHealth;
    }
}
