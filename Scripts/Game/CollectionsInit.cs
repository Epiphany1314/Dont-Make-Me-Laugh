using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionsInit : MonoBehaviour
{
    [Header("Ц��Ԥ����ģ��")]
    public GameObject CollitionsPrefeb;

    [Header("Ц������")]
    public int CollitionsNum;

    [Header("Y����������ɷ�Χ����")]
    public float RandomValue = 5f;

    public GameObject start;
    public GameObject end;
    
    void Start()
    {
        if (CollitionsPrefeb == null) 
        {
            CollitionsPrefeb = Resources.Load<GameObject>("Smile/SmileObject");
        }

        if (start == null || end == null) 
        {
            start = GameObject.Find("start");
            end = GameObject.Find("end");
        }
        
        float diff = (end.transform.position.x - start.transform.position.x) / (CollitionsNum + 1);

        // ����λ�õ�
        Vector3 initPosition = start.transform.position;

        for(int i = 0; i < CollitionsNum; i++) 
        {
            initPosition.x += diff;
            initPosition.y = Random.Range(-RandomValue, +RandomValue);
            Instantiate(CollitionsPrefeb, initPosition, Quaternion.identity, transform);
        }
    }

}
