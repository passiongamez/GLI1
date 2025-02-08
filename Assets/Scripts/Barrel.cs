using System.Collections;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] GameObject _explosion;

    MeshRenderer _renderer;

    WaitForSeconds _waitTime = new WaitForSeconds(2.5f);
    WaitForSeconds _quickTime = new WaitForSeconds(1f);

    AIBehavior _behavior;

    [SerializeField] AudioSource _audioSource;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

   public void ExplosionOccurence()
    {
        Collider[] hitCollider = Physics.OverlapSphere(_explosion.transform.position, 7f);
        foreach(Collider collider in hitCollider)
        {
            if(collider.tag == "Enemy")
            {
               _behavior = collider.GetComponent<AIBehavior>();
                Debug.Log("found ai script");
                if(_behavior != null)
                {
                    _behavior.ExplosionDeath();
                }
            }
        }
    }

    public void StartExplosion()
    {
        StartCoroutine(ExplosionRoutine());
    }

    IEnumerator ExplosionRoutine()
    {
        Debug.Log("Explosion routine started");
        _explosion.SetActive(true);
        yield return _quickTime;
        _renderer.enabled = false;
        yield return _waitTime;
        gameObject.SetActive(false);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawSphere(_explosion.transform.position, 7f);
    //}
}
