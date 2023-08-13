using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LR9
{

    /// <summary>
    /// Основная ViewModel для главной страницы приложения.
    /// </summary>

    class ChiefVM : INotifyPropertyChanged
    {
    

        string _OriginalPath;

        public string staticPeriod; //период для распределения по папкам

        public string OriginalPath { get; set; }



        public void Sorter ()
        {
            string sourceDirectory = @"C:\FirstPath"; //откуда берем файлы
            string destinationDirectory = @"C:\PathToCopy"; //куда кладем файлы
            string[] extensions = { "*.png", "*.jpg", "*.asp" }; //расширения файлов, которые нам нужны

            // Получаем список всех .png файлов в исходной папке
            

           
            foreach (string extension in extensions) // пройдемся по всем расширениям из списка
            {
                string[] imageFiles = Directory.GetFiles(sourceDirectory, extension);  // массив строк с полным путем к исходным файлам для обращения к ним

                foreach (string imageFile in imageFiles) //проходка по всем исходным файлам
                {
                    #region CREATEPATHTOSAVE
                    // Получаем дату создания файла
                    DateTime creationDate = File.GetCreationTime(imageFile); 
                    
                    // Создаем папку в папке назначения, если она не существует
                    string destinationSubDirectory = Path.Combine(destinationDirectory, $"{creationDate.ToString("yyy")}\\{MonthList.Month[Convert.ToInt32(creationDate.ToString("MM"))]}\\{creationDate.ToString("dd")}");
                    Directory.CreateDirectory(destinationSubDirectory);

                    // Формируем путь для сохранения файла (контрольный путь + название файла)
                    string destinationPath = Path.Combine(destinationSubDirectory, Path.GetFileName(imageFile));

                    #endregion


                    //хешируем исходный файл в переменную fileHash (для дальнейшего сравнения ее с хешами файлов в новой папке для поиска копий)
                    string fileHash;
                    using (var md5 = MD5.Create())
                    {
                        using (var stream = File.OpenRead(imageFile))
                        {
                            byte[] hash = md5.ComputeHash(stream);
                            fileHash = BitConverter.ToString(hash).Replace("-", "").ToLower();

                           

                        }
                    }

                    //Удаляем копии файлов
                        if (HashListClass.HashList.Contains(fileHash))
                        {
                            File.Delete(imageFile);
                        }
                        else
                        {
                            HashListClass.HashList.Add(fileHash);
                        }

                        
                    //string HashString = HashSearch.FindFileByHash(destinationDirectory, fileHash); //определяем путь к копии файла. Если копии нет, то возвращает null.

                    //if (!String.IsNullOrEmpty(HashString)) //если значение не нулевое (есть копия), то мы ее пробуем удалить
                    //{
                    //    try
                    //    {
                    //        File.Delete(destinationPath);
                    //        Console.WriteLine("Файл успешно удален.");
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Console.WriteLine($"Ошибка при удалении файла: {ex.Message}");
                    //    }
                    //}
                    //else
                    //{
                    //    // Копируем файл в папку назначения, как только мы удалили все копии
                    //    File.Copy(imageFile, destinationPath, true);


                    //}
                }
            }
        
           

            
        }


        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      Sorter();
                  }));
            }
        }




        #region PROPERTYCHANGER
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}

