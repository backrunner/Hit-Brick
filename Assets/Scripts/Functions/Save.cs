using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour {

    public static void setData(string key, long data)
    {
        PlayerPrefs.SetString(Encryption.SHA512(key), Encryption.encrypt_aes(data.ToString().Trim()));
    }

    public static long getData(string key)
    {
        if (PlayerPrefs.HasKey(Encryption.SHA512(key)))
        {
            string data = PlayerPrefs.GetString(Encryption.SHA512(key));
            return long.Parse(Encryption.decrypt_aes(data));
        }
        else
        {
            return 0;
        }
    }
    public static bool checkKey(string key)
    {
        return PlayerPrefs.HasKey(Encryption.SHA512(key));
    }
}
