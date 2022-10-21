using System;
using System.Collections.Generic;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public class Berserk : Character, IAlive
    {
        private int CountAttackOff = 0;  // Compteur de Round attaque off

        Type IPain.CharacterType { get => GetType(); set => GetType(); }
        string IPain.Name { get => Name; set => Name = value; }
        int IPain.CurrentLife { get => CurrentLife; set => CurrentLife = value; }
        int IPain.CurrentAttackNumber { get => CurrentAttackNumber; set => CurrentAttackNumber = value; }
        int IPain.TotalAttackNumber { get => TotalAttackNumber; set => TotalAttackNumber = value; }
        int IPain.CountAttackOff { get => CountAttackOff; set => CountAttackOff = value; }


        public Berserk(string name) : base(name, 100, 100, 80, 20, 300, 300, 1, 1)
        {
        }


        // Appler à chaque début round
        public override void OnEachRound()
        {
            CurrentAttackNumber = TotalAttackNumber;    // Réinitialisation des points d'actions

            // Berserk : TotalAttackNumber passe à 4 si sa vie est en dessous de 50%
            IncreaseAttackNumber();

            // Pas de check (this as IPain).IsSensitiveToPain() : Le berseker n’est pas affecté par la douleur
        }




        // =======================================================================
        // (Berseker) TotalAttackNumber passe à 4 si sa vie est en dessous de 50%
        // =======================================================================
        private void IncreaseAttackNumber()
        {
            if (CurrentLife < (MaximumLife * 0.5))  // inférieur à 50% de sa vie max
            {
                Console.WriteLine("{0} entre dans l'état Berseker", Name);
                Console.WriteLine("{0} : +4 PA", Name);
                Console.WriteLine();

                TotalAttackNumber = 4;
                CurrentAttackNumber = TotalAttackNumber;
            }
        }


        // =======================================================================
        // (Berseker) Ajoute tous les points de vie qu'il a perdu a ses dégâts au moment d’attaquer
        // =======================================================================
        public override void ActionAttack(List<Tuple<int, Character>> characters)
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
            int damageDeal = margeAttack * (Damage + (MaximumLife - CurrentLife)) / 100; // Ajoute vie perdu aux dégats

            DealDamage(characters, target, margeAttack, damageDeal);
        }


        // =======================================================================
        // (Berseker) Le berseker n’est pas affecté par la douleur
        // =======================================================================
        void IPain.Pain(int damageTaken)
        {
            Console.WriteLine("{0} est insensible à la douleur !", Name);
        }




    }
}
