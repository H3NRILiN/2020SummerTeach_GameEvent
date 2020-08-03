using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Variable/IntItemPair")]
public class IntItemPairVariable : VariableCore<IntItemPair>
{

}

[System.Serializable]
public class IntItemPair
{
    public int intValue;
    public ItemObject itemValue;
}