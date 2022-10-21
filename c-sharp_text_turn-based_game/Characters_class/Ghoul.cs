using System;
namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public class Ghoul : Character, IUndead, IScavenger, IPain
    {
        private int CountAttackOff = 0;  // Compteur de Round attaque off

        Type IPain.CharacterType { get => GetType(); set => GetType(); }
        string IPain.Name { get => Name; set => Name = value; }
        int IPain.CurrentLife { get => CurrentLife; set => CurrentLife = value; }
        int IPain.CurrentAttackNumber { get => CurrentAttackNumber; set => CurrentAttackNumber = value; }
        int IPain.TotalAttackNumber { get => TotalAttackNumber; set => TotalAttackNumber = value; }
        int IPain.CountAttackOff { get => CountAttackOff; set => CountAttackOff = value; }

        string IScavenger.Name { get => Name; set => Name = value; }
        int IScavenger.MaximumLife { get => MaximumLife; set => MaximumLife = value; }
        int IScavenger.CurrentLife { get => CurrentLife; set => CurrentLife = value; }


        public Ghoul(string name) : base(name, 50, 80, 120, 30, 250, 250, 5, 5)
        {
        }



        // Appler à chaque début round
        public override void OnEachRound()
        {
            CurrentAttackNumber = TotalAttackNumber;    // Réinitialisation des points d'actions
            (this as IPain).IsSensitiveToPain();    // Check si on est affecté par la douleur
        }





    }
}
