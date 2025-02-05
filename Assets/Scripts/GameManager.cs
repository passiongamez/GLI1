using UnityEngine;

public class GameManager : MonoBehaviour
{
    int _points = 0;


    public void AddPoints(int points)
    {
        _points += points;
    }
}
