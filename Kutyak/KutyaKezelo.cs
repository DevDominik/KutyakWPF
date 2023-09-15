using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleAppKutyak
{
    public class KutyaKezelo
    {
        static Dictionary<int, string> kutyaNevekDict = new Dictionary<int, string>();
        static Dictionary<int, List<string>> kutyaFajtakDict = new Dictionary<int, List<string>>();

        List<Kutya> kutyak = new List<Kutya>();
        

        public KutyaKezelo()
        {
            foreach (var sor in File.ReadAllLines("Datas\\KutyaNevek.csv").Skip(1))
            {
                string[] mezok = sor.Split(';');
                kutyaNevekDict.Add(int.Parse(mezok[0]), mezok[1]);
            }

            foreach (var sor in File.ReadAllLines("Datas\\KutyaFajtak.csv").Skip(1))
            {
                string[] mezok = sor.Split(';');
                List<string> fajtaNevek = new List<string>();
                fajtaNevek.Add(mezok[1]);
                fajtaNevek.Add(mezok[2]);
                kutyaFajtakDict.Add(int.Parse(mezok[0]), fajtaNevek);
            }
            //Kutyák adatainak beolvasása

            List<string> sorok = File.ReadAllLines("Datas\\Kutyak.csv", encoding: Encoding.UTF8).ToList();
            sorok.RemoveAt(0);
            foreach (string s in sorok)
            {
                string[] mezok = s.Split(';');
                Kutya ujKutya = new Kutya(int.Parse(mezok[0]),
                                          int.Parse(mezok[1]),
                                          int.Parse(mezok[2]),
                                          int.Parse(mezok[3]),
                                          mezok[4]);
                kutyak.Add(ujKutya);
            }
        }

        public int NevekSzama { get => kutyaNevekDict.Count; }

        //(A ESET)
        //--------
        //Hol érdemes megvalósítani?  A kiszolgáló osztályban, azaz itt?
        public double AtlagEletkor { get => kutyak.Average(x => x.Eletkor); }
        public Kutya LegidosebbKutya { get => kutyak.MaxBy(x => x.Eletkor); }

        //(B ESET)
        //--------
        //Vagy engedjük hozzá az adatokhoz a külvilágot és intézze maga?
        //Ekkor nem csak legidősebbet, átlagéletkort, hanem bármi mást is lehet.
        //Ez a követendő szemlélet! Persze védjük az adatokat a szükséges módon
        //pld. módosítások ellen a set törlésével.
        public List<Kutya> Kutyak { get => kutyak;}

        //Osztálymetódusokkal szolgáljuk ki a konverziós igényeket
        static public string KutyaNeve(Kutya obj)
        {
            return kutyaNevekDict[obj.NevAzonosito];
        }
        static public string KutyaFajtaMagyarul(Kutya obj)
        {
            return kutyaFajtakDict[obj.FajtaAzonosito][0];
        }
        static public string KutyaFajtaAngolul(Kutya obj)
        {
            return kutyaFajtakDict[obj.FajtaAzonosito][1];
        }

        static public string KutyaNeveFromID(int id)
        {
            return kutyaNevekDict[id];
        }
        static public string KutyaFajtaMagyarulFromID(int id)
        {
            return kutyaFajtakDict[id][0];
        }
        static public string KutyaFajtaAngolulFromID(int id)
        {
            return kutyaFajtakDict[id][1];
        }


    }
}
