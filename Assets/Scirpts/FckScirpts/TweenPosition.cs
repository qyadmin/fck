using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class TweenPosition : MonoBehaviour
{
    /// <summary>
    /// 动画的起始位置
    /// </summary>
    [HideInInspector]
    public Vector3 PostionFrom;

    /// <summary>
    /// 动画的终点为止
    /// </summary>
    [HideInInspector]
    public Vector3 PostionTo;

    /// <summary>
    /// 动画参数
    /// </summary>
    [SerializeField]
    public TweenerClass Tweener;

    private float time_counter;
    private RectTransform this_rectTrans;
    private bool is_runAnim = false;
    private bool is_runOnce = false;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (is_runOnce)
        {
            time_counter += Time.deltaTime;
            if (time_counter > Tweener.DelayTime)
            {
                is_runAnim = true;
                is_runOnce = false;
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
        this_transPos = PostionFrom;
        old_transPos = PostionFrom;
        disVec = PostionTo - PostionFrom;
        moveSpeed = disVec.magnitude / Tweener.DurationTime;
        is_runOnce = true;
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
        var minX = PostionFrom.x > PostionTo.x ? PostionTo.x : PostionFrom.x;
        var maxX = PostionFrom.x > PostionTo.x ? PostionFrom.x : PostionTo.x;
        var minY = PostionFrom.y > PostionTo.y ? PostionTo.y : PostionFrom.y;
        var maxY = PostionFrom.y < PostionTo.y ? PostionTo.y : PostionFrom.y;
        var minZ = PostionFrom.z > PostionTo.z ? PostionTo.z : PostionFrom.z;
        var maxZ = PostionFrom.z < PostionTo.z ? PostionTo.z : PostionFrom.z;
        this_transPos.x = Mathf.Clamp(this_transPos.x, minX, maxX);
        this_transPos.y = Mathf.Clamp(this_transPos.y, minY, maxY);
        this_transPos.z = Mathf.Clamp(this_transPos.z, minZ, maxZ);

        var temp = this_transPos - old_transPos;

        switch (Tweener.AnimType)
        {
            case ExeType.Once:
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
            case ExeType.Loop:
                {
                    if (temp == Vector3.zero)
                    {
                        this_transPos = PostionFrom;
                    }
                    //else
                    //{
                    //    this_transPos += (disVec * moveSpeed * Time.deltaTime) / disVec.magnitude;
                    //}

                }
                break;
            case ExeType.PinpPong:
                {
                    var to = this_transPos - PostionTo;
                    var from = this_transPos - PostionFrom;
                    if ((temp == Vector3.zero && to.magnitude < from.magnitude) || this_transPos == PostionTo)
                    {
                        PingPong_end = true;
                    }
                    else if ((temp == Vector3.zero && to.magnitude > from.magnitude) || this_transPos == PostionFrom)
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
        if (Tweener.AnimCoordType == CoordinateType.Local)
        {
            this_rectTrans.localPosition = this_transPos;
            old_transPos = this_transPos;
        }
        else
        {
            this_rectTrans.position = this_transPos;
            old_transPos = this_transPos;
        }


    }
}