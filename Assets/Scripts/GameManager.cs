using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AlpacaMyGames;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public Action<float> OnUpdateTime;
    public Action<int> OnUpdateScore;

    private List<EnemyObstacle> _projectiles;
    private List<EnemyObstacle> _movingWalls;

    private float _timePassed;
    private float _timeIncrement;

    private int _score = 0;

    [SerializeField] private List<Highscore> _highscoreList = new List<Highscore>();

    private void Awake()
    {
        _instance = this;

        _movingWalls = Utilities.GetListOfObjectsFromContainer<EnemyObstacle>(transform, "MovingWalls");
        _projectiles = Utilities.GetListOfObjectsFromContainer<EnemyObstacle>(transform, "Projectiles");
    }

    private void Update()
    {
        updateTime();
    }

    private void updateTime()
    {
        _timePassed += Time.deltaTime;
        _timeIncrement += Time.deltaTime;

        if (_timeIncrement >= 0.1f)
        {
            _timeIncrement += 0.1f;
            OnUpdateTime?.Invoke(_timePassed);
        }
    }

    private void resetTime()
    {
        _timePassed = 0.0f;
        _timeIncrement = 0.0f;
        OnUpdateTime?.Invoke(_timePassed);
    }

    public void UpdateScore(int increment = 1)
    {
        _score += increment;

        alterObstaclesByScore(_score);

        OnUpdateScore?.Invoke(_score);
    }

    private void resetScore()
    {
        _score = 0;
        OnUpdateScore?.Invoke(_score);
    }

    public void ResetTimeScore()
    {
        addHighscore();

        resetTime();
        resetScore();

        deactivateObstacles(_movingWalls);
        resetObstacleSpeed(_movingWalls);
        deactivateObstacles(_projectiles);
        resetObstacleSpeed(_projectiles);
    }

    private void alterObstaclesByScore(int score)
    {
        List<int> projectileSpeedIncreseAt = new List<int> { 5, 10, 15, 20, 25 };
        int projectileStartLevel = 1;
        int activateProjectileEvery = 2;

        List<int> movingWallSpeedIncreseAt = new List<int> { 10, 20, 30 };
        int movingWallStartLevel = 10;
        int activateWallEvery = 3;

        if (score == projectileStartLevel || score % activateProjectileEvery == 0)
            activateObstacles(_projectiles, 1);

        if (projectileSpeedIncreseAt.Contains(score))
            increaseObstacleSpeed(_projectiles, 5.0f);

        if (score >= movingWallStartLevel && score % activateWallEvery == 0)
            activateObstacles(_movingWalls, 1);

        if (movingWallSpeedIncreseAt.Contains(score))
            increaseObstacleSpeed(_movingWalls, 3.0f);
    }

    private void increaseObstacleSpeed(List<EnemyObstacle> obstacles, float percentage)
    {
        for (int i = 0; i < obstacles.Count; i++)
            obstacles[i].IncreaseSpeedByPercentage(percentage);
    }

    private void resetObstacleSpeed(List<EnemyObstacle> obstacles)
    {
        for (int i = 0; i < obstacles.Count; i++)
            obstacles[i].ResetSpeed();
    }

    private void activateObstacles(List<EnemyObstacle> obstacles, int numberOfObstacles)
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (obstacles[i].IsActive())
                continue;

            obstacles[i].Activate(true);
            numberOfObstacles--;

            if (numberOfObstacles <= 0)
                break;
        }
    }

    private void deactivateObstacles(List<EnemyObstacle> obstacles)
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (!obstacles[i].IsActive())
                continue;

            obstacles[i].Activate(false);
        }
    }

    private void addHighscore()
    {
        Highscore newHighscore = new Highscore(_timePassed, _score);
        if (newHighscore.TotalScore < 1.0f)
            return;
        
        _highscoreList.Add(new Highscore(_timePassed, _score));

        if (_highscoreList.Count >= 1)
            _highscoreList = SortList(_highscoreList);

        HighscoreUI.UpdateHighscores(_highscoreList);
    }

    private List<Highscore> SortList(List<Highscore> list)
    {
        List<Highscore> sortedList = new List<Highscore>();

        for (int i = list.Count - 1; i >= 0 ; i--)
        {
            int maxIndex = FindMaxIndex(list);
            sortedList.Add(list[maxIndex]);
            list.RemoveAt(maxIndex);
        }

        return sortedList;
    }

    private int FindMaxIndex(List<Highscore> list)
    {
        float max = 0.0f;
        int maxIndex = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].TotalScore > max)
            {
                maxIndex = i;
                max = list[i].TotalScore;
            }
        }

        return maxIndex;
    }
}
