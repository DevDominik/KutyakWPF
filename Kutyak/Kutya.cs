using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppKutyak
{
    public class Kutya
    {
        int id;
        int fajta_id;
        int nev_id;
        int eletkor;
        string utolsoOrvosiEllenorzes;

        public Kutya(int id, int fajta_id, int név_id, int életkor, string utolsoOrvosiEllenorzes)
        {
            this.id = id;
            this.fajta_id = fajta_id;
            this.nev_id = név_id;
            this.eletkor = életkor;
            this.utolsoOrvosiEllenorzes = utolsoOrvosiEllenorzes;
        }
        public int Azonosito { get => id;}
        public int FajtaAzonosito { get => fajta_id; }
        public int NevAzonosito { get => nev_id; }
        public int Eletkor { get => eletkor; }
        public string UtolsoEllenorzes { get => utolsoOrvosiEllenorzes; }
    }
}
