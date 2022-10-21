using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public class PlayerActions
    {
        public PlayerActions()
        {
        }






        // =======================================================================
        // Return un type de personnage choisi par JOUEUR
        // =======================================================================
        public static Type ChooseCharacterType(List<Tuple<int, Type>> listTypes)
        {
            bool isNumber = true;
            int characterId = 0;

            for (int i = 0; i < listTypes.Count; i++)
            {
                Console.WriteLine("{0} : {1}", listTypes[i].Item1, listTypes[i].Item2.Name);
            }

            do
            {
                Console.WriteLine();
                Console.Write("Classe de mon personnage : ");
                isNumber = int.TryParse(Console.ReadLine(), out int number);
                characterId = number;

                if (isNumber == false)
                {
                    Console.WriteLine();
                    Console.WriteLine("Erreur : veuillez entrer un nombre !");
                }
                else if (characterId < 1 || characterId > listTypes.Count)
                {
                    Console.WriteLine();
                    Console.WriteLine("Erreur : veuillez entrer un nombre parmi la liste !");
                }
            }
            while (isNumber == false || characterId < 1 || characterId > listTypes.Count);


            return listTypes[characterId - 1].Item2;
        }


        // =======================================================================
        // Return le nom du personnage choisi par le JOUEUR
        // =======================================================================
        public static string ChooseCharacterName(List<Tuple<int, Character>> characters)
        {
            bool isYes = true;
            string characterName = "";

            do
            {
                Console.WriteLine();
                Console.Write("Nom de mon personnage : ");
                characterName = Console.ReadLine();
                characterName = characterName.Trim();   // On supprime les caractères spéciaux au début et à la fin

                // Utilisation des expressions régulières afin de supprimer les caractère spéciaux
                // Nom personnage autorisé : /!\ UNIQUEMENT /!\ alphabet + chiffre
                characterName = Regex.Replace(characterName, "[^a-zA-Z0-9]+", "", RegexOptions.Compiled);

                if (characterName == "")
                {
                    Console.WriteLine();
                    Console.WriteLine("Erreur : veuillez entrer un nom de personnage !");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Valider le nom de votre personnage : {0}", characterName);
                    isYes = PlayerActions.PressYorN();

                    if (isYes == false)
                    {
                        characterName = "";
                    }

                    for (int i = 0; i < characters.Count; i++)
                    {
                        if (characterName.ToUpper() == characters[i].Item2.Name.ToUpper())
                        {
                            Console.WriteLine();
                            Console.WriteLine("Nom de personnage déjà utilisé !");
                            Console.WriteLine();
                            characterName = "";
                        }
                    }


                }
            }
            while (characterName == "");


            return characterName;
        }


        // =======================================================================
        // Return une cible choisi par le JOUEUR parmi la liste de personnage
        // =======================================================================
        public static Character ChooseTarget(List<Tuple<int, Character>> characters, Character playerCharacter)
        {
            Console.WriteLine();
            Console.WriteLine("*********************************************");
            Console.WriteLine("**********      À VOTRE TOUR !     **********");
            Console.WriteLine("*********************************************");
            Console.WriteLine();

            Round.AlertCharactersRemaining(characters, playerCharacter);  // Alert Message : characters remaining

            bool isNumber = true;
            int targetId = 0;

            do
            {
                Console.WriteLine();
                Console.Write("Choisissez un personnage à attaquer : ");
                isNumber = int.TryParse(Console.ReadLine(), out int number);
                targetId = number;

                if (isNumber == false)
                {
                    Console.WriteLine();
                    Console.WriteLine("Erreur : veuillez entrer un nombre !");
                }
                else if (targetId < 1 || targetId > characters.Count)
                {
                    Console.WriteLine();
                    Console.WriteLine("Erreur : veuillez entrer un nombre parmi la liste !");
                }
                else if (characters[targetId - 1].Item2 == playerCharacter)
                {
                    Console.WriteLine();
                    Console.WriteLine("Erreur : tu ne peux pas d'attaquer");
                }
            }
            while (isNumber == false || targetId < 1 || targetId > characters.Count || characters[targetId - 1].Item2 == playerCharacter);

            Console.WriteLine();
            Console.WriteLine("*********************************************");

            return characters[targetId - 1].Item2;
        }


        // =======================================================================
        // Menu de l'utilisateur : appuyer sur Espace
        // =======================================================================
        public static void PressSpaceContinue()
        {
            do
            {
                Console.WriteLine();
                Console.WriteLine("oooooooooooooooooooooooooooooooooooooooooooooo");
                Console.WriteLine("ooooo                                    ooooo");
                Console.WriteLine("ooo       Press SPACE to continue...       ooo");
                Console.WriteLine("ooooo                                    ooooo");
                Console.WriteLine("oooooooooooooooooooooooooooooooooooooooooooooo");
                Console.WriteLine();
            }
            while (Console.ReadKey().Key != ConsoleKey.Spacebar);

            Console.WriteLine();
        }


        // =======================================================================
        // Menu de l'utilisateur : appuyer Y ou N
        // =======================================================================
        public static bool PressYorN()
        {
            ConsoleKey playerAnswer;

            do
            {
                Console.Write("Press y (oui) or n (non) : ");
                playerAnswer = Console.ReadKey().Key;
                Console.WriteLine();
            }
            while (playerAnswer != ConsoleKey.Y && playerAnswer != ConsoleKey.N);

            if (playerAnswer == ConsoleKey.Y)
            {
                return true;
            }
            else if (playerAnswer == ConsoleKey.N)
            {
                Console.WriteLine();
                return false;
            }

            return false;
        }


        // =======================================================================
        // Menu de l'utilisateur : continuer de jouer ou de quitter
        // =======================================================================
        public static bool ContinueOrQuit()
        {
            ConsoleKey keyTyped;

            Console.WriteLine();
            Console.WriteLine("oooooooooooooooooooooooooooooooooooooooooooooo");
            Console.WriteLine("ooooo                                    ooooo");
            Console.WriteLine("ooo        Press ENTER to continue         ooo");
            Console.WriteLine("ooo            Press Q to quit             ooo");
            Console.WriteLine("ooooo                                    ooooo");
            Console.WriteLine("oooooooooooooooooooooooooooooooooooooooooooooo");
            Console.WriteLine();

            do
            {
                keyTyped = Console.ReadKey().Key;
                Console.WriteLine();
            }
            while (keyTyped != ConsoleKey.Enter && keyTyped != ConsoleKey.Q);


            if (keyTyped == ConsoleKey.Enter)
            {
                return false;
            }
            if (keyTyped == ConsoleKey.Q)
            {
                Console.WriteLine();
                Console.WriteLine("************************************************************************");
                Console.WriteLine("************          Voulez vous quitter le jeu ?          ************");
                Console.WriteLine("************************************************************************");
                Console.WriteLine();

                return PressYorN();
            }

            return false;
        }






    }
}
