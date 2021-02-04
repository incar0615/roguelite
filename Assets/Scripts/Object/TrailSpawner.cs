using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using P1;

public class TrailSpawner : PoolObject, P1.GameObjects.IAttackModifierObj
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

    void OnDIsable()
    {
        StopCoroutine(makeTrailCoroutine);
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
            /// FIXME. TrailSpawner 같은 경우 PoolManager에서 Instance를 생성할때 OnEnable로 코루틴이 실행되므로 주의
            TrailObject trailObj = PoolManager.Instance.GetObjectFromPool("TrailObject", transform.position, transform.rotation)?.GetComponent<TrailObject>(); ;

            if (!trailObj) break;

            trailObj.Duration = duration;

            yield return new WaitForSeconds(period);
        }
    }
    public void ReturnObject()
    {
        StopCoroutine(makeTrailCoroutine);
        PoolManager.Instance.ReturnObjectToPool(this);
    }
}
