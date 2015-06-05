using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileDialog
{
    public partial class Form1 : Form
    {
        private string _surroundLeft = "";
        private string _surroundRight = "";
        private string _separation = "";
        private static bool _ignoreRepetition = true;
        private static string inputTemp1 = "";
        private static string inputTemp2 = "";

        private int lineCount;

        private static List<string> _list1 = new List<string>();
        private static List<string> _list2 = new List<string>();
        private static List<string> _mutual = new List<string>();



        public Form1()
        {
            InitializeComponent();
            
            repetition.Checked = true;
        }

        private void input1_TextChanged(object sender, EventArgs e)
        {
            inputTemp1 = input1.Text;
        }

        private void input2_TextChanged(object sender, EventArgs e)
        {
            inputTemp2 = input2.Text;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private static void CleanLists()
        {
            _list1 = new List<string>();
            _list2 = new List<string>();
            _mutual = new List<string>();
        }

        private void compareButton_Click(object sender, EventArgs e)
        {
            CleanLists();
             
            var str1 = inputTemp1.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
            var str2 = inputTemp2.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);

            var tempList1 = ConvertToList(str1);
            var tempList2 = ConvertToList(str2);

            label8.Text = tempList1.Count.ToString();
            label9.Text = tempList2.Count.ToString();

            CopyList(tempList1, _list1);
            CopyList(tempList2, _list2);

            FindMutual(tempList1, tempList2);
           
            
            
            OutputResults();

        }

        public void OutputResults()
        {
            textBox3.Text = CastToString(_mutual, lineCount);
            label7.Text = _mutual.Count.ToString();
            textBox4.Text = CastToString(_list1, lineCount);
            label5.Text = _list1.Count.ToString();
            textBox5.Text = CastToString(_list2, lineCount);
            label6.Text = _list2.Count.ToString();
        }

        private string CastToString(IEnumerable<string> list, int newLineCount)
        {
            var returnValue = "";
            int i = 1;
            foreach (var value in list)
            {

                returnValue += _surroundLeft + value + _surroundRight + _separation;
                if (newLineCount > 0 && i % newLineCount == 0)
                {
                    returnValue += "\r\n";
                }
                i++;
            }
            return returnValue;
        }

        private static List<string> ConvertToList(IEnumerable<string> arr)
        {
            return arr.ToList();
        }

        private static void CopyList(IEnumerable<string> fromList, ICollection<string> destination)
        {
            foreach (var temp in fromList)
            {
                destination.Add(temp);
            }
        }

        private static void FindMutual(IEnumerable<string> temp1, List<string> temp2)
        {
            var selectedItem1 = new int[_list1.Count];
            var selectedItem2 = new int[_list2.Count];
            var i = 0;
            foreach (var list1Temp in temp1)
            {
                var j = 0;
                foreach (var list2Temp in temp2)
                {
                    if (list1Temp.Equals(list2Temp, StringComparison.OrdinalIgnoreCase))
                    {
                        if ((selectedItem1[i] == 1 || selectedItem2[j] == 1) && _ignoreRepetition)
                        {
                            j++;
                            continue;
                        }
                        selectedItem1[i] = 1;
                        selectedItem2[j] = 1;
                        _mutual.Add(list2Temp);
                        _list1.Remove(list1Temp);
                        _list2.Remove(list2Temp);
                    }
                    j++;
                }
                i++;
            }
        }

        private void surroundLeft_TextChanged(object sender, EventArgs e)
        {
            _surroundLeft = textBox6.Text;
        }

        private void separation_TextChanged(object sender, EventArgs e)
        {
            _separation = textBox7.Text;
        }

        private void surroundRight_TextChanged(object sender, EventArgs e)
        {
            _surroundRight = textBox8.Text;
        }

        private void output1_TextChanged(object sender, EventArgs e)
        {

        }

        private void output2_TextChanged(object sender, EventArgs e)
        {

        }

        private void outputSame_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void newLineCount_TextChanged(object sender, EventArgs e)
        {
            var numVal = 0;
            try
            {
                numVal = Convert.ToInt32(newLineCount.Text);
            }
            catch (Exception)
            {
                numVal = 0;
            }
            lineCount = numVal;
        }

        private void saveFileDialog_Click(object sender, EventArgs e)
        {
            var saveFileDialog1 = new SaveFileDialog();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (var s = File.Open(saveFileDialog1.FileName, FileMode.Create))
                using (var sw = new StreamWriter(s))
                {
                    var outputFile = "========Mutual=========\r\n\r\n";
                    outputFile += CastToString(_mutual, lineCount);
                    outputFile += "\r\n\r\n========";
                    outputFile += ("Output1");
                    outputFile += ("========\r\n\r\n\r\n");
                    outputFile += (CastToString(_list1, lineCount));
                    outputFile += ("\r\n\r\n========");
                    outputFile += ("Output2");
                    outputFile += ("========\r\n\r\n\r\n");
                    outputFile += (CastToString(_list2, lineCount));

                    sw.Write(outputFile);
                }
            }
        }

        private void repetition_CheckedChanged(object sender, EventArgs e)
        {
            _ignoreRepetition = !_ignoreRepetition;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            input1.Text = "";
            input2.Text = "";
        }
    }
}
