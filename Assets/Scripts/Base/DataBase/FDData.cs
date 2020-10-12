using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDData
{
    public virtual void BuildData(int _typeID, int _grade)
    {

    }
}

public class FDStaticData
{
    public virtual void BuildData(int _typeID, int _grade)
    {

    }
}

public class FDDynamicData
{
    public virtual void BuildData(FDStaticData _staticData)
    {

    }
}
