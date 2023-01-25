using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlpacaMyGames;

public class Projectile : EnemyObstacle
{
    private bool _isActive = false;
    private float _initialSpeed = 4.0f;
    private float _speedBonus = 1.0f;
    private Vector2 _projectileDirection;
    private Vector2 _worldBoundary;

    private Player _player;

    private void Start()
    {
        _worldBoundary = Utilities.GetWorldOrthographicCameraSize();
        _projectileDirection = (Vector3.zero - transform.position).normalized;
        _player = Player.Instance;
    }

    private void Update()
    {
        move();
    }

    private Vector2 getDirection(Vector2 position)
    {
        return ((Vector2)_player.transform.position - position).normalized;
    }

    protected override void move()
    {
        if (!_isActive)
            return;

        if (_projectileDirection == Vector2.zero)
            return;

        Vector2 position = transform.position;
        float speed = _initialSpeed * _speedBonus;
        position += speed * _projectileDirection * Time.deltaTime;

        float boundaryMargin = 2.0f;
        if (position.x < -_worldBoundary.x * boundaryMargin || position.y < -_worldBoundary.y * boundaryMargin ||
            position.x > _worldBoundary.x * boundaryMargin || position.y > _worldBoundary.y * boundaryMargin)
        {
            position = Utilities.GetRandomBoundaryPosition();
            _projectileDirection = getDirection(position);
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
        {
            float marginScale = 2.0f;
            Vector2 margin = new Vector2(transform.localScale.x * marginScale, transform.localScale.y * marginScale);

            transform.position = Utilities.GetRandomBoundaryPosition(margin);
        }
    }

    public override bool IsActive()
    {
        return _isActive;
    }
}
