﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Factory : MonoBehaviour
{
    public virtual string XmlTag {get { return ""; } }

    public virtual IEnumerable<MonoBehaviour> Create(Vector2 tileMercPos, JSONObject geo)
    {
        return null;
    }

    public virtual GameObject CreateLayer(Vector2 tileMercPos, List<JSONObject> toList)
    {
        return null;
    }
}
