using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    
    internal static class Program
    {
        public static String path = System.IO.Path.GetFullPath(@"..\..\");
        static void Main(string[] args)
        {
            
            
            //Settings
            int[] size = new int[] { 2,6};
            bool sound = false;
            
            //Variables
            size[0] = size[0] + 1; // adding size for the coordinates
            size[1] = size[1] + 1; // adding size for the coordinates


            //Arrays
            int[] a = new int[] { 0, 0 }; // 1 coordinate (x,y)
            int[] b = new int[] { 0, 0 }; // 2.coordinate (x,y)
            char[] symbols = new char[]
            {
                '☺', '☻', '♦', '♠', '♥', '♣', '♫', '☼', '©', '&', '▲', '►', '▼', '◄', '#', '§', '$', '£', '✿', '✌', '☁',
                '❈', '☂', '☎', '▣', '☃'
            };
            char[,] blancField = new char[size[0], size[1]];
            char[,] cardField = new char[size[0], size[1]];
            Random random = new Random();


            blancField = MakeCleanArray(size);
            cardField = MakeASide(size, symbols);


            ShowField(cardField, size);
            Console.WriteLine("-----------------------------");
            ShowField(blancField, size);

            while (!CheckIfFinish(blancField, size))
            {
                blancField = playRound(blancField, cardField, size, sound);
            }


            /*
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
            */


            /////////////////!!!!!!You shall not pass!!!!!////////////////
            ThisIsTheEnd(sound);
            /////////////////!!!!!!You shall not pass!!!!!////////////////
        }


        static void ThisIsTheEnd(bool sound)
        {
            Jukebox(sound);

            Console.ReadLine();
        }

        private static void Jukebox(bool sound)
        {

            //Songs
            SoundPlayer Skyfall = new SoundPlayer(@path + "sounds\\Skyfall.wav");
            SoundPlayer InTheEnd = new SoundPlayer(@path + "sounds\\InTheEnd.wav");
            SoundPlayer GoodThingsComeToAnEnd = new SoundPlayer(@path + "sounds\\GoodThingsComeToAnEnd.wav");


            Random Song = new Random();
            switch (Song.Next(1, 4))
            {
                case 1:
                     if(sound){Skyfall.Play();}
                    Console.WriteLine(
                        "This is the end...\nHold your breath and count to ten.\nFeel the Earth move and then.\nHear my heart burst again.\nFor this is the end.\nI've drowned and dreamt this moment.\nSo overdue, I owe them.\nSwept away, I'm stolen.\n");
                    break;
                case 2:
                    if (sound)
                    {
                        InTheEnd.Play();
                    }

                    Console.WriteLine(
                        "I tried so hard and got so far\nBut in the end, it doesn't even matter\nI had to fall to lose it all\nBut in the end, it doesn't even matter");
                    break;
                case 3:
                if (sound)
                {
                    GoodThingsComeToAnEnd.Play();
                }

                Console.WriteLine(
                        "Flames to dust, lovers to friends\nWhy do all good things come to an end?\nFlames to dust, lovers to friends\nWhy do all good things come to an end?");
                    break;
            }
        }

        /////////////////////////////
        static void ShowField(char[,] field, int[] size)
        {
            for (int i = 0; i < size[0]; i++)
            {
                for (int j = 0; j < size[1] + 1; j++)
                {
                    if (j == size[1])
                    {
                        Console.Write($"{((char)(i + 48)),4}");
                    }
                    else
                    {
                        Console.Write($"{field[i, j],4}");
                    }
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
                for (int j = 0; j < size[1]; j++)
                {
                    if (i == 0)
                    {
                        aArray[i, j] = ((char)(j + 48));
                    }

                    else if (j == 0)
                    {
                        aArray[i, j] = ((char)(i + 48));
                    }
                    else
                    {
                        aArray[i, j] = '■';
                    }
                }
            }

            return aArray;
        }


        private static char[,] MakeASide(int[] size, char[] symbols)
        {
            var aArray = new char[size[0], size[1]];
            aArray = MakeCleanArray(size);
            var random = new Random();
            for (var i = 0; i < (size[0] - 1) * (size[1] - 1) / 2; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    var placed = false;
                    while (!placed)
                    {
                        var a = random.Next(size[0] - 1); // x coordinate new object
                        var b = random.Next(size[1] - 1); // y coordinate new object

                        if (aArray[a + 1, b + 1] == '■')
                        {
                            aArray[a + 1, b + 1] = symbols[i];
                            placed = true;
                        }
                    }
                }
            }

            return aArray;
        }



        static int[] InputCardLocation(char[,] blancField, int[] size)
        {
            int[] iArray;
            bool number;

            do
            {
                number = true;
                char[] cArray = Console.ReadLine().ToCharArray();
                
                iArray = new int[cArray.Length];
                Console.WriteLine();
                iArray[0] = cArray[0] - 48;
                iArray[1] = cArray[1] - 48;
                if ((iArray[0] < 0) || (iArray[0] > size[0] - 1) || (iArray[1] < 0) || (iArray[1] > size[1] - 1))
                {
                    number = false;
                    Console.WriteLine("This is not a number or it is outside the filed");
                    Console.Write("Input new coordinates");
                }
                else if ((blancField[iArray[0],iArray[1]]) != '■')
                {
                    number = false;
                    Console.WriteLine("This card has been turned before");
                    Console.Write("Input new coordinates:");
                }
               
            } while (!number);

            return iArray;
        }

        public static char[,] playRound(char[,] blancField, char[,] cardField, int[] size,bool sound)
        {
            int[] cardA = new int[] { 0, 0 };
            int[] cardB = new int[] { 0, 0 };
            
            
            Console.Write("please enter Coordinates (11,23) for the first card: ");
            cardA = InputCardLocation(blancField,size);
            blancField[cardA[0], cardA[1]] = cardField[cardA[0], cardA[1]];
            ShowField(blancField, size);
            do
            {
                if ((cardA[0] == cardB[0])&&(cardA[1] == cardB[1]))
                {
                    Console.WriteLine("This is the same Card, you have to have 2 cards, change ");
                }
                Console.Write("please enter Coordinates (11,23) for the second card: ");
                cardB = InputCardLocation(blancField, size);
            } while ((cardA[0] == cardB[0])&&(cardA[1] == cardB[1]));
            blancField[cardB[0], cardB[1]] = cardField[cardB[0], cardB[1]];
            ShowField(blancField, size);
            Console.WriteLine("-----------");
            if (!((cardField[cardA[0], cardA[1]]) == (cardField[cardB[0], cardB[1]])))
            {
                blancField[cardA[0], cardA[1]] = '■';
                blancField[cardB[0], cardB[1]] = '■';
                Console.WriteLine("Try again");
                SoundPlayer Duck = new SoundPlayer(@path + "sounds\\WhatTheDuck.wav");
                if (sound)
                {
                    Duck.Play();
                }
            }
            else
            {
                SoundPlayer Yeah = new SoundPlayer(@path + "sounds\\Yeah.wav");
                if (sound)
                {
                    Yeah.Play();
                }

                Console.WriteLine("You got one! ENTER the next round");
                Console.ReadLine();

            }
            ShowField(blancField, size);
            
            return blancField;
        }
        public static bool CheckIfFinish(char[,] blancField, int[] size)
        {
            bool finish = true;
            for (int i = 0; i < size[0]; i++)
            {
                for (int j = 0; j < size[1]; j++)
                {
                    if (blancField[i, j] == '■')
                    {
                        finish = false;
                    }
                }
            }

            return finish;
        }
    }
}
