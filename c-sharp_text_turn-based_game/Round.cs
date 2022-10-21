using System;
using System.Collections.Generic;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public class Round
    {
        private List<Tuple<int, Character>> Characters;
        private Character PlayerCharacter = Program.PlayerCharacter;
        public static List<Character> Target = new List<Character>();
        public static bool PlayerTurn;  // Boolean si c'est au tour du Player de jouer

        public Round(List<Tuple<int, Character>> characters)
        {
            this.Characters = characters;
        }



        public void PlayRound()
        {
            Target = new List<Character>();
            List<Tuple< int, Character >> charactersBefore = new List<Tuple<int, Character>>(Characters);

            for (int i = 0; i < Characters.Count; i++)
            {
                Character currentCharacter = Characters[i].Item2;


                // ************************* Round du JOUEUR *************************

                if (currentCharacter == PlayerCharacter && PlayerCharacter.CurrentAttackNumber > 0 && PlayerCharacter.CurrentLife > 0 && Battle.HaveWinner(Characters) == false)
                {
                    PlayerTurn = true;

                    // Tant que personnage du JOUEUR peux attaquer
                    while (PlayerCharacter.CurrentAttackNumber > 0 && PlayerCharacter.CurrentLife > 0 && Battle.HaveWinner(Characters) == false)
                    {
                        PlayerCharacter.ActionAttack(Characters);
                        Console.WriteLine();

                        // Le personnage JOUEUR meurt (d'une contre-attaque)
                        if (PlayerCharacter.CurrentLife <= 0)
                        {
                            AlertPlayerCharacterDead();
                        }

                        i = UpdateIndex(charactersBefore, currentCharacter, Target, i);
                    }

                    PlayerTurn = false;

                    PlayerActions.PressSpaceContinue();
                }

                // ************************* Round d'un PNJ *************************

                if (currentCharacter != PlayerCharacter)
                {
                    while (currentCharacter.CurrentAttackNumber > 0 && currentCharacter.CurrentLife > 0 && Battle.HaveWinner(Characters) == false)
                    {
                        currentCharacter.ActionAttack(Characters);
                        Console.WriteLine();
                        //Target.ForEach(c => { Console.WriteLine(c); });

                        // Le personnage JOUEUR est la cible et meurt (de l'attaque)
                        if (Target.Contains(PlayerCharacter) && PlayerCharacter.CurrentLife <= 0)
                        {
                            AlertPlayerCharacterDead();
                            PlayerActions.PressSpaceContinue();
                        }

                        i = UpdateIndex(charactersBefore, currentCharacter, Target, i);
                    }
                }


                AlertCantAttack(currentCharacter);

                Battle.HaveWinner(Characters); // On check s'il y a un gagnant durant le round
            }

            Battle.AlertHaveWinner(Characters);    // Alert Message : Winner
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
        // Met à jour l'indice selon cas précis
        // =======================================================================
        private int UpdateIndex(List<Tuple<int, Character>> charactersBefore, Character currentCharacter,
            List<Character> target, int i)
        {
            // Il y a eu un/des personnages morts
            if (Characters.Count < charactersBefore.Count)
            {
                bool isTargetDead = false;
                target.ForEach(c => { if (c.CurrentLife <= 0) isTargetDead = true; });

                // On se fait tué par une Contre-attaque
                if (currentCharacter.CurrentLife <= 0)
                {
                    i -= 1;
                    return i;
                }
                // On tue un/des personnages
                if (isTargetDead)
                {
                    int countDeadCharacters = 0;
                    target.ForEach(c => { if (c.CurrentLife <= 0) countDeadCharacters += 1; });

                    int indexTarget = 0;
                    int indexCurrentCharacter = 0;

                    charactersBefore.ForEach(c => { if (currentCharacter == c.Item2) indexCurrentCharacter = charactersBefore.IndexOf(c); });

                    // Plusieurs morts (KAMIKAZE)
                    if (countDeadCharacters > 1)
                    {
                        for (int j=0; j<target.Count; j++)
                        {
                            if (target[j].CurrentLife <= 0)
                            {
                                charactersBefore.ForEach(c => { if (target[j] == c.Item2) indexTarget = charactersBefore.IndexOf(c); });

                                if (indexCurrentCharacter > indexTarget)
                                {
                                    i -= 1;
                                    return i;
                                }
                                else
                                {
                                    return i;
                                }
                            }
                        }
                    }
                    // 1 seul mort
                    else if (countDeadCharacters == 1)
                    {
                        charactersBefore.ForEach(c => { if (target[0] == c.Item2) indexTarget = charactersBefore.IndexOf(c); });

                        if (indexCurrentCharacter > indexTarget)
                        {
                            i -= 1;
                            return i;
                        }
                        else
                        {
                            return i;
                        }

                    }
                }
            }

            return i;
        }


        // =======================================================================
        // Affiche un message si un personnage ne peux plus attaquer
        // =======================================================================
        private void AlertCantAttack(Character currentCharacter)
        {
            if (currentCharacter.CurrentAttackNumber <= 0 && currentCharacter.CurrentLife > 0 && Battle.HaveWinner(Characters) == false)
            {
                Console.WriteLine("{0} ne peut plus attaquer", currentCharacter.Name);
                Console.WriteLine();
            }
        }


        // =======================================================================
        // Affiche la liste des personnages restants
        // =======================================================================
        public static void AlertCharactersRemaining(List<Tuple<int, Character>> characters, Character playerCharacter)
        {
            // On affiche la liste des personnages restant
            for (int i = 0; i < characters.Count; i++)
            {
                if (characters[i].Item2 != playerCharacter)
                {
                    Console.WriteLine("     {0} : {1} ({2}) vie : {3}",
                        i + 1, characters[i].Item2.Name, characters[i].Item2.GetType().Name, characters[i].Item2.CurrentLife);
                }
                else
                {
                    Console.WriteLine("===> {0} : {1} ({2}) vie : {3}", i + 1, playerCharacter.Name, playerCharacter.GetType().Name, playerCharacter.CurrentLife);
                }
            }
        }


        // =======================================================================
        // Affiche un message alert de la mort du personnage du JOUEUR
        // =======================================================================
        private void AlertPlayerCharacterDead()
        {
            Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            Console.WriteLine("xxxxxxxx                                        xxxxxxxx");
            Console.WriteLine("xxxxxxxx      Game Over : vous êtes mort !      xxxxxxxx");
            Console.WriteLine("xxxxxxxx                                        xxxxxxxx");
            Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            Console.WriteLine();
        }


        // =======================================================================
        // Affiche resume du round
        // =======================================================================
        public void AlertResumeRound()
        {
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("=====          Resume du round           =====");
            Console.WriteLine("==============================================");
            Console.WriteLine("=====                                         ");
            for (int i = 0; i < Characters.Count; i++)
            {
                if (Characters[i].Item2 != PlayerCharacter)
                {
                    Console.WriteLine("=====    {0} ({1}) vie restant : {2}", Characters[i].Item2.Name, Characters[i].Item2.GetType().Name, Characters[i].Item2.CurrentLife);
                }
                else
                {
                    Console.WriteLine("======>  {0} ({1}) vie restant : {2}", PlayerCharacter.Name, Characters[i].Item2.GetType().Name, PlayerCharacter.CurrentLife);
                }
            }
            Console.WriteLine("=====                                         ");
            Console.WriteLine("==============================================");
            Console.WriteLine();
        }







    }
}
