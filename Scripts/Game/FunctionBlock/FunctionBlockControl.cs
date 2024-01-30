using UnityEngine;

public enum FunctionBlockType 
{
    EMPTY,
    ARROW,
    POWERBALL,
    PROPELLER,
    HEART
}

public partial class FunctionBlockControl : MonoBehaviour
{
    [Header("��ǰ���ɾ��й��ܵ�Block������")]
    public FunctionBlockType FunctionBlockTypeInfo = FunctionBlockType.EMPTY;

    [Header("������")]
    public GameObject Arrow;

    public float SpeedOfArrow = 1f;

    [Header("����������")]
    public GameObject Propeller;
    //��ת�ٶ�
    public float RotateSpeed = 6f;

    [Header("��������")]
    public GameObject Heart;
    //�����ֱ̬��
    public float MaxSizeRadius = 5f;
    //��С��ֱ̬��
    public float MinSizeRadius = 1f;
    //�����ٶ�
    public float SpreadSpeed = 1f;
    //�����ٶ�
    public float ShrinkSpeed = 6f;

    private float loopTime = 0f;

    private GameObject playerA;
    
    private void Start()
    {
        SpeedOfArrow = 1f;

        Propeller = this.gameObject;
        Heart = this.gameObject;
        Arrow = this.gameObject;

        RotateSpeed = 6f;
        playerA = GameObject.Find("PlayerA");
    }

    private void Update()
    {
        if (FunctionBlockTypeInfo != FunctionBlockType.EMPTY)  
        {
            FunctionBlockState(FunctionBlockTypeInfo);
        }   
    }

    private void FunctionBlockState(FunctionBlockType functionBlockType) 
    {
        switch (functionBlockType) 
        {
            case FunctionBlockType.ARROW:
                ArrowFun(Arrow);
                break;

            case FunctionBlockType.POWERBALL:
                
                break;
                
            case FunctionBlockType.PROPELLER:
                PropellerFun(Propeller);
                break;

            case FunctionBlockType.HEART:
                HeartFun(Heart);
                break;
        }
    }

    private void PropellerFun(GameObject Propeller)
    {
        Propeller.transform.Rotate(Vector3.forward, RotateSpeed * Time.deltaTime, Space.World);
    }

    private void HeartFun(GameObject Heart)
    {
        //���ŵ�ʱ��
        float frontTime = (MaxSizeRadius - MinSizeRadius) / SpreadSpeed;
        //������ʱ��
        float laterTime = (MaxSizeRadius - MinSizeRadius) / ShrinkSpeed;
        if (loopTime == 0f)
        {
            Heart.transform.localScale = new Vector3(MinSizeRadius, MinSizeRadius, MinSizeRadius);
            loopTime += Time.deltaTime;
        }
        if (loopTime > 0f && loopTime <= frontTime)
        {
            Heart.transform.localScale += new Vector3(SpreadSpeed * Time.deltaTime, SpreadSpeed * Time.deltaTime, SpreadSpeed * Time.deltaTime);
            loopTime += Time.deltaTime;
        }
        if (loopTime > frontTime && loopTime <= frontTime + laterTime)
        {
            Heart.transform.localScale -= new Vector3(ShrinkSpeed * Time.deltaTime, ShrinkSpeed * Time.deltaTime, ShrinkSpeed * Time.deltaTime);
            loopTime += Time.deltaTime;
        }
        if (loopTime >= frontTime + laterTime)
        {
            loopTime = 0f;
        }

    }

    private void ArrowFun(GameObject Arrow) 
    {
        Arrow.transform.LookAt(playerA.transform, new Vector3(0, 0, 1));
        Arrow.transform.Translate(Vector3.forward * Time.deltaTime * SpeedOfArrow, Space.Self);
    }
}
