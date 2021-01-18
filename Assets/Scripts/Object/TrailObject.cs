using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailObject : MonoBehaviour
{
    [SerializeField]
    private P1.GameObjects.Element element;
    public P1.GameObjects.Element Element { get { return element; } set { element = value; } }

    [SerializeField]
    private float duration;
    public float Duration { get { return duration; } set { duration = value; } }

    private Coroutine lifeCoroutine;

    void OnEnable()
    {
        lifeCoroutine = StartCoroutine(LifeCoroutine());
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LifeCoroutine()
    {
        // PoolManager 에서 GetObject를 하자마자 Active를 변경시켜서 OnEnable()이 호출되기떄문에 
        // 한 프레임 쉬어준다.
        yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(duration);

        P1.PoolManager.ReturnObject(this);
    }
}
