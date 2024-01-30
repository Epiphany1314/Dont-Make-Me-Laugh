using UnityEngine;

public class SmileCollect : MonoBehaviour
{
    private ProcessManager processManager;

    private int smileANumber = 1;

    //���Aײ��һ�������ϰ������B��õķ���
    private int smileBNumber = 2;

    private void Start()
    {
        processManager = GameObject.Find("ProcessManager").GetComponent<ProcessManager>();
    }

    private void Update()
    {
        smileANumber = processManager.AddSmileValueANumber;
        //��������ͬ����processManager�ж���
        //smileBNumber = processManager.AddSmileValueBNumber;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collections")
        {
            processManager.SmileValueA += smileANumber;
            Debug.Log(processManager.SmileValueA);
            GameObject.Destroy(other.gameObject);
        }

        //if (other.tag == "BasicBlock")
        //{
        //    processManager.SmileValueB += smileBNumber;
        //    GameObject.Destroy(other.gameObject);
        //}
    }
}