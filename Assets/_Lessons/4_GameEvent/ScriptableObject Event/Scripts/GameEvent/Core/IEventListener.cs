using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//來自：https://www.youtube.com/watch?v=iXNwWpG7EhM&ab_channel=DapperDino
/// <summary>
/// 事件接收器的接收功能，用於：事件被觸發時會直接呼叫 OnEventRaise 方法
/// (通常這方法會用於觸發UnityEvent)
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IEventListener<T>
{
    void OnEventRaise(T parameter);
}
