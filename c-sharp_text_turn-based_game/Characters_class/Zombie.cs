using System;
using System.Collections.Generic;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public class Zombie : Character, IUndead, IScavenger
    {
        string IScavenger.Name { get => Name; set => Name = value; }
        int IScavenger.MaximumLife { get => MaximumLife; set => MaximumLife = value; }
        int IScavenger.CurrentLife { get => CurrentLife; set => CurrentLife = value; }


        public Zombie(string name) : base(name, 100, 0, 20, 60, 1000, 1000, 1, 1)
        {
        }



        public override int JetDefense()
        {
            return 0;   // jet de defense toujours égal à 0
        }


        public override void ActionCounterAttack(List<Tuple<int, Character>> characters, Character target, int margeAttack)
        {
            // Le Zombie ne peut pas contre-attaquer
            return;
        }



    }
}
