using System;
using System.Collections.Generic;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    class Program
    {
        public static Character PlayerCharacter;

        static void Main(string[] args)
        {
            List<Tuple<int, Type>> listTypes = new List<Tuple<int, Type>>();
            List<Tuple<int, Character>> characters = new List<Tuple<int, Character>>(); // Tuple contenant le jetInitiative (de chaque round) associé au personnage


            // ********************************** Liste des types de personnage disponible **********************************
            listTypes.Add(new Tuple<int, Type>(1, typeof(Warrior)));
            listTypes.Add(new Tuple<int, Type>(2, typeof(Guardian)));
            listTypes.Add(new Tuple<int, Type>(3, typeof(Berserk)));
            listTypes.Add(new Tuple<int, Type>(4, typeof(Zombie)));
            listTypes.Add(new Tuple<int, Type>(5, typeof(Robot)));
            listTypes.Add(new Tuple<int, Type>(6, typeof(Lich)));
            listTypes.Add(new Tuple<int, Type>(7, typeof(Ghoul)));
            listTypes.Add(new Tuple<int, Type>(8, typeof(Vampire)));
            listTypes.Add(new Tuple<int, Type>(9, typeof(Priest)));
            listTypes.Add(new Tuple<int, Type>(10, typeof(Kamikaze)));




            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                       *****************************************************************                               ");
            Console.WriteLine("                  ***************************************************************************                          ");
            Console.WriteLine("             *************************************************************************************                     ");
            Console.WriteLine("        ******************                                                           ******************                ");
            Console.WriteLine("   ***********************                                                           ***********************           ");
            Console.WriteLine("**************************                  BIENVENUE SUR LE JEU                     **************************        ");
            Console.WriteLine("   ***********************                                                           ***********************           ");
            Console.WriteLine("        ******************                                                           ******************                ");
            Console.WriteLine("             *************************************************************************************                     ");
            Console.WriteLine("                  ***************************************************************************                          ");
            Console.WriteLine("                       *****************************************************************                               ");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            int numberCharacters = NumberCharacters();  // Nombre de personnages participant au combat

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("**********************************************************************");
            Console.WriteLine("*****                            GAME MODE                       *****");
            Console.WriteLine("**********************************************************************");
            Console.WriteLine("*****                                                            *****");
            Console.WriteLine("*****                                                            *****");
            Console.WriteLine("*****    1 : Player vs AI (Random)       2 : Demo AI (Random)    *****");
            Console.WriteLine("*****                                                            *****");
            Console.WriteLine("*****    3 : Player vs AI (Custom)       4 : Demo AI (Custom)    *****");
            Console.WriteLine("*****                                                            *****");
            Console.WriteLine("*****                                                            *****");
            Console.WriteLine("**********************************************************************");
            Console.WriteLine();

            int gameMode = GameMode(); // Choix mode de jeu

            Console.WriteLine();
            Console.WriteLine();


            // Game mode RANDOM
            if (gameMode == 1 || gameMode == 2)
            {
                // Création d'une liste de personnages Random
                characters = RandomListCharacters(listTypes, numberCharacters, gameMode);
            }
            // Game mode CUSTOM
            else
            {
                // Création d'une liste de personnages Custom
                characters = CustomListCharacters(listTypes, numberCharacters, gameMode);
            }

            if (gameMode == 1 || gameMode == 3) // Player vs AI
            {
                // ********************************** Init choix du Joueur **********************************
                string playerCharacterName = PlayerActions.ChooseCharacterName(characters);
                Console.WriteLine();

                Type playerCharacterType = PlayerActions.ChooseCharacterType(listTypes);

                // Création de l'instance Character selon le type choisi par le joueur
                Character playerCharacter = (Character)Activator.CreateInstance(playerCharacterType, playerCharacterName);
                characters.Add(new Tuple<int, Character>(0, playerCharacter));

                Console.WriteLine();

                // ********************************** FIN : Init choix du Joueur **********************************


                Battle battle = new Battle(characters);
                PlayerCharacter = playerCharacter;
                battle.StartBattle();
            }
            else  // Demo AI
            {
                Battle battle = new Battle(characters);
                battle.StartBattle();
            }
        }






        /*
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
        /****************************************************************************************************************************
         *********************************                     FONCTION DIVERS                          *****************************
         ****************************************************************************************************************************/


        // =======================================================================
        // Select game mode
        // =======================================================================
        public static int GameMode()
        {
            ConsoleKeyInfo playerAnswer;
            bool isNumber = true;
            int number;

            do
            {
                Console.WriteLine();
                Console.Write("Please select a game mode : ");
                playerAnswer = Console.ReadKey();
                isNumber = int.TryParse(playerAnswer.KeyChar.ToString(), out number);

                if (isNumber == false)
                {
                    Console.WriteLine();
                    Console.WriteLine("Erreur : veuillez entrer un nombre !");
                }
            }
            while (isNumber == false || !(1 <= number && number <= 4) );    // number DOIT ETRE compris entre 1 et 4

            return number;
        }


        // =======================================================================
        // Return number of characters
        // =======================================================================
        public static int NumberCharacters()
        {
            bool isNumber = true;
            int number = 0;

            do
            {
                Console.WriteLine();
                Console.Write("Number of characters (max 20) : ");
                isNumber = int.TryParse(Console.ReadLine(), out number);

                if (isNumber == false)
                {
                    Console.WriteLine();
                    Console.WriteLine("Erreur : veuillez entrer un nombre !");
                }
                else if (number < 2 || number > 20)
                {
                    Console.WriteLine();
                    Console.WriteLine("Erreur : nombre de personnages doit être compris entre 2 et 20 !");
                }
            }
            while (isNumber == false || number < 2 || number > 20);


            return number;
        }


        // =======================================================================
        // Return a list of random characters
        // =======================================================================
        public static List<Tuple<int, Character>> RandomListCharacters(List<Tuple<int, Type>> listTypes, int numberCharacters, int gameMode)
        {
            // Tuple contenant le jetInitiative (de chaque round) associé au personnage
            List<Tuple<int, Character>> characters = new List<Tuple<int, Character>>();

            if (gameMode == 1)  // Player vs AI (Random)
            {
                numberCharacters -= 1;  // pour avoir le bon nombre de participant : prendre en compte le personnage du joueur
            }

            for (int i=0; i< numberCharacters; i++)
            {
                int randNumb = new Random().Next(0, listTypes.Count);

                Type characterType = listTypes[randNumb].Item2;
                string characterName = characterType.Name + "_" + (i+1);

                // Création de l'instance Character selon le type choisi aléatoirement
                Character character = (Character)Activator.CreateInstance(characterType, characterName);

                characters.Add(new Tuple<int, Character>(0, character));
            }
            return characters;

        }


        // =======================================================================
        // Return a list of custom characters
        // =======================================================================
        public static List<Tuple<int, Character>> CustomListCharacters(List<Tuple<int, Type>> listTypes, int numberCharacters, int gameMode)
        {
            // Tuple contenant le jetInitiative (de chaque round) associé au personnage
            List<Tuple<int, Character>> characters = new List<Tuple<int, Character>>();

            bool isNumber = true;
            int number = 0;
            int j = 0;

            if (gameMode == 3)  // Player vs AI (Custom)
            {
                numberCharacters -= 1;  // pour avoir le bon nombre de participant : prendre en compte le personnage du joueur
            }

            for (int i=0; i< listTypes.Count; i++)
            {
                Console.WriteLine("{0} : {1}", listTypes[i].Item1, listTypes[i].Item2.Name);
            }

            Console.WriteLine();

            do
            {
                Console.Write("Add character : ");
                isNumber = int.TryParse(Console.ReadLine(), out number);

                if (isNumber == false)
                {
                    Console.WriteLine();
                    Console.WriteLine("Erreur : veuillez entrer un nombre !");
                    Console.WriteLine();
                }
                else if (number < 1 || number > listTypes.Count)
                {
                    Console.WriteLine();
                    Console.WriteLine("Erreur : impossible d'ajouter le personnage !");
                    Console.WriteLine();
                }
                else
                {
                    j++;
                    Type characterType = listTypes[number-1].Item2;
                    string characterName = characterType.Name + "_" + j;

                    // Création de l'instance Character selon le type choisi aléatoirement
                    Character character = (Character)Activator.CreateInstance(characterType, characterName);

                    characters.Add(new Tuple<int, Character>(0, character));
                }
            }
            while (isNumber == false || number < 1 || number > listTypes.Count || numberCharacters > characters.Count);

            Console.WriteLine();

            return characters;

        }





    }
}
