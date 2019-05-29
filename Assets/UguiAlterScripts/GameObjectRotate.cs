using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UguiAlter;

public class GameObjectRotate : MonoBehaviour
{

    /// <summary>
    /// 动画参数
    /// </summary>
    [SerializeField]
    public UguiTweenClass Tweener;

    public bool IsRunAwake;

    private float time_counter;
    private float _rotateTime;
    private RectTransform this_rectTrans;
    private bool is_runAnim = false;
    private bool _run = false;

    private Quaternion _initialQuaternion;

    public UnityEvent EndEvent;

    //Use this for initialization
    void Start()
    {
        this_rectTrans = gameObject.GetComponent<RectTransform>();
        _initialQuaternion = this_rectTrans.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (_run)
        {
            time_counter += Time.deltaTime;
            if (time_counter > Tweener.DelayTime)
            {
                is_runAnim = true;
                _run = false;
                time_counter = 0;
                _rotateTime = 0;
            }
        }
        if (is_runAnim)
        {
            _rotateTime += Time.deltaTime;
            if (_rotateTime >= Tweener.DurationTime)
            {
                is_runAnim = false;
                _rotateTime = 0;
                this_rectTrans.localRotation = _initialQuaternion;
                if (EndEvent != null)
                    EndEvent.Invoke();
            }
            else
                this_rectTrans.Rotate(GetRotateType(Tweener.RotateAnimType));
        }
    }

    public void InitAnimation()
    {
        _run = true;
    }

    public void ResetAnimation()
    {
        _rotateTime = 0;
    }

    public void CloseAnimation()
    {
        _run = false;
        is_runAnim = false;
    }

    private Vector3 GetRotateType(RotateType type)
    {
        var temp = Vector3.zero;
        switch (type)
        {
            case RotateType.RotateX:
                temp = Vector3.right;
                break;
            case RotateType.RotateY:
                temp = Vector3.up;
                break;
            case RotateType.RotateZ:
                temp = Vector3.forward;
                break;
        }
        return temp;
    }

    private void OnEnable()
    {
        if (IsRunAwake)
            _run = true;
    }
}