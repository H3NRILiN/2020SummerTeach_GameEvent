using System.Collections;
using System.Collections.Generic;
using ISU.Example.Inventory;
using UnityEngine;
namespace ISU.Example
{
    [CreateAssetMenu(menuName = "_Finished/Variable/IntItemPair")]
    public class IntItemPairVariable : VariableCore<IntItemPair>
    {

    }

    [System.Serializable]
    public class IntItemPair
    {

        public ItemObject itemValue;
        public int intValue;
        public IntItemPair()
        {
        }

        public IntItemPair(ItemObject itemValue, int intValue)
        {
            this.intValue = intValue;
            this.itemValue = itemValue;
        }
    }
}