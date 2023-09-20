using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySqlConnector;
using System.Windows;

namespace ConsoleAppKutyak
{
    public class KutyaKezelo
    {
        static Dictionary<int, string> kutyaNevekDict = new Dictionary<int, string>();
        static Dictionary<int, List<string>> kutyaFajtakDict = new Dictionary<int, List<string>>();

        List<Kutya> kutyak = new List<Kutya>();
        

        public KutyaKezelo()
        {
            MySqlConnection kapcsolat;
            try
            {
                kapcsolat = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=kutyak");
            }
            catch (Exception)
            {
                MessageBox.Show("Nem tud az adatbázishoz kapcsolódni");
                throw;
            }
            kapcsolat.Open();
            MySqlCommand parancs = new MySqlCommand("select * from kutya_nevek", kapcsolat);
            MySqlDataReader olvaso = parancs.ExecuteReader();
            while (olvaso.Read()) {
                kutyaNevekDict[olvaso.GetInt32("id")] = olvaso.GetString("kutyanév");
            }
            olvaso.Close();
            parancs = new MySqlCommand("select * from kutya_fajtak", kapcsolat);
            olvaso = parancs.ExecuteReader();
            while (olvaso.Read())
            {
                kutyaFajtakDict[olvaso.GetInt32("id")] = new List<string> { olvaso.GetString("név"), olvaso.GetString("eredeti név")};
            }
            olvaso.Close();
            //Kutyák adatainak beolvasása
            parancs = new MySqlCommand("select * from kutyak", kapcsolat);
            olvaso = parancs.ExecuteReader();
            while (olvaso.Read())
            {
                kutyak.Add(new Kutya(olvaso.GetInt32("id"), olvaso.GetInt32("fajta_id"), olvaso.GetInt32("név_id"), olvaso.GetInt32("életkor"), olvaso.GetString("utolsó orvosi ellenőrzés")));
            }
            olvaso.Close();
            kapcsolat.Clone();
            kapcsolat.Dispose();
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
