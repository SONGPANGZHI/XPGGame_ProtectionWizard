using UnityEngine;

public class ScoreManagement : MonoBehaviour
{
    public static ScoreManagement Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public int TotalScore;

    //µÃ·Ö
    public int GetScore(int addNum)
    {
        return TotalScore += addNum;
    }

    //¿Û·Ö
    public int DeductionScore(int reduceNum)
    {
        int currentScore = TotalScore -= reduceNum;

        if (currentScore<=0)
            TotalScore = 0;
        else
            return reduceNum;

        return 0;
    }
}
