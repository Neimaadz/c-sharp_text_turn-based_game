using System;
using System.Collections.Generic;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public class Priest : Character, IAlive, IBlessed, IHolyDamage
    {
        private int CountAttackOff = 0;  // Compteur de Round attaque off

        string IHolyDamage.Name { get => Name; set => Name = value; }

        Type IPain.CharacterType { get => GetType(); set => GetType(); }
        string IPain.Name { get => Name; set => Name = value; }
        int IPain.CurrentLife { get => CurrentLife; set => CurrentLife = value; }
        int IPain.CurrentAttackNumber { get => CurrentAttackNumber; set => CurrentAttackNumber = value; }
        int IPain.TotalAttackNumber { get => TotalAttackNumber; set => TotalAttackNumber = value; }
        int IPain.CountAttackOff { get => CountAttackOff; set => CountAttackOff = value; }


        public Priest(string name) : base(name, 75, 125, 50, 50, 150, 150, 1, 1)
        {
        }


        // Appler à chaque début round
        public override void OnEachRound()
        {
            CurrentAttackNumber = TotalAttackNumber;    // Réinitialisation des points d'actions

            // Priest : Se soigne de 10% de MaximumLife au début de chaque tour
            Heal();

            (this as IPain).IsSensitiveToPain();    // Check si on est affecté par la douleur
        }



        // =======================================================================
        // (Prêtre) Se soigne de 10% de MaximumLife au début de chaque tour
        // =======================================================================
        private void Heal()
        {
            int heal = (int)(MaximumLife * 0.1);

            Console.WriteLine("{0} se soigne", Name);
            Console.WriteLine("{0} : +{1} PDV", Name, heal);
            Console.WriteLine();

            CurrentLife += heal;

            if (CurrentLife >= MaximumLife)  // Pour caper la vie
            {
                CurrentLife = MaximumLife;
            }
        }


        // =======================================================================
        // (Prêtre) Inflige des dégâts sacrés
        // =======================================================================
        public override void DealDamage(List<Tuple<int, Character>> characters, Character target, int margeAttack, int damageDeal)
        {
            switch (margeAttack)
            {
                //============================ Attaque réussi ===========================================================
                case int n when n > 0:

                    // Prêtre : Inflige des dégâts sacrés
                    (this as IHolyDamage).DealHolyDamage(target, damageDeal);

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
        // (Prêtre) cible en priorité les morts-vivants
        // =======================================================================
        public override Character RandomTarget(List<Tuple<int, Character>> characters)
        {
            Character target;
            int numbCharactersRemaining = characters.Count; // Nombre de personnage restant
            int index = 0;

            List<int> indexUndeadCharacters = new List<int>();  // Liste contenant la position des Mort-vivants parmi la liste de persos

            for (int i = 0; i < numbCharactersRemaining; i++)
            {
                if (characters[i].Item2 is IUndead)  // S'il y a un Mort-vivant parmi la liste des perso
                {
                    indexUndeadCharacters.Add(i);   // On les ajoutes dans une liste
                }
            }

            if (indexUndeadCharacters.Count > 0)  // S'il reste des Mort-Vivants;
            {
                int numbUndeadCharactersRemaining = indexUndeadCharacters.Count; // Nombre de Mort-vivant restant
                index = new Random().Next(0, numbUndeadCharactersRemaining);

                // Grâce à notre liste contenant la position des Mort-vivant, on choisi la position parmi la liste de persos
                target = characters[indexUndeadCharacters[index]].Item2;
            }
            else
            {
                // Afin d'éviter de s'auto attaquer
                do
                {
                    index = new Random().Next(0, numbCharactersRemaining);
                }
                while (characters[index].Item2.Equals(this));  // Tant que c'est le même perso

                target = characters[index].Item2;
            }

            return target;
        }





    }
}
