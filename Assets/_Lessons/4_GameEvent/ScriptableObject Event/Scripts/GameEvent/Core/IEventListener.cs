using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//來自：https://www.youtube.com/watch?v=iXNwWpG7EhM&ab_channel=DapperDino
public interface IEventListener<T>
{
    void OnEventRaise(T parameter);
}
