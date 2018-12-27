using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine; 

public class MpTextMessage
{
    public string type = null;
    public string content = null;

    public MpTextMessage(string type)
    {
        this.type = type;
        this.content = "";
    }

    public MpTextMessage(string type, string content)
    {
        this.type = type;
        this.content = content;
    }

    public override string ToString()
    {
        if (type != null && content != null)
        {
            return JsonUtility.ToJson(this);
        } else
        {
            return null;
        }
    }

    public byte[] GetBytes()
    {
        return Encoding.UTF8.GetBytes(this.ToString());
    }
}

