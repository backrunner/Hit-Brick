using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour {

    public static void setData(string key, long data)
    {
        PlayerPrefs.SetString(Encryption.SHA512(key), Encryption.encrypt_aes(data.ToString().Trim()));
    }
    public static void setData(string key, int data)
    {
        PlayerPrefs.SetString(Encryption.SHA512(key), Encryption.encrypt_aes(data.ToString().Trim()));
    }
    public static void setData(string key, string data)
    {
        PlayerPrefs.SetString(Encryption.SHA512(key), Encryption.encrypt_aes(data.Trim()));
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

    public static string getString(string key)
    {
        if (PlayerPrefs.HasKey(Encryption.SHA512(key)))
        {
            string data = PlayerPrefs.GetString(Encryption.SHA512(key));
            return Encryption.decrypt_aes(data);
        }
        else
        {
            return null;
        }
    }

    public static bool getBool(string key)
    {
        if (PlayerPrefs.HasKey(Encryption.SHA512(key)))
        {
            string data = PlayerPrefs.GetString(Encryption.SHA512(key));
            if (Encryption.decrypt_aes(data.Trim()) == "true")
            {
                return true;
            } else
            {
                return false;
            }
        }
        return false;
    }

    public static void setBool(string key,bool value)
    {
        if (value)
        {
            PlayerPrefs.SetString(Encryption.SHA512(key), Encryption.encrypt_aes("true"));
        } else
        {
            PlayerPrefs.SetString(Encryption.SHA512(key), Encryption.encrypt_aes("false"));
        }
    }

    public static bool checkKey(string key)
    {
        return PlayerPrefs.HasKey(Encryption.SHA512(key));
    }

    public static void purge()
    {
        PlayerPrefs.DeleteAll();
        Destroy(gameController.eventSystem);
        Destroy(gameController.thisgameObj);
        SceneManager.LoadSceneAsync(0);
    }
}
