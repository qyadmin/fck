using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UguiAlter;

public class UguiTweenScale : MonoBehaviour
{
    /// <summary>
    /// 动画的起始位置
    /// </summary>
    public Vector3 ScaleFrom;

    /// <summary>
    /// 动画的终点为止
    /// </summary>
    public Vector3 ScaleTo;

    /// <summary>
    /// 动画参数
    /// </summary>
    [SerializeField]
    public UguiTweenClass Tweener;

    public  bool IsRunAwake;

    private float time_counter;
    private RectTransform this_rectTrans;
    private bool is_runAnim = false;
    private Vector3 this_transPos;
    private Vector3 old_transPos;
    private Vector3 disVec;
    private float moveSpeed;
    private bool PingPong_end = false;

    public UnityEvent EndEvent;

    // Use this for initialization
    void Start()
    {
        this_rectTrans = gameObject.GetComponent<RectTransform>();
        this_transPos = ScaleFrom;
        old_transPos = ScaleFrom;
        disVec = ScaleTo - ScaleFrom;
        moveSpeed = disVec.magnitude / Tweener.DurationTime;
        transform.localScale = ScaleFrom;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRunAwake)
        {
            time_counter += Time.deltaTime;
            if (time_counter > Tweener.DelayTime)
            {
                is_runAnim = true;
                IsRunAwake = false;
                time_counter = 0;
            }
        }

        if (is_runAnim)
        {
            Anim_Run();
        }
    }

    public void InitAnimation()
    {
        this_transPos = ScaleFrom;
        old_transPos = ScaleFrom;
        disVec = ScaleTo - ScaleFrom;
        moveSpeed = disVec.magnitude / Tweener.DurationTime;
        transform.localScale = ScaleFrom;
        IsRunAwake = true;
    }

    private void Anim_Run()
    {
        
        
        if (PingPong_end)
        {
            this_transPos -= (disVec * moveSpeed * Time.deltaTime) / disVec.magnitude;
        }
        else
        {
            this_transPos += (disVec * moveSpeed * Time.deltaTime) / disVec.magnitude;
        }

        //限制坐标值的大小，这样可以使用==来直接判断位置到达的地方
        var minX = ScaleFrom.x > ScaleTo.x ? ScaleTo.x : ScaleFrom.x;
        var maxX = ScaleFrom.x > ScaleTo.x ? ScaleFrom.x : ScaleTo.x;
        var minY = ScaleFrom.y > ScaleTo.y ? ScaleTo.y : ScaleFrom.y;
        var maxY = ScaleFrom.y < ScaleTo.y ? ScaleTo.y : ScaleFrom.y;
        var minZ = ScaleFrom.z > ScaleTo.z ? ScaleTo.z : ScaleFrom.z;
        var maxZ = ScaleFrom.z < ScaleTo.z ? ScaleTo.z : ScaleFrom.z;
        this_transPos.x = Mathf.Clamp(this_transPos.x, minX, maxX);
        this_transPos.y = Mathf.Clamp(this_transPos.y, minY, maxY);
        this_transPos.z = Mathf.Clamp(this_transPos.z, minZ, maxZ);

        var temp = this_transPos - old_transPos;

        switch (Tweener.AnimType)
        {
            case UguiAlter.ExeType.Once:
                {
                    //this_transPos += (disVec * moveSpeed * Time.deltaTime) / disVec.magnitude;

                    if (temp == Vector3.zero)
                    {
                        is_runAnim = false;
                        if (EndEvent != null)
                            EndEvent.Invoke();
                    }
                }
                break;
            case UguiAlter.ExeType.Loop:
                {
                    if (temp == Vector3.zero)
                    {
                        this_transPos = ScaleFrom;
                    }
                    //else
                    //{
                    //    this_transPos += (disVec * moveSpeed * Time.deltaTime) / disVec.magnitude;
                    //}

                }
                break;
            case UguiAlter.ExeType.PinpPong:
                {
                    var to = this_transPos - ScaleTo;
                    var from = this_transPos - ScaleFrom;
                    if ((temp == Vector3.zero && to.magnitude < from.magnitude) || this_transPos == ScaleTo)
                    {
                        PingPong_end = true;
                    }
                    else if ((temp == Vector3.zero && to.magnitude > from.magnitude) || this_transPos == ScaleFrom)
                    {
                        PingPong_end = false;
                    }
                    //if (PingPong_end)
                    //{
                    //    this_transPos -= (disVec * moveSpeed * Time.deltaTime) / disVec.magnitude;
                    //}
                    //else
                    //{
                    //    this_transPos += (disVec * moveSpeed * Time.deltaTime) / disVec.magnitude;
                    //}
                }
                break;
        }

        //先判断坐标类型
        if (Tweener.AnimCoordType == UguiAlter.CoordinateType.Local)
        {
            transform.localScale = this_transPos;
            old_transPos = this_transPos;
        }
        else
        {
            transform.localScale = this_transPos;
            old_transPos = this_transPos;
        }
    }
}