using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

namespace Z13_Form_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MyFiles.Check();
        }
        void UpdateList()
        {
            listBox1.SelectionMode = SelectionMode.MultiExtended;
            listBox1.Items.Clear();

            string[] ConfigsList = MyFiles.GetAllConfig();
            if( ConfigsList.Length != 0)
            {
                listBox1.Items.AddRange(ConfigsList);
            }
            else
            {
                listBox1.Items.Add("Нет конфигураций");
                listBox1.SelectionMode = SelectionMode.None;
            }
            listBox2.Items.Clear();
            listBox2.Items.AddRange(MyProgrammWork.ActiveConfig.ToArray());

        }
        string[] ReturnSelectedItemHowArray()
        {
            string[] SelectedItems = new string[listBox1.SelectedItems.Count];
            for (int i = 0; i < SelectedItems.Length; i++)
            {
                SelectedItems[i] = listBox1.SelectedItems[i].ToString();
            }
            return SelectedItems;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UpdateList();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            MyProgrammWork.Add(ReturnSelectedItemHowArray());
            UpdateList();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            MyProgrammWork.Delete(ReturnSelectedItemHowArray());
            UpdateList();
        }
        private void aa()
        {
            bool ConfigIsActive = false;//Конфигурация не активна (до поисков её в списке)
            foreach (string i in MyProgrammWork.ActiveConfig)//Ищет выбранную конфигурацию в списке активных конфигураций
            {
                if (i == listBox1.SelectedItem.ToString())//если имя конфигурации совпадает с именем из списка
                {
                    MyProgrammWork.ActiveConfig.Remove(i);//Удаление конфигурации из списка активных конфигураций для удаления лишних
                    ConfigIsActive = true;//Конфигурация активна - подтверждение
                    break;//Выход из цикла
                }
            }
            MyProgrammWork.Delete(MyProgrammWork.ActiveConfig.ToArray());//Удаление всех остальных конфигураций    
            if (!ConfigIsActive)
            {
                MyProgrammWork.Add(new string[1] { listBox1.SelectedItem.ToString() });//Добавление конфигурации на рабочий стол, если была не активна
            }
            else
            {
                MyProgrammWork.ActiveConfig.Add(listBox1.SelectedItem.ToString());//Добавление назад активной конфигурации в лист, если была активна изначально
                MyProgrammWork.UpdateFileOfActiveConfigurations();//Обновляем файл активных конфигураций
            }
            UpdateList();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //ПРОЧТИ В БУДУЩЕМ, СТАРИК!!!
            //Мне здесь немного не нравится как алгоритм обходится с уже имеющейся конфигурацией. Все эти
            //манёвры какие-то тупые. Я бы хотел, чтобы в цикле прям всё точно определилось, а так без этого прихоится 
            //за ней подтирать в случае, если конфигурация уже активна. Это тупо
            aa();
            
        }
        private void button5_Click(object sender, EventArgs e)
        {
            MyProgrammWork.Delete(MyProgrammWork.ActiveConfig.ToArray());
            UpdateList();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            MyProgrammWork.UpdateFileOfActiveConfigurations();
            UpdateList();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Process.Start(MyFiles.FolderConfiguration);
            UpdateList();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            aa();
        }
    }
}
