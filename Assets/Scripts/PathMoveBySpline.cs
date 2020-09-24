using UnityEngine;

/// <summary>
/// ͨ��Spline�����ζ�
/// </summary>
public class PathMoveBySpline : MonoBehaviour
{
    public Spline spline;
    public float passedTime = 0f;

    //�ζ��ٶ�
    public float speed = 0.02f;
    //�Ƕ�ƫ��
    public float rotationOffset;
    //�ζ�����
    public WrapMode wrapMode = WrapMode.Loop;

    private Transform m_selfTrans;

    void Awake()
    {
        m_selfTrans = transform;
    }

    void Update()
    {
        //ʱ���
        passedTime += Time.deltaTime * speed;
        //�������ͼ����һ��ʱ���
        float clampedParam = WrapValue(passedTime, 0f, 1f, wrapMode);
        //���ýǶ�
        m_selfTrans.rotation = spline.GetOrientationOnSpline(WrapValue(passedTime + rotationOffset, 0f, 1f, wrapMode));
        //��������
        m_selfTrans.position = spline.GetPositionOnSpline(clampedParam);
    }

    /// <summary>
    /// �������ͼ����һ��ʱ���
    /// </summary>
    private float WrapValue(float v, float start, float end, WrapMode wMode)
    {
        switch (wMode)
        {
            case WrapMode.Clamp:
            case WrapMode.ClampForever:
                return Mathf.Clamp(v, start, end);
            case WrapMode.Default:
            case WrapMode.Loop:
                return Mathf.Repeat(v, end - start) + start;
            case WrapMode.PingPong:
                return Mathf.PingPong(v, end - start) + start;
            default:
                return v;
        }
    }
}
