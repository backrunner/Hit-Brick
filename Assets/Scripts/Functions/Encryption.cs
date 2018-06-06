using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class Encryption : MonoBehaviour {

    public static string secure_key = "backrunner!yUgSCYDooxTNHRmjF6iBR";

    public static string encrypt_aes(string data)
    {
        byte[] bs = Encoding.UTF8.GetBytes(data);

        RijndaelManaged aes256 = new RijndaelManaged();
        aes256.Key = Encoding.UTF8.GetBytes(secure_key);
        aes256.Mode = CipherMode.ECB;
        aes256.Padding = PaddingMode.PKCS7;

        return Convert.ToBase64String(aes256.CreateEncryptor().TransformFinalBlock(bs, 0, bs.Length));
    }

    public static string decrypt_aes(string data)
    {
        byte[] bs = Convert.FromBase64String(data);

        RijndaelManaged aes256 = new RijndaelManaged();
        aes256.Key = Encoding.UTF8.GetBytes(secure_key);
        aes256.Mode = CipherMode.ECB;
        aes256.Padding = PaddingMode.PKCS7;

        return Encoding.UTF8.GetString(aes256.CreateDecryptor().TransformFinalBlock(bs, 0, bs.Length));
    }

    public static string SHA512(string password)
    {
        byte[] bytes = Encoding.UTF7.GetBytes(password);
        byte[] result;
        SHA512 shaM = new SHA512Managed();
        result = shaM.ComputeHash(bytes);
        StringBuilder sb = new StringBuilder();
        foreach (byte b in result)
        {
            sb.AppendFormat("{0:x2}", b);
        }
        return sb.ToString();
    }
}
