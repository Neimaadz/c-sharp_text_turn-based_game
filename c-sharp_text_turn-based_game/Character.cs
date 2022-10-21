using System;
using System.Collections.Generic;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public abstract class Character
    {
        public string Name;
        public int Attack;
        public int Defense;
        public int Initiative;
        public int Damage;
        public int MaximumLife;
        public int CurrentLife;
        public int CurrentAttackNumber;
        public int TotalAttackNumber;

        public Character()
        {
            
        }

        public Character(string name, int attack, int defense, int initiative, int damage, int maximumLife, int currentLife, int currentAttackNumber, int totalAttackNumber)
        {
            this.Name = name;
            this.Attack = attack;
            this.Defense = defense;
            this.Initiative = initiative;
            this.Damage = damage;
            this.MaximumLife = maximumLife;
            this.CurrentLife = currentLife;
            this.CurrentAttackNumber = currentAttackNumber;
            this.TotalAttackNumber = totalAttackNumber;
        }


        // Appler au début du combat
        public void OnInit()
        {
            Console.WriteLine("{0} ({1}) rejoint la partie", Name, this.GetType().Name);
        }

        // Appler à chaque début round
        public virtual void OnEachRound()
        {
            CurrentAttackNumber = TotalAttackNumber;    // Réinitialisation des points d'actions
        }


        public virtual int JetInitiative()
        {
            return Initiative + new Random().Next(1, 101);
        }

        public virtual int JetAttack()
        {
            return Attack + new Random().Next(1, 101);
        }

        public virtual int JetDefense()
        {
            return Defense + new Random().Next(1, 101);
        }

        public virtual void ActionAttack(List<Tuple<int, Character>> characters)
        {
            //============================ Target ==============================================================
            Round.Target = new List<Character>();   // Rénitialiser la liste des targets
            Character target;

            if (Round.PlayerTurn == true)
            {
                target = PlayerActions.ChooseTarget(characters, Program.PlayerCharacter);
                Console.WriteLine();
            }
            else
            {
                target = RandomTarget(characters);
            }

            Round.Target.Add(target);
            //==================================================================================================

            CurrentAttackNumber -= 1;   // On retire -1 point d'attaque

            Console.WriteLine("{0} lance Attaque", Name);

            int margeAttack = JetAttack() - target.JetDefense();
            int damageDeal = margeAttack * Damage / 100;

            DealDamage(characters, target, margeAttack, damageDeal);
        }

        public virtual void ActionCounterAttack(List<Tuple<int, Character>> characters, Character target, int margeAttack)
        {
            CurrentAttackNumber -= 1;   // On retire -1 point d'attaque

            Console.WriteLine("{0} lance Contre-attaque", Name);

            int bonusAttack = margeAttack * (-1);
            int margeCounterAttack = (JetAttack() + bonusAttack) - target.JetDefense();
            int damageDeal = margeCounterAttack * Damage / 100;

            DealDamage(characters, target, margeCounterAttack, damageDeal);
        }


        // =======================================================================
        // Permet de savoir si une attaque est réussi ou pas selon la margeAttack
        // =======================================================================
        public virtual void DealDamage(List<Tuple<int, Character>> characters, Character target, int margeAttack, int damageDeal)
        {
            switch (margeAttack)
            {
                //============================ Attaque réussi ===========================================================
                case int n when n > 0:

                    // Dégats communs
                    DealCommonDamage(target, damageDeal);

                    //============================ Cas de la cible ===========================================================

                    // Si cible est sensible à la douleur
                    if (target is IPain)
                    {
                        (target as IPain).Pain(damageDeal);     // damageDeal = dégat subis
                    }

                    IsCharacterDead(characters, target);
                    break;

                //============================ Defense de l'adversaire réussi ===========================================================
                case int n when n <= 0:
                    Console.WriteLine("Echec de l'attaque...");

                    if (target.CurrentAttackNumber > 0)    // Si le défenseur qui contre-attaque possède assez de point d'attaque
                    {
                        target.ActionCounterAttack(characters, this, margeAttack);  // Defenseur contre attaque
                    }

                    break;
            }
        }


        // =======================================================================
        // Retourne une cible aléatoire
        // =======================================================================
        public virtual Character RandomTarget(List<Tuple<int, Character>> characters)
        {
            Character target;
            int numbCharactersRemaining = characters.Count; // Nombre de personnage restant
            int index = 0;

            // Afin d'éviter de s'auto attaquer
            do
            {
                index = new Random().Next(0, numbCharactersRemaining);
            }
            while (characters[index].Item2.Equals(this));  // Tant que c'est le même perso

            target = characters[index].Item2;

            return target;
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
        // Inflige des dégats communs
        // =======================================================================
        public static void DealCommonDamage(Character target, int damageDeal)
        {
            Console.WriteLine("{0} : -{1} PDV", target.Name, damageDeal);
            target.CurrentLife -= damageDeal;
        }


        // =======================================================================
        // Permet de check s'il y a un personnage qui est mort
        // =======================================================================
        public static void IsCharacterDead(List<Tuple<int, Character>> characters, Character character)
        {
            int index = 0;

            if (character.CurrentLife <= 0)
            {
                while (characters[index].Item2.Name != character.Name)  // Afin de trouver, parmi la liste, le perso qui est mort selon son Nom
                {
                    index++;
                }
                Console.WriteLine("{0} est mort", character.Name);

                for (int i = 0; i < characters.Count; i++)
                {
                    if (characters[i].Item2 is IScavenger && character.Name != characters[i].Item2.Name)    // Si il y a des charognards dans la partie et pour ne pas s'auto manger
                    {
                        (characters[i].Item2 as IScavenger).EatDeadCharacter();
                    }
                }

                characters.RemoveAt(index); // On retire le personnage mort de liste des personnages
            }
        }





    }
}
