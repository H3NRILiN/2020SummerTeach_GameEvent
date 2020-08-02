using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Variable/ListQuest")]
public class ListQuestVariable : VariableCore<List<Quest>>
{
    private void OnEnable()
    {
        if (!useStaticValue)
            value = new List<Quest>();
    }
}
