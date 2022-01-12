using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitJoy
{
    public class MessageObject : System.Object
    {
        public int nParameter;
        public int nParameter2;
        public float fParameter;
        public float fParameter2;
        public bool bParameter;
        
        public MessageObject()
        {
            nParameter = -1;
            nParameter2 = -1;
            fParameter = -1;
            fParameter2 = -1;
            bParameter = false;
        }
    }

    public delegate void MessageEvent(System.Object data);

    public class MessageObserver
    {
        public string msgType;
        public Object target;
        public MessageEvent targetEvent;

        public MessageObserver(Object target, string msgType, MessageEvent msgEvent)
        {
            this.target = target;
            this.msgType = msgType;
            this.targetEvent = msgEvent;
        }
    }

    public static class MessageCenter
    {
        public static List<MessageObserver> observers = new List<MessageObserver>();

        public static void AddMessageObserver(Object target, string msgType, MessageEvent msgEvent)
        {
            MessageObserver observer = new MessageObserver(target, msgType, msgEvent);
            observers.Add(observer);
        }

        public static void RemoveAllObservers(Object target)
        {
            List<MessageObserver> targetObservers = new List<MessageObserver>();
            foreach (MessageObserver observer in observers)
            {
                if (observer.target == target)
                {
                    targetObservers.Add(observer);
                }
            }

            foreach (MessageObserver observer in targetObservers)
            {
                observers.Remove(observer);
            }
        }

        public static void PostMessage(string msgType)
        {
            //Debug.Log("PostMessage:" + msgType);
            List<MessageObserver> observersCopy = new List<MessageObserver>();
            observersCopy.AddRange(observers);

            foreach (MessageObserver observer in observersCopy)
            {
                if (observer.msgType.Equals(msgType))
                {
                    observer.targetEvent(null);
                }
            }
        }

        public static void PostMessageWithData(string msgType, System.Object data)
        {
            //Debug.Log("PostMessageWithData:" + msgType);
            List<MessageObserver> observersCopy = new List<MessageObserver>();
            observersCopy.AddRange(observers);

            foreach (MessageObserver observer in observersCopy)
            {
                if (observer.msgType.Equals(msgType))
                {
                    observer.targetEvent(data);
                }
            }
        }
    }
}
