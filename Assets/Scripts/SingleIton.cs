using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleIton <T>: MonoBehaviour where  T:MonoBehaviour
{
    static T _ins;
    public static T _Ins { get { return _ins; } }

   public virtual void Awake()
    {
        _ins = this as T;
    }
}
