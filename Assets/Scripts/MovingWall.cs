using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlpacaMyGames;

public class MovingWall : EnemyObstacle
{
    [SerializeField] private Vector2 _movementDirection;
    private Vector2 _initialPosition;
    private float _initialSpeed = 4.0f;
    private float _speedBonus = 1.0f;
    private bool _isActive = false;

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    private void Update()
    {
        move();
    }

    protected override void move()
    {
        if (!_isActive)
            return;

        Vector2 position = transform.position;
        float speed = _initialSpeed * _speedBonus;
        position += _movementDirection * speed * Time.deltaTime;

        if (!Utilities.IsInsideWorldScreen(position, transform.localScale * 2.0f))
        {
            _movementDirection *= -1.0f;
            return;
        }

        transform.position = position;
    }

    public override void IncreaseSpeedByPercentage(float percentage)
    {
        _speedBonus *= (1.0f + percentage / 100);
        Debug.Log(_speedBonus);
    }

    public override void ResetSpeed()
    {
        _speedBonus = 1.0f;
    }

    public override void Activate(bool value)
    {
        _isActive = value;

        if (!value)
            transform.position = _initialPosition;
    }

    public override bool IsActive()
    {
        return _isActive;
    }
}
