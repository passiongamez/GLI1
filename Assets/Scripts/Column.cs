using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class Column : MonoBehaviour
{
   public bool _isOccupied = false;
    bool _isRunning = false;

   [SerializeField] int _hp = 3;

    [SerializeField] GameObject _meshAndColliders;

    WaitForSeconds _rechargeWait = new WaitForSeconds(3);
    WaitForSeconds _waitTime = new WaitForSeconds(1);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(_hp <= 0)
        {
            _hp = 0;
            _meshAndColliders.SetActive(false);
        }

        if (_hp < 3 && _isRunning == false)
        {
            StartCoroutine(RechargeHP());
        }

        if(_hp == 3)
        {
            _meshAndColliders.SetActive(true);
        }
    }

    public void IsOccupied()
    {
        _isOccupied = true;
    }

    public void Vacant()
    {
        _isOccupied = false;
    }

    public void DamageColumn()
    {
        _hp--;
    }

    IEnumerator RechargeHP()
    {
        _isRunning = true;
        yield return _rechargeWait;
        while (_hp < 3)
        {
            _hp++;
            yield return _waitTime;
        }
        _isRunning = false;
    }
}
