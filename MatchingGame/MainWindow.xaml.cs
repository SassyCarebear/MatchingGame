using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


/*
 * Program:     Matching Game  
 * Developer:   Cara Jones  
 * Date:        04/20/2022
 */

namespace MatchingGame
{
    using System.Windows.Threading;
    using System.Windows.Resources;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Set up Timer
        /// </summary>
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;


        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            
            SetUpGame();
        }// end of MainWindow

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = "       " + timeTextBlock.Text + " - \r\nPlay again?";
                
            }// end of if that stops timer and asks to play again

        }// end of Timer_Tick


        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()       //initialize list
            {

                "🦩","🦩",
                "🦕","🦕",
                "🐧","🐧",
                "🐘","🐘",
                "🦀","🦀",
                "🐇","🐇",
                "🐖","🐖",
                "🐐","🐐",
                
            };

            currTime.Content = DateTime.Now.ToString("hh:mm:ss");       //sets current time in corner
            Random random = new Random();
                    
            foreach (TextBlock textblock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textblock.Name != "timeTextBlock")
                {

                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textblock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                    textblock.Visibility = Visibility.Visible;
                }//end of if block for game set-up

            }// end of foreach statement that sets up game

            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }// end of SetUpGame method
        TextBlock lastTextBlockClicked;
        bool findingMatch = false;


        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            TextBlock textblock = sender as TextBlock;
            if (findingMatch == false)
            {
                textblock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textblock;
                findingMatch = true;
            }// end of if
            else if (textblock.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                textblock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }//end of else if 
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }// end of else (false)
        }

        private void mainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();            //restarts the game
            }
        }// end of timeTextBlock


        private void currTime_TextInput(object sender, TextCompositionEventArgs e)
        {
        }
    }// end of class
}// end of solution
