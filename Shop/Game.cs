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
            _blueBerry.name = "Blue Berry";
            _blueBerry.cost = 1000;
            //Make the anime figure
            _animeFigure.name = "An Anime Figure";
            _animeFigure.cost = 2000;
            //make the literal panacea
            _panacea.name = "The literal Panacea";
            _panacea.cost = 1;
        }

        void save()
        {
            StreamWriter writer = new StreamWriter("SaveData.txt");
        }

        bool load()
        {
            return true;
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

            int choice = GetInput("Please, so are you a new customer or a returning one?", "New Customer (New Game)", "Returning one (Load Game)");

            if (choice == 0)
            {
                _currentScene++;
            }
            if (choice == 1)
            {
                if (load())
                {
                    Console.WriteLine("Oh well welcome back! (Load Successful)");
                    Console.ReadKey(true);
                    Console.Clear();
                    _currentScene++;
                }
            }
        }

        string[] GetShopMenuOptions(item[] inventory)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + inventory[i].name + inventory[i].cost);
            }
            return inventory;
        }

        void DisplayShopMenu()
        {
            InitializeItems();
            GetShopMenuOptions(_shopStock);
            Console.WriteLine("What are you buying?");
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
