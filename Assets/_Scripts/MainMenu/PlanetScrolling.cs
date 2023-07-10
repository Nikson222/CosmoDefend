using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetScrolling : MonoBehaviour
{
    [SerializeField] float _rollSpeed;
    [SerializeField] Image _platetImage;

    private float currentAngle = 0f;

    private void Update()
    {
        currentAngle += _rollSpeed * Time.deltaTime;

        _platetImage.rectTransform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
    }
}
