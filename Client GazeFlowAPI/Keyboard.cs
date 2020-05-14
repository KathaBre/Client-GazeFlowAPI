using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using GazeFlowAPI;
using System.Speech.Synthesis;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Devices;
//using System.Runtime.InteropServices;

namespace Client_GazeFlowAPI
{
    public partial class Keyboard : Form
    {
        //[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        ////Mouse actions
        //private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        //private const int MOUSEEVENTF_LEFTUP = 0x04;
        //private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        //private const int MOUSEEVENTF_RIGHTUP = 0x10;

        public Keyboard()
        {
            //starte GazePointer
            //Process.Start("C:\\Program Files (x86)\\GazePointer\\GazePointer\\GazePointer"); 
            InitializeComponent();
        }
               
        //public void button1_Click(object sender, EventArgs e)
        //{
        //    //Process.Start("C:\\Program Files (x86)\\GazePointer\\GazePointer\\GazePointer");
        //}

        //public void DoMouseClick()
        //{
        //    //Call the imported function with the cursor's current position
        //    uint X = (uint)Cursor.Position.X;
        //    uint Y = (uint)Cursor.Position.Y;
        //    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        //}

        //Globaler Zähler für "Klick-Kriterium" -> wird resetet wenn lastchar wechselt
        int count = 0;

        /*Liste besser als Arr -> dynamisch*/
        List<char> char_list = new List<char>();

        //Für Zähler -> wechselt wenn andere Taste angeschaut wird
        char lastchar;

        public void IsPressed(CGazeData GazeData, int width_letter, int height_letter, int Height, int Width, SpeechSynthesizer speaker)
        {
            //Taste A
            if (GazeData.GazeX <= width_letter && GazeData.GazeY <= height_letter)
            {
                if (lastchar != 'A')
                {
                    count = 0;
                }
                count++;
                lastchar = 'A';
                if (count == 40)
                {
                    char_list.Add('A');
                    //Console.Write("A");
                    myText.Text += "A";
                    myText.Refresh();
                }
            }
            //Taste B
            if (GazeData.GazeX > width_letter && GazeData.GazeX <= (2 * width_letter) && GazeData.GazeY <= height_letter)
            {
                if (lastchar != 'B')
                {
                    count = 0;
                }
                count++;
                lastchar = 'B';
                if (count == 40)
                {
                    char_list.Add('B');
                    //Console.Write("B");
                    myText.Text += "B";
                    myText.Refresh();
                }

            }

            //Taste F
            if (GazeData.GazeX > (5 * width_letter) && GazeData.GazeX <= Width && GazeData.GazeY <= height_letter)
            {
                if (lastchar != 'F')
                {
                    count = 0;
                }
                count++;
                lastchar = 'F';
                if (count == 40)
                {
                    char_list.Add('F');
                    //Console.Write("F");
                    myText.Text += "F";
                    myText.Refresh();
                }
            }

            //Taste H
            if (GazeData.GazeX > width_letter && GazeData.GazeX <= (2 * width_letter) && GazeData.GazeY > height_letter && GazeData.GazeY <= (2 * height_letter))
            {
                if (lastchar != 'H')
                {
                    count = 0;
                }
                count++;
                lastchar = 'H';
                if (count == 40)
                {
                    char_list.Add('H');
                    //Console.Write("H");
                    myText.Text += "H";
                    myText.Refresh();
                }

            }

            //ENTER-Taste
            if (GazeData.GazeX > (5 * width_letter) && GazeData.GazeX <= Width && GazeData.GazeY > (4 * height_letter) && GazeData.GazeY <= Height)
            {
                if (lastchar != '#')
                {
                    count = 0;
                }
                count++;
                lastchar = '#';
                if (count == 40)
                {
                    string str = new string(char_list.ToArray());
                    speaker.SpeakAsync(str);
                }
            }
        }

        private void Backspace_Click(object sender, EventArgs e)
        {
            try
            {
                if (myText.Text == " ")
                {
                    myText.Text = Strings.Mid(myText.Text, 1, Strings.Len(myText.Text) - 1 + 1);
                }
                else
                {
                    myText.Text = Strings.Mid(myText.Text, 1, Strings.Len(myText.Text) - 1);
                }
            }
            catch (Exception) { }
        }
    }
}
