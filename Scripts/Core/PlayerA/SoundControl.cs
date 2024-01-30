using UnityEngine;

public enum PlayerAMoveState 
{ 
    NormalMove,
    ImpedeMove
}

[RequireComponent(typeof(Rigidbody))]
public partial class SoundControl : MonoBehaviour
{
    [Header("PlayerA��X��ĵ�ǰ�ƶ��ٶ�")]
    public float SpeedX = 100.0f;

    [Header("PlayerA��X����Normal��Impede�ƶ��ٶ�")]
    public float SpeedXNormal;
    public float SpeedXImpede;

    [Header("������С")]
    public static float Volume = default;

    [Header("��ǰ������������")]
    public GameObject PlayerA = null;

    [Header("������С")]
    public float gravity = 0.8f;

    [Header("Volume���Ŵ�С")]
    public float volumeForceScale = 2f;

    [Header("�����������")]
    public int maxVolume = 3;

    [Header("��ǰ���A���ƶ�״̬")]
    public PlayerAMoveState CurrentPlayerMoveState = PlayerAMoveState.NormalMove;

    private Rigidbody rigidbody = null;
    private AudioClip micRecord = default;
    private string device = default;
    private const int sampleSize = 128;

    [SerializeField]
    private float[] samples = new float[sampleSize];

    private void Start()
    {
        //��˷��豸����Ƶ��ʼ��
        device = Microphone.devices[0];
        micRecord = Microphone.Start(device,true,999,44100);

        // ��ʱ
        if (PlayerA == null) 
        {
            //����������ƶ���
            PlayerA = GameObject.Find("PlayerA");
        }

        if (rigidbody == null)  
        {
            rigidbody = this.gameObject.GetComponent<Rigidbody>();
        }
    }

    private void Update()
    {
        Volume = GetVolume();
        // Debug.Log("��ǰ������Ϊ��" + Volume);

        // �������A״̬�������ٶ�
        PlayerMoveStateControl(CurrentPlayerMoveState);

        //ִ�����A��X����λ��
        XDirectionMove(PlayerA.transform);
        //XDirectionMove(rigidbody);

        //ִ�����A��Y����λ��
        SoundControlMoveZ(Volume,rigidbody);
    }

    #region ��Ƶģ��
    private float GetVolume() 
    {
        int offset = Microphone.GetPosition(device) - sampleSize + 1;// -128 + 1
        if (offset < 0) 
        {
            Debug.LogWarning("��������ʧ�ܣ�");
            return 0;
        }

        // �����Ƶ��Ϣ,�洢��AudioClip����
        micRecord.GetData(samples,offset);

        float levelMax = 0;
        for (int i = 0; i < sampleSize; ++i) 
        {
            float wavePeak = samples[i];
            if (levelMax < wavePeak)  
            {
                levelMax = wavePeak;
            }
        }

        return levelMax * 99;
    }

    #endregion

    #region ���A�ƶ�����ģ��
    private void XDirectionMove(Transform playerATransform) 
    {
        float ForceNumber = SpeedX * Time.deltaTime;
        Vector3 forceVector = new Vector3(ForceNumber, 0, 0);
        playerATransform.Translate(forceVector);
    }

    //private void XDirectionMove(Rigidbody rb)
    //{
    //    float ForceNumber = SpeedX * Time.deltaTime;
    //    Vector3 forceVector = new Vector3(ForceNumber, 0, 0);
    //    rb.AddForce(forceVector, ForceMode.Force);
    //    //playerATransform.Translate(forceVector);
    //}

    private void SoundControlMoveZ(float volume, Rigidbody playerRb)
    {
        if (volume > maxVolume)
            volume = maxVolume;
        int intVolume = ((int)((int)volume / 3)) * 3;
        float totalForce = volume * volumeForceScale - gravity;
        Vector3 forceVector = new Vector3(0, totalForce, 0);
        playerRb.AddForce(forceVector, ForceMode.Force);
    }
    #endregion

    #region ���A�ƶ�״̬����ģ��
    private void PlayerMoveStateControl(PlayerAMoveState currentPlayerMoveState) 
    {
        if (currentPlayerMoveState == PlayerAMoveState.NormalMove) 
        {
            SpeedX = SpeedXNormal;
        }

        if (currentPlayerMoveState == PlayerAMoveState.ImpedeMove) 
        {
            SpeedX = SpeedXImpede;
        }
    }
    #endregion

    #region ����
    private float SpeedY = 100.0f;
    private float ForceScale = 1;

    private void ControlCube(float tempVolume, GameObject soundObject)
    {
        if (tempVolume > 0f)  
        {
            float y = tempVolume;

            float x = soundObject.transform.localScale.x;
            float z = soundObject.transform.localScale.z;

            soundObject.transform.localScale = new Vector3(x,y,z);
        }
    }

    private void YDirectionMove(Rigidbody rb)
    {
        float ForceNumber = SpeedY * Time.deltaTime;
        Vector3 forceVector = new Vector3(0, -ForceNumber, 0);
        rb.AddForce(forceVector, ForceMode.Force);
    }

    private void SoundControlMove(float volume, Rigidbody rb)
    {
        float ForceNumber = volume * Time.deltaTime * ForceScale;
        Vector3 forceVector = new Vector3(0, ForceNumber, 0);
        rb.AddForce(forceVector, ForceMode.Force);
    }
    #endregion
}
