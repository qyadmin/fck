using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Center.Message
{

    //泛型分发机制
    public class EventPatcher<T>
    {
        public Action<T> listener;

        public void Addlistener(Action<T> GetObj)
        {
            listener += GetObj;
        }

        public void Removelistener(Action<T> GetObj)
        {
            listener -= GetObj;
        }

        public void Send(T GetObj)
        {
            if (listener != null)
                listener.Invoke(GetObj);
        }
        public void ClearAllEevnt()
        {
            while (this.listener != null)
            {
                this.listener -= this.listener;
            }
        }
    }


    //无参数分发机制
    public class EventNoneParamPatcher
    {
        public Action listener;

        public void Addlistener(Action GetObj)
        {
            listener += GetObj;
        }

        public void Removelistener(Action GetObj)
        {
            listener -= GetObj;
        }

        public void Send()
        {
            if (listener != null)
                listener.Invoke();
        }

        public void ClearAllEevnt()
        {
            while (this.listener != null)
            {
                this.listener -= this.listener;
            }
        }
    }
}
