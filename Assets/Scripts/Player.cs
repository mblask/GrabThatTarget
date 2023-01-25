using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlpacaMyGames;

public class Player : MonoBehaviour
{
    private static Player _instance;
    public static Player Instance
    {
        get
        {
            return _instance;
        }
    }

    private Rigidbody2D _rigidbody;

    private IPlayerInput _playerInput;
    private float _playerSpeed;
    private Vector2 _movingSpeed = new Vector2(5.0f, 10.0f);

    private Vector3 _movement;

    private GameManager _gameManager;
    private ICameraController _cameraController;

    private void Awake()
    {
        _instance = this;
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<IPlayerInput>();
        _cameraController = Camera.main.GetComponent<ICameraController>();
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    private void Update()
    {
        _movement = _playerInput.GetMovement();
        _playerSpeed = _playerInput.IsSprinting() ? _movingSpeed.y : _movingSpeed.x;

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        Vector3 position = transform.position;
        position += _movement * _playerSpeed * Time.deltaTime;

        if (!Utilities.IsInsideWorldScreen(position))
            return;
        
        _rigidbody.MovePosition(position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_gameManager == null)
            _gameManager = GameManager.Instance;

        TargetPoint targetPoint = collision.GetComponent<TargetPoint>();
        if (targetPoint != null)
        {
            targetPoint.SetRandomPosition();
            _gameManager.UpdateScore();
        }

        EnemyObstacle enemyObstacle = collision.GetComponent<EnemyObstacle>();
        if (enemyObstacle != null)
        {
            Debug.Log("Game over");
            _gameManager.ResetTimeScore();
            _cameraController.ShakeCamera();
        }
    }
}
