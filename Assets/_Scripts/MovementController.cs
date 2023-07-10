using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Space]
    [SerializeField] private float _speedOfReturnToStartPosition = 0.01f;

    [Space]
    [SerializeField] private Transform _startSpacePlanePosition;
    [SerializeField] private Vector2 _tapOffset;

    [SerializeField] private Transform _parentTransform;

    private bool _isDragging = false;

    private void Start()
    {
        if (_parentTransform == null)
            _parentTransform = transform.parent;
    }

    private void FixedUpdate()
    {
        if (!_isDragging)
        {
            ReturnSpacecraftToStartPosition();
        }
    }


    private void ReturnSpacecraftToStartPosition()
    {
        var deltaSpeed = _speedOfReturnToStartPosition * Time.fixedDeltaTime;
        _parentTransform.position = Vector2.MoveTowards(_parentTransform.position, _startSpacePlanePosition.position, deltaSpeed);
    }


    private void MoveFollowPointer()
    {
        Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + _tapOffset;

        _parentTransform.position = mousePosition;
    }


    private void OnMouseDrag()
    {
        _isDragging = true;
        MoveFollowPointer();
    }


    private void OnMouseUp()
    {
        _isDragging = false;
    }
}
