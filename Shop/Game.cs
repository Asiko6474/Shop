using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Shop
{
    struct item
    {
        public string name;
        public int cost;
    }
    class Game
    {
        int playerIndex = -1;
        private Player _player;
        private Shop _shop;
        private bool _gameOver;
        private int _currentScene;
        private item[] _shopStock;

        private item _blueBerry;
        private item _animeFigure;
        private item _panacea;

        private void Start()
        {
            _gameOver = false;
            _player = new Player();
            InitializeItems();
            _shopStock = new item[] { _blueBerry, _animeFigure, _panacea };
            _shop = new Shop(_shopStock);
        }

        int GetInput(string description, params string[] options)
        {
            string input = "";
            int inputReceived = -1;

            while (inputReceived == -1)
            {

                Console.WriteLine(description);
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine((i + 1) + ". " + options[i]);
                }
                Console.Write("> ");

                input = Console.ReadLine();

                if (int.TryParse(input, out inputReceived))
                {
                    inputReceived--;
                    if (inputReceived < 0 || inputReceived >= options.Length)
                    {
                        inputReceived = -1;
                        Console.WriteLine("Invalid Input");
                        Console.ReadKey(true);
                    }
                }
                else
                {
                    inputReceived = -1;
                    Console.WriteLine("Invalid Input");
                    Console.ReadKey(true);
                }

                Console.Clear();
            }

            return inputReceived;
        }

        private void Update()
        {
            DisplayCurrentScene();
        }

        private void End()
        {

        }
        //creates the item properties
        private void InitializeItems()
        {
            //make the Blue Berry
            _blueBerry.name = "Blue Berry ";
            _blueBerry.cost = 1000;
            //Make the anime figure
            _animeFigure.name = "An Anime Figure ";
            _animeFigure.cost = 2000;
            //make the literal panacea
            _panacea.name = "The literal Panacea ";
            _panacea.cost = 1;
        }

        void Save()
        {
            StreamWriter writer = new StreamWriter("SaveData.txt");

            _player.Save(writer);
            writer.WriteLine(playerIndex);
            writer.Close();
        }

        public bool Load()
        {
            bool loadSucessful = true;
            
            if(!File.Exists("SaveData.txt"))
                loadSucessful = false;

            StreamReader reader = new StreamReader("SaveData.txt");

            reader.Close();

            return loadSucessful;
        }

        void DisplayCurrentScene()
        {
            switch (_currentScene)
            {
                case 0:
                    Start();
                    DisplayOpeningMenu();
                    break;
                case 1:
                    DisplayShopMenu();
                    break;
                case 2:
                    End();
                    break;
            }
        }

        void DisplayOpeningMenu()
        {
            Console.WriteLine("Heyoooo~! Welcome to Nakano's shop of stuff!");

            int choice = GetInput("So are you a new customer or a returning one?", "New Customer (New Game)", "Returning one (Load Game)");

            if (choice == 0)
            {
                _currentScene++;
            }
            if (choice == 1)
            {
                if (Load())
                {
                    Console.WriteLine("Oh welcome back! (Load Successful)");
                    Console.ReadKey(true);
                    Console.Clear();
                    _currentScene++;
                }
                else
                {
                    Console.WriteLine("Are you sure? I don't remember you. (Load failed)");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
        }

        public void GetShopMenuOptions(item[] inventory)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + inventory[i].name + inventory[i].cost + "G");
            }
            
        }

        void DisplayShopMenu()
        {
            Console.WriteLine("you have " + _player.Gold() + "G left.");
            InitializeItems();
            Console.WriteLine("What are you buying?");
            GetShopMenuOptions(_shopStock);
            Console.WriteLine("4. I rather save.");
            Console.WriteLine("5. I rather leave.");
            char input = Console.ReadKey(true).KeyChar;
            //Set out what item to buy
            int itemIndex = -1;
            switch (input)
            {
                case '1':
                    itemIndex = 0;
                    Console.WriteLine("That is one Blue Berry! I wonder why I sell only 1 single berry.");
                    Console.ReadKey(true);
                    Console.Clear();
                    break;
                case '2':
                    itemIndex = 1;
                    Console.WriteLine("Ah yes the Anime Figure, fun fact, it is the most expensive on the market, quite beautiful too!");
                    Console.WriteLine("Too bad this is a text based game and you can't actually see it.");
                    Console.ReadKey(true);
                    Console.Clear();
                    break;
                case '3':
                    itemIndex = 2;
                    Console.WriteLine("That is the Panacea");
                    Console.ReadKey(true);
                    Console.Clear();
                    break;
                case '4':
                    Save();
                    Console.WriteLine("Alright then! you saved!");
                    Console.ReadKey(true);
                    Console.Clear();
                    return;
                case '5':
                    Console.WriteLine("Alright then, see you again next time!");
                    Console.ReadKey(true);
                    Console.Clear();
                    _gameOver = true;
                    _currentScene++;
                    return;
                default:
                        Console.Clear();
                        return;
            }
            if (_player.Gold() < _shopStock[itemIndex].cost)
            {
                Console.WriteLine("You can't afford this.");
                return;
            }


            // Pick which slot to put the purchased item in.
            Console.WriteLine("Where are you going to put it?");
            GetShopMenuOptions(_player.GetInventory());
            input = Console.ReadKey().KeyChar;


            switch (input)
            {
                case '1':
                    playerIndex = 0;
                    break;
                case '2':
                    playerIndex = 1;
                    break;
                case '3':
                    playerIndex = 2;
                    break;
                default:
                    return;
            }

            Console.Clear();
            _shop.Sell(_player, itemIndex, playerIndex);

        }
        public void Run()
        {
            Start();

            while(_gameOver == false)
            {
                Update();
            }

            End();
        }
    }
}
