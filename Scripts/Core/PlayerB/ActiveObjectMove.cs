using UnityEngine;

public class ActiveObjectMove : MonoBehaviour
{
    private GameObject playerA;
    private GameObject activeObject;
    private float offsetX;

    private void Start()
    {
        if (playerA == null)  
        {
            playerA = GameObject.Find("PlayerA");
        }

        if (activeObject == null) 
        {
            activeObject = this.gameObject;
        }

        offsetX = activeObject.transform.position.x - playerA.transform.position.x;
    }

    private void Update()
    {
        if (activeObject != null) 
        {
            // ���µ�ǰActiveObject�����λ�ã������Camera����ͬ��
            activeObject.transform.position = new Vector3(playerA.transform.position.x + offsetX, activeObject.transform.position.y, activeObject.transform.position.z);
        }
    }
}
