using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int score;
    public int getScore()
    {
        return score;
    }
    public void setScore(int nScore)
    {
        score = nScore;
    }
}
