using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace HostData.Serialization;

internal static class Json
{
    public static string Serialization<T>(T instance, string key)
    {
        var json = JsonSerializer.Serialize(instance);
        return Encrypt(json, key);
    }

    public static string Serialization<T>(IEnumerable<T> instance, string key)
    {
        var json = JsonSerializer.Serialize(instance);
        return Encrypt(json, key);
    }

    public static T Deserialize<T>(string json, string key)
    {
        var decryptJson = Decrypt(json, key);
        return JsonSerializer.Deserialize<T>(decryptJson);
    }

    public static IEnumerable<T> DeserializeToList<T>(string json, string key)
    {
        var decryptJson = Decrypt(json, key);
        return JsonSerializer.Deserialize<List<T>>(decryptJson);
    }

    private static string Encrypt(string clearText, string key)
    {
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            using (Rfc2898DeriveBytes pdb = new(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 }))
            {
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
            }
            using MemoryStream ms = new();
            using (CryptoStream cs = new(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(clearBytes, 0, clearBytes.Length);
                cs.Close();
            }
            clearText = Convert.ToBase64String(ms.ToArray());
        }
        return clearText;
    }

    private static string Decrypt(string cipherText, string key)
    {
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            using (Rfc2898DeriveBytes pdb = new(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 }))
            {
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
            }
            using MemoryStream ms = new();
            using (CryptoStream cs = new(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(cipherBytes, 0, cipherBytes.Length);
                cs.Close();
            }
            cipherText = Encoding.Unicode.GetString(ms.ToArray());
        }
        return cipherText;
    }
}