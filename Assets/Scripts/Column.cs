using UnityEngine;

public class Column : MonoBehaviour
{
   public bool _isOccupied = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IsOccupied()
    {
        _isOccupied = true;
    }

    public void Vacant()
    {
        _isOccupied = false;
    }
}
