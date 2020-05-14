using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GazeFlowAPI;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Speech.Synthesis;
using System.Windows.Input;

namespace Client_GazeFlowAPI
{
    
    class Program
    {
       
        static void Main()
        {
            /*Erstellung und Öffnen des GUIs Keyboard*/
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Keyboard keyboard = new Keyboard();

            int Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            int Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            int width_letter = Width / 6;
            int height_letter = Height / 5;

            /*Bei 1366 x 768: 
             * width_letter = 227,667
             * height_letter = 153,6
             * 
             * Bereiche der einzelnen letters:
             * A    x: [0 - width_letter]                       y: [0 - height_letter]
             * B    x: [width_letter - (2*width_letter)         y: bleibt gleich
             * C    x: [(2*width_letter) - (3*width_letter)]    y: bleibt gleich
             * usw.
             */          
           

            //Sprachausgabe
            SpeechSynthesizer speaker = new SpeechSynthesizer();

            //In dem Fall unnötig, aber falls zB vorher OutputToWav eingestellt war
            speaker.SetOutputToDefaultAudioDevice();
            //Geschwindigkeit (-10 - 10)
            speaker.Rate = 1;
            //Lautstärke (0-100)
            speaker.Volume = 100;
            // Stimme
            speaker.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Adult);
               
            /*Liste besser als Arr -> dynamisch*/
            List<char> char_list = new List<char>();         

            CGazeFlowAPI gazeFlowAPI = new CGazeFlowAPI();

            //Keypress keypress = new Keypress();
            
            string AppKey = "AppKeyDemo";

            if (gazeFlowAPI.Connect("127.0.0.1", 43333, AppKey))
            {
                
                while (true)
                {
                    keyboard.Show();
                    keyboard.Focus();
                    CGazeData GazeData = gazeFlowAPI.ReciveGazeDataSyn();
                    if (GazeData == null)
                    {
                        Console.WriteLine("Disconected");
                        return;
                    }
                    else
                    {       
                        //Methode ausgelagert
                        keyboard.IsPressed(GazeData, width_letter, height_letter, Height, Width, speaker);
                    }
                }           
            }
            else
                Console.WriteLine("Connection fail");

            Console.Read();
        }
    }
}
