using System;
namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public interface IScavenger
    {
        public string Name { get; set; }
        public int MaximumLife { get; set; }
        public int CurrentLife { get; set; }

        public void EatDeadCharacter()
        {
            int randLife = new Random().Next(50, 101);

            Console.WriteLine("{0} mange le cadavre et se régénère", Name);
            Console.WriteLine("{0} : +{1} PDV", Name, randLife);
            CurrentLife += randLife;

            // Pour caper la vie
            if (CurrentLife >= MaximumLife)
            {
                CurrentLife = MaximumLife;
            }
        }



    }
}
