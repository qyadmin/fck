using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Timer
    {
        public bool _isTicking;//是否在计时中
        float _currentTime;//当前时间
        float _endTime;//结束时间

        public delegate void EventHander();

        public event EventHander tickEvnet;

        public Timer(float second)
        {
            _currentTime = 0;
            _endTime = second;
        }

        ///<summary>
        ///开始计时
        ///</summary>
        public void StartTimer()
        {
            _isTicking = true;
        }
        ///<summary>
        ///更新中
        ///</summary>
        public void UpdateTimer(float deltaTime)
        {
            if (_isTicking)
            {
                _currentTime += deltaTime;
            }
            if (_currentTime > _endTime)
            {
                _isTicking = false;
                tickEvnet();
            }
        }
        ///<summary>
        ///持续计时
        ///</summary>
        public void UpdateRepeatTimer(float deltaTime)
        {
            if (_isTicking)
            {
                _currentTime += deltaTime;
            }
            if (_currentTime > _endTime)
            {
                tickEvnet();
                ReStartTimer();
            }
        }



        ///<summary>
        ///停止计时
        ///</summary>
        public void StopTimer()
        {
            _isTicking = false;
        }
        ///<summary>
        ///持续计时
        ///</summary>
        public void ContinueTimer()
        {
            _isTicking = true;
        }

    ///<sumary>
    ///结束计时
    ///</sumary>
    public void EndTimer()
    {
        _currentTime = 0;
        _isTicking = false;
    }


    ///<summary>
    ///重新计时
    ///</summary>
    public void ReStartTimer()
        {
            _isTicking = true;
            _currentTime = 0;
        }
        ///<summary>
        ///重新设定计时器
        ///</summary>
        public void ResetEndTimer(float second)
        {
            _endTime = second;
        }
    }