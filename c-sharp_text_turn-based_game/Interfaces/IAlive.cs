using System;
namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public interface IAlive : IPain
    {
        public bool IsAlive()
        {
            return true;
        }
    }
}
