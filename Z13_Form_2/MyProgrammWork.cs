using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Z13_Form_2
{
    class MyFiles // Работа с файлами в общем
    {
        public static string FolderConfiguration { get; } = "Configuration";//Папка с конфигурациями
        public static string FileActiveConfigurations { get; } = "ActiveConfig.txt"; //Файл конфигураций активных (те что на рабочем столе)
        public static string FolderDesktopOFUser { get; } = $"\\Users\\{Environment.UserName}\\Desktop";//рабочий стол пользователя

        public static void Check() //Проверяет наличие файлов
        {
            if (!Directory.Exists(FolderConfiguration))
            {
                Directory.CreateDirectory(FolderConfiguration);
            }
            if (!File.Exists(FileActiveConfigurations))
            {
                File.WriteAllText(FileActiveConfigurations, "");
            }
        }
        public static string[] GetAllConfig()//Возвращает конфигурации в папке конфигураций
        {
            string[] Configs = Directory.GetDirectories(FolderConfiguration);
            for (int i = 0; i < Configs.Length; i++)
            {
                Configs[i] = Path.GetFileNameWithoutExtension(Configs[i]);
            }
            return Configs;
        }
    }

    class MyProgrammWork // Работа программы
    {
        public static List<string> ActiveConfig { get; set; } = File.ReadAllLines(MyFiles.FileActiveConfigurations).ToList<string>();

        public static void Add(string[] names)//Добавляет конфигурацию на рабочий стол
        {
            foreach (string ConfigName in names)
            {
                if (!CheckConfigActiv(ConfigName))
                {
                    string FolderOfThisConfig = $"{MyFiles.FolderConfiguration}\\{ConfigName}";
                    string[] ConfigsFile = Directory.GetFiles(FolderOfThisConfig);
                    ConfigsFile = ReturnClearFileName(ConfigsFile);

                    foreach (string FileName in ConfigsFile)
                    {
                        File.Copy($"{FolderOfThisConfig}\\{FileName}", $"{MyFiles.FolderDesktopOFUser}\\{FileName}");
                    }
                    ActiveConfig.Add(ConfigName);
                }
            }
            UpdateFileOfActiveConfigurations();
        }
        public static void Delete(string[] names)//Удаляет конфигурацию с рабочего стола
        {
            foreach (string ConfigName in names)
            {
                string[] ConfigsFile = Directory.GetFiles($"{MyFiles.FolderConfiguration}\\{ConfigName}");
                ConfigsFile = ReturnClearFileName(ConfigsFile);

                foreach (string FileName in ConfigsFile)
                {
                    try
                    {
                        File.Delete($"{MyFiles.FolderDesktopOFUser}\\{FileName}");
                    }
                    catch
                    {

                    }
                }
                ActiveConfig.Remove(ConfigName);
            }
            UpdateFileOfActiveConfigurations();
        }
        public static bool CheckConfigActiv(string name)
        {
            foreach (string i in MyProgrammWork.ActiveConfig)//Ищет выбранную конфигурацию в списке активных конфигураций
            {
                if (i == name)//если имя конфигурации совпадает с именем из списка
                {
                    return true;
                }
            }
            return false;
        }
        public static void UpdateFileOfActiveConfigurations()//Обновляет список активных конфигураций в файле (желательно сувать прям в функции)
        {
            File.WriteAllLines(MyFiles.FileActiveConfigurations, ActiveConfig);
        }
        static string[] ReturnClearFileName(string[] names)//Возвращает имя файла с его типом
        {
            string[] NewName = new string[names.Length];
            for (int i = 0; i < NewName.Length; i++)
            {
                NewName[i] = names[i].Substring(names[i].LastIndexOf('\\'));
            }
            return NewName;
        }

    }
}
