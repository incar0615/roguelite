using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSpawner : MonoBehaviour, P1.GameObjects.IAttackModifierObj
{
    [SerializeField]
    private P1.GameObjects.Element element;
    public P1.GameObjects.Element Element{ get { return element; } set { element = value; } }

    [SerializeField]
    private float duration;
    public float Duration { get { return duration; } set { duration = value; } }

    [SerializeField]
    private float period;
    public float Period { get { return period; } set { period = value; } }

    private Coroutine makeTrailCoroutine;

    void OnEnable()
    {
        makeTrailCoroutine = StartCoroutine(MakeTrailRoutine());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MakeTrailRoutine()
    {
        while(true)
        {
            var trailObj = P1.PoolManager.GetTrailObject();

            trailObj.Duration = duration;
            trailObj.transform.position = transform.position;

            yield return new WaitForSeconds(period);
        }
    }
    public void ReturnObject()
    {
        StopCoroutine(makeTrailCoroutine);
        P1.PoolManager.ReturnObject(this);
    }
}
