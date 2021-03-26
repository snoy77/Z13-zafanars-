using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

using System.Diagnostics;

//Проблемы:
//№001 - Заря должна находится только на уровне папки пользователя. Временное решение - Отключить функцию как самостоятельную. Включить только при
//                                                                                                      существовании файла пути к рабочему столу.

//~1. Продумать интерфейс программы
//~1.1 Основные кнопки: Замена, Удаление, Добавить, Чистый
//~1.2 Расмотреть вариант с CheckListBox за место кнопок. Оставить кнопку "Поставить" и "Чистый"
//~2. Написать функцию проверки наличия файлов: "заря-13 Систем" "Заря-13 Систем\\Конфигурации" "Заря-13 Систем\\Сейчас конфигурации.ткст"
//~2.1 Написат функцию создания Системных файлов
//~3. Написать функцию, возращающую путь к рабочему столу. (Возможно стоит добавить файл системы, где будет хранится путь и проверяться при открытии программы)
//4. Написать функцию, добавляющую в лист папки из конфигураций
//~5. написать функцию, Добавляющую ярлыки на рабочий стол
//~6. Написать функцию удаления ярлыков определённой конфигурации с рабочего стола, смотря на список ярлыков из папки конфигурации
//7. Добавление и чтение файла списка активных конфигураций
//8. Добавить интерфейс.

//Дополнительно:
//1. Подумать о замене языка интерфейса.
//2. Подумать о разном интерфейсе программы.

namespace Z13_F2
{
    

    public partial class Form1 : Form
    {
        static int indexofInterface;
        void UpdateList()
        {
            string[] AllFolder = Directory.GetDirectories(NameSystemsFile.ConfigurationsFolder);
            for (int i = 0; i < AllFolder.Length; i++)
            {
                AllFolder[i] = Path.GetFileNameWithoutExtension(AllFolder[i]);
            }
            switch (indexofInterface)
            {
                case 1:
                    {
                        listBox1.Items.Clear();
                        listBox1.Items.AddRange(AllFolder);
                    }
                    break;
                default:

                    break;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            SystemsFile.CheckFiles();
            indexofInterface = int.Parse(File.ReadAllText(NameSystemsFile.DataOfInterfaceFile));
            UpdateList();

            label1.Text = "Версия: " + ProgrammsInformation.V;
        }

        private void Put_BUTTON_Click(object sender, EventArgs e)
        {
            Configuration.Put(listBox1.SelectedItem.ToString());
        }
        private void Delete_BUTTON_Click(object sender, EventArgs e)
        {
            Configuration.Delete(listBox1.SelectedItem.ToString());
        }
        private void Replace_BUTTON_Click(object sender, EventArgs e)
        {
            Configuration.Replace(listBox1.SelectedItem.ToString());
        }
        private void Clear_BUTTON_Click(object sender, EventArgs e)
        {
            Configuration.Clear();
        }
    }

