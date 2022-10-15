using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            //Variables
            int[] size = new int[] { 6, 6 };


            //Arrays
            int [] a = new int []{0,0};
            int [] b = new int []{0,0};
            char[] aCard = new char[] { '☺', '☻', '♦', '♠', '♥', '♣', '♫', '☼', '©', '&', '▲', '►', '▼', '◄', '#', '§', '$', '£' };
            char[,] blancField = new char[size[0],size[1]];
            char[,] cardField = new char[size[0],size[1]];
            Random random = new Random();
            
            
           
                    
            blancField = MakeCleanArray(size);
            cardField = MakeCleanArray(size);
            cardField = MakeASide(size, aCard);
            
           
            ShowField(cardField,size);
            Console.WriteLine("-----------------------------");

            for (int i = 0; i < 2; i++)
            {
                ShowField(blancField, size);


                a = InputCardLocation(size);
                b = InputCardLocation(size);

                if (cardField[a[0], a[1]] == cardField[b[0], b[1]])
                {
                    Console.WriteLine("Got one");
                    blancField[a[0], a[1]] = cardField[a[0], a[1]];
                    blancField[b[0], b[1]] = cardField[b[0], b[1]];
                }
                else
                {
                    Console.WriteLine("try again");
                }
            }










            /////////////////!!!!!!You shall not pass!!!!!////////////////
            ThisIsTheEnd();
            /////////////////!!!!!!You shall not pass!!!!!////////////////
        }

        static void ThisIsTheEnd()
        {
            Jukebox();

            Console.ReadLine();
        }
        private static void Jukebox()
        {
            //Songs
            SoundPlayer Skyfall = new SoundPlayer(@"C:\Users\Janick\Documents\Gibb\iet-403\Programme\Juckbox\Sounds\Skyfall.wav");
            SoundPlayer InTheEnd = new SoundPlayer(@"C:\Users\Janick\Documents\Gibb\iet-403\Programme\Juckbox\Sounds\InTheEnd.wav");
            SoundPlayer GoodThingsComeToAnEnd = new SoundPlayer(@"C:\Users\Janick\Documents\Gibb\iet-403\Programme\Juckbox\Sounds\GoodThingsComeToAnEnd.wav");


            Random Song = new Random();
            switch (Song.Next(1,4))
            {
                case 1:
                    Skyfall.Play();
                    Console.WriteLine("This is the end...\nHold your breath and count to ten.\nFeel the Earth move and then.\nHear my heart burst again.\nFor this is the end.\nI've drowned and dreamt this moment.\nSo overdue, I owe them.\nSwept away, I'm stolen.\n");
                    break;
                case 2: 
                    InTheEnd.Play();
                    Console.WriteLine("I tried so hard and got so far\nBut in the end, it doesn't even matter\nI had to fall to lose it all\nBut in the end, it doesn't even matter");
                    break;
                case 3:
                    GoodThingsComeToAnEnd.Play();
                    Console.WriteLine("Flames to dust, lovers to friends\nWhy do all good things come to an end?\nFlames to dust, lovers to friends\nWhy do all good things come to an end?");
                    break;    
                
            }
           
        }
        
        /// /////////////////////////
        static void ShowField(char[,] field,int[] size)
        {
            
            for (int i = 0; i < size[0]; i++)
            {
                for(int j = 0; j < size[1]; j++)
                {
                    Console.Write($"{field[i,j],4}");
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        static char[,] MakeCleanArray(int[] size)
        {
            char[,] aArray = new char[size[0], size[1]];
            for (int i = 0; i < size[0]; i++)
            {
                for(int j = 0; j < size[1]; j++)
                {
                    aArray[i, j] = '■';
                }
            }

            return aArray;
        }

        /*static char[,] MakeASide(int[] size, char[] symboles)
        {
            char[,] aArray = new char[size[0], size[1]];
            Random random = new Random();
            for (int i = 0; i < size[0]; i++)
            {
                for (int j = 0; j < size[1]; j++)
                {
                    aArray[i, j] = symboles[random.Next(18)];
                }
            }

            return aArray;
        }*/
        static char[,] MakeASide(int[] size, char[] aCard)
        {
            char[,] aArray = new char[size[0],size[1]];
            aArray = MakeCleanArray(size);
            Random random = new Random();
            for (int i = 0; i < aCard.Length; i++)
            {
                int a = random.Next(size[0]);
                int b = random.Next(size[1]);
                if (aArray[a, b] == '■')
                {
                    aArray[a, b] = aCard[i];
                }
                int c = random.Next(size[0]);
                int d = random.Next(size[1]);
                if (aArray[c, d] == '■')
                {
                    aArray[a, b] = aCard[i];
                }
            }
            return aArray;
        }
        
        static int [] InputCardLocation(int[] size)
        {
            int[] iArray;
            bool number;
            
            do
            {
                number = true;
                Console.Write("Please enter a card location:\t");
                char[] cArray = Console.ReadLine().ToCharArray();
                iArray = new int[cArray.Length];
                Console.WriteLine();
                iArray[0] =  cArray[0] - 48;
                iArray[1] =  cArray[1] - 48;
                if ((iArray[0]< 0) || (iArray[0] > size[0]-1) || (iArray[1] < 0) || (iArray[1] > size[1]-1))
                {
                    number = false;
                    Console.WriteLine("This is not a number or it is outside the filed");
                }
                
            } while (!number);

            return iArray;
        }
        
        
    }
}
