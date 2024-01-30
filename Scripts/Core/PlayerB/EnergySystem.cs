using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    [Header("�����ָ����")]
    public float EnergyGrowTime = 1.0f;

    [Header("���������ָ���ֵ")]
    public int EnergyGrowNumber = 1;

    [Header("���ֹ��ܿ����������ֵ")]
    public int BasicBlock1_Energy = 1;
    public int BasicBlock2_Energy = 1;
    public int BasicBlock3_Energy = 1;
    public int BasicBlock4_Energy = 1;
    public int BasicBlock5_Energy = 1;

    [Header("��ǰ����ֵ")]
    [SerializeField]
    private int energyNumber = 0;

    public int EnergyNumber 
    {
        get 
        {
            return energyNumber;
        }
        set 
        {
            if (value >= 10)
            {
                energyNumber = 10;
            }
            else 
            {
                energyNumber = value;
            }
        }
    }

    private float timeRecord = 0;

    private void Update()
    {
        EnergyGrow();
    }

    private void EnergyGrow() 
    {
        timeRecord += Time.deltaTime;

        if (timeRecord >= EnergyGrowTime) 
        {
            timeRecord = 0;
            this.EnergyNumber += EnergyGrowNumber;
        }
    }
}