    public class ProgrammsInformation
    {
        public static string V = "Form-2 06.07.19";
    }
    public class NameSystemsFile//Класс - хранитель имён файлов
    {
        public static string SystemsFolder = "Z13 System";
        public static string ConfigurationsFolder = SystemsFolder + "\\ConfigDesktop";
        public static string NowConfigFile = SystemsFolder + "\\NowConfigurations.txt";
        public static string DesktopPathFile = SystemsFolder + "\\DesktopPath.txt";
        public static string DataOfInterfaceFile = SystemsFolder + "\\InterfaceData.txt";
        public static string ErrorFile = SystemsFolder + "\\ErrorFile.txt";
    }
    public class SystemsFile : NameSystemsFile //Класс, отвечающий за действия над системными файлами
    {
        public static void CheckFiles()//Проверка файлов системы на существование, в противном случае их создание
        {
            if (!Directory.Exists(SystemsFolder))
            {
                Directory.CreateDirectory(SystemsFolder);
            }
            if (!Directory.Exists(ConfigurationsFolder))
            {
                Directory.CreateDirectory(ConfigurationsFolder);
            }
            if (!File.Exists(NowConfigFile))
            {
                File.WriteAllText(NowConfigFile, "");
            }
            if (!File.Exists(DesktopPathFile))
            {
                File.WriteAllText(DesktopPathFile, "");
                File.WriteAllText(DesktopPathFile, GetDesktopPath());
            }
            if (!File.Exists(ErrorFile))
            {
                File.WriteAllText(ErrorFile, "");
            }
            if (!File.Exists(DataOfInterfaceFile))
            {
                File.WriteAllText(DataOfInterfaceFile, "1");
            }
        }
        public static string GetDesktopPath()//Возвращает путь к рабочему столу. Проблема №001
        {
            string PathOfProgramm = Application.ExecutablePath;
            string Result = "";

            int CountSymbol = 0;
            for (int i = 0; i < PathOfProgramm.Length; i++)
            {
                //Цикл проходится по всему пути к программе, однако подсчитывает количество переходов к каталогу.
                //При определённом количестве прохождений, он останавливается на каталоге активного пользователя, и добавляется к пути название папки рабочего стола.
                //Недостаток то, что скорее всего программа не будет работать, если поместить её в файлы, не входящие в файлы пользователя, например ProgrammFiles.
                if (PathOfProgramm[i] == '\\')
                {
                    CountSymbol++;
                }
                if (CountSymbol < 3)
                {
                    Result += PathOfProgramm[i];
                }
                else
                {
                    break;
                }
            }
            return Result + "\\Desktop";
        }
    }
    public class Configuration : NameSystemsFile//Класс, отвечающий за действия над конфигурациями
    {
        protected static void Massa(int index, string NameOfConfiguration)//Так как в основном код "поставит ьконфиг" и "удалить конфиг" совершенно одинаковый,
                                                                          //было решено написать отдельную функцию, к которой будут обращаться 
                                                                          //другие две по индексу.
        {
            string FolderOfConfigurathion = ConfigurationsFolder + "\\" + NameOfConfiguration;//Построение имени папки конфигурации
            if (Directory.Exists(FolderOfConfigurathion))
            {
                string[] FilesConfiguraton = Directory.GetFiles(FolderOfConfigurathion);//Получение файлов в папке конфигурации
                string DesktopPath = File.ReadAllText(DesktopPathFile);//Получение рабочего стола
                foreach (string NameFile in FilesConfiguraton)
                {
                    //Неясно как будет лучше: или чтобы оператор свитч постоянно работал при новом витке цикла, 
                    //или два цикла на один оператор свитча
                    switch (index)
                    {
                        case 1://Для отправки ярлыков на рабочий стол
                            {
                                if (NameFile.Substring(NameFile.LastIndexOf('.')) == ".lnk")
                                {
                                    try
                                    {
                                        File.Copy(NameFile, DesktopPath + "\\" + NameFile.Substring(NameFile.LastIndexOf('\\') + 1));
                                    }
                                    catch
                                    {
                                        File.AppendAllText(ErrorFile, "Ошибка добавления файла на рабочий стол");
                                    }
                                }
                            }
                            break;
                        case 2://Для удаления файлов с рабочего стола
                            {
                                File.Delete(DesktopPath + "\\" + NameFile.Substring(NameFile.LastIndexOf('\\') + 1));
                            }
                            break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Папки конфигурации не существует");
            }
        }
        public static void Put(string NameOfConfiguration)//Ставит конфигурацию на рабочий стол
        {
            if (!CheckRecord(NameOfConfiguration))
            {
                AddRecord(NameOfConfiguration);
                Massa(1, NameOfConfiguration);
            }
            else
            {
                MessageBox.Show("Конфигурация уже активна");
            }
        }
        public static void Delete(string NameOfConfiguration)//Удаляет конфигурацию с рабочего стола
        {
            if (CheckRecord(NameOfConfiguration))
            {
                Massa(2, NameOfConfiguration);
                DeleteRecord(NameOfConfiguration);
            }
            else
            {
                MessageBox.Show("Конфигурация не активна");
            }
        }
        public static void Replace(string NameOfConfiguration)//Заменяет все конфигурации на выбранную конфигурацию
        {
            bool Was = false;
            foreach (string Configuration in File.ReadAllLines(NowConfigFile))
            {
                Delete(Configuration);
            }
            if (!Was)
            {
                Put(NameOfConfiguration);
            }
        }
        public static void Clear()
        {
            foreach (string Configuration in File.ReadAllLines(NowConfigFile))
            {
                Delete(Configuration);
            }
        }

        protected static bool CheckRecord(string NameOfConfiguration)
        {
            bool RecordExists = false;
            foreach (string record in File.ReadLines(NowConfigFile))
            {
                if (record == NameOfConfiguration)
                {
                    RecordExists = true;
                    break;
                }
            }
            return RecordExists;
        }
        protected static void AddRecord(string NameOfConfiguration)
        {
            if (Directory.Exists(ConfigurationsFolder + "\\" + NameOfConfiguration))
            {
                File.AppendAllText(NowConfigFile, NameOfConfiguration + "\r\n");
            }
        }
        protected static void DeleteRecord(string NameofConfiguration)
        {
            List<string> NewListInFile = new List<string>();

            foreach (string Record in File.ReadAllLines(NowConfigFile))
            {
                if (!(Record == NameofConfiguration))
                {
                    NewListInFile.Add(Record);
                }
            }
            File.WriteAllLines(NowConfigFile, NewListInFile);
        }
    }
}