[System.Serializable]
public class Highscore
{
    private float _time;
    private int _score;
    public float TotalScore;

    public Highscore(float time, int score)
    {
        _time = time;
        _score = score;

        TotalScore = _time * _score;
    }
}
