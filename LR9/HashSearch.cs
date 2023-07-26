using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LR9
{
    /// <summary>
    /// Do search files from given folder by theit Hash
    /// </summary>
    public class HashSearch
    {
        public static string FindFileByHash(string folderPath, string hashToFind)
        {
            
            // Ищем файлы в текущей папке
            string[] files = Directory.GetFiles(folderPath);
            foreach (string file in files)
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(file))
                    {
                        byte[] hash = md5.ComputeHash(stream);
                        string fileHash = BitConverter.ToString(hash).Replace("-", "").ToLower();

                        if (fileHash.Equals(hashToFind))
                        {

                            return file; // Возвращаем путь к найденному файлу
                        }
                    }
                }
            }

            // Рекурсивно ищем файл по хэшу в подпапках
            string[] subfolders = Directory.GetDirectories(folderPath);

            foreach (string subfolder in subfolders)
            {
                string filePath = FindFileByHash(subfolder, hashToFind);
                if (!string.IsNullOrEmpty(filePath))
                {
                    return filePath; // Возвращаем путь к найденному файлу из подпапки
                }
            }

            return null; // Файл не найден
        }
    }
}
