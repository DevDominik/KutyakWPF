using ConsoleAppKutyak;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kutyak
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            KutyaKezelo kezelo = new KutyaKezelo();
            lbNevekSzama.Content = kezelo.NevekSzama;
            lbAtlagEletkor.Content = kezelo.AtlagEletkor;
            Kutya legidosebb = kezelo.LegidosebbKutya;
            lbLegidosebbKutya.Content = $"{KutyaKezelo.KutyaNeve(legidosebb)}; {KutyaKezelo.KutyaFajtaMagyarul(legidosebb)}";

            var legsurubbNap = kezelo.Kutyak.GroupBy(x => x.UtolsoEllenorzes).MaxBy(x => x.Count());
            
            lbLegleterheltebb.Content = $"{legsurubbNap.Key}; {legsurubbNap.Count()} kutya";
        }
    }
}
