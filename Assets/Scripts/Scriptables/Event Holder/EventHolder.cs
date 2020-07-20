using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "SO/EventHolder")]
public class EventHolder : ScriptableObject
{
    public UnityEvent e;

    public virtual void Excecute()
    {
        e?.Invoke();
    }
}
