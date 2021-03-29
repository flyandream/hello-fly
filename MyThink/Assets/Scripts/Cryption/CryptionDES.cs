using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class CryptionDES 
{
    #region DES
    private static byte[] keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
    /// <summary>
    /// des加密
    /// </summary>
    /// <param name="encryptString">将加密的字符串</param>
    /// <param name="key">加密密钥，8位</param>
    /// <returns>加密成功后返回加密后的字符串，失败返回源串</returns>
    public static string EncryptDES(string encryptString, string key = "45131929")
    {
        try
        {
            byte[] rgbkey = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] rgbIV = keys;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider dcsp = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, dcsp.CreateEncryptor(), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            cStream.Close();
            return Convert.ToBase64String(mStream.ToArray());
        }
        catch
        {
            Debug.LogError("StringEncrypt/EncryptDES()/Encrypt error");
            return encryptString;
        }
    }/// <summary>
     /// des解密
     /// </summary>
     /// <param name="decryptString"></param>
     /// <param name="key"></param>
     /// <returns></returns>
    public static string DecryptDES(string decryptString, string key = "45131929")
    {
        try
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(key);
            byte[] rgbIV = keys;
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            cStream.Close();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }
        catch
        {
            Debug.LogError("StringEncrypt/DecryptDES()/ Decrypt error!");
            return decryptString;
        }
    }
    #endregion;

    #region AES
    /// <summary>
    /// aes解密
    /// </summary>
    /// <param name="content">明文</param>
    /// <param name="key">密钥</param>
    /// <returns></returns>
    public static string AESEncrypt(string content,string key)
    {
        if (string.IsNullOrEmpty(content))
        {
            return null;
        }
        byte[] contentBytes = Encoding.UTF8.GetBytes(content);
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        RijndaelManaged rm = new RijndaelManaged();
        rm.Key = keyBytes;
        rm.Mode = CipherMode.ECB;
        rm.Padding = PaddingMode.PKCS7;
        ICryptoTransform ict = rm.CreateEncryptor();
        byte[] resultBytes = ict.TransformFinalBlock(contentBytes, 0, contentBytes.Length);
        return Convert.ToBase64String(resultBytes, 0, resultBytes.Length);
    }

    public static string AESDecrypt(string content,string key) 
    {
        if (string.IsNullOrEmpty(content))
        {
            return null;
        }
        byte[] contentBytes = Convert.FromBase64String(content);
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        RijndaelManaged rm = new RijndaelManaged();
        rm.Key = keyBytes;
        rm.Mode = CipherMode.ECB;
        rm.Padding = PaddingMode.PKCS7;
        ICryptoTransform ict = rm.CreateDecryptor();
        byte[] resultBytes = ict.TransformFinalBlock(contentBytes,0,contentBytes.Length);
        return Encoding.UTF8.GetString(resultBytes);
    }

    #endregion;

}
