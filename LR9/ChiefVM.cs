using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LR9
{
    enum Months
    {
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12,
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7
    };

    class ChiefVM : INotifyPropertyChanged
    {
    

        string _OriginalPath;

        public string OriginalPath { get; set; }



        public void Sorter ()
        {
            string sourceDirectory = @"C:\FirstPath";
            string destinationDirectory = @"C:\PathToCopy";
            string[] extensions = { "*.png", "*.jpg", "*.asp" };

            // Получаем список всех .png файлов в исходной папке
            

           
            foreach (string extension in extensions) // пройдемся по всем расширениям из списка
            {
                string[] imageFiles = Directory.GetFiles(sourceDirectory, extension);

                foreach (string imageFile in imageFiles) //проходка по всем файлам
                {
                    // Получаем дату создания файла
                    DateTime creationDate = File.GetCreationTime(imageFile);

                    // Создаем папку в папке назначения, если она не существует
                    string destinationSubDirectory = Path.Combine(destinationDirectory, $"{creationDate.ToString("yyy")}\\{MonthList.Month[Convert.ToInt32(creationDate.ToString("MM"))]}\\{creationDate.ToString("dd")}");
                    Directory.CreateDirectory(destinationSubDirectory);

                    // Формируем путь для сохранения файла
                    string destinationPath = Path.Combine(destinationSubDirectory, Path.GetFileName(imageFile));

                    



                    if (File.Exists(destinationPath))
                    {
                        try
                        {
                            File.Delete(destinationPath);
                            Console.WriteLine("Файл успешно удален.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка при удалении файла: {ex.Message}");
                        }
                    }
                    else
                    {
                        // Копируем файл в папку назначения
                        File.Copy(imageFile, destinationPath, true);

                        
                    }
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
