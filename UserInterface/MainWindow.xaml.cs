using Api;
using System;
using System.Collections.Generic;
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

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task LoadApi()
        {
            var casesinfo = await Api.StateControl.LoadCases(txtBox.Text,"CASES");
            var regional = casesinfo.data.regional;
            bool mila = false;
            long dispCases=0,dispDeaths = 0,dispActive=0,dispRecovered=0,dispBeds = 0,dispVaccinated=0;
            string dispName="";
            foreach (var x in regional)
            {
                var curState = x.loc;
                if (curState == txtBox.Text)
                {
                    var total = x.totalConfirmed;
                    var deaths = x.deaths;
                    var recovered = x.discharged;
                    var active = total - (deaths+recovered);
                    dispName = curState;
                    dispCases = total;
                    dispActive = active;
                    dispDeaths = deaths;
                    dispRecovered = recovered;
                    mila = true;
                    break;
                }
            }
            if (!mila)
            {
                deathsPanel.Visibility = recPanel.Visibility = casesPanel.Visibility = bedPanel.Visibility = vacPanel.Visibility = activePanel.Visibility = Visibility.Hidden;
                stateName.Content = "State/Union Territory doesn't Exist";
                return;
            }
            casesinfo = await Api.StateControl.LoadCases(txtBox.Text, "BEDS");
            regional = casesinfo.data.regional;
            foreach (var x in regional)
            {
                var curState = x.state;
                if (curState == txtBox.Text)
                {
                    var beds = x.totalBeds;
                    dispBeds = beds;
                    break;
                }
            }

            casesinfo = await Api.StateControl.LoadCases(txtBox.Text, "VACCINE");
            regional = casesinfo.getBeneficiariesGroupBy;
            foreach (var x in regional)
            {
                var curState = x.state_name;
                if (curState == txtBox.Text)
                {
                    var vaccines = x.fully_vaccinated+x.partial_vaccinated;
                    dispVaccinated = vaccines;
                    break;
                }
            }
            deathsPanel.Visibility = recPanel.Visibility = casesPanel.Visibility = bedPanel.Visibility = vacPanel.Visibility = activePanel.Visibility = Visibility.Visible;
            stateName.Content = dispName;

            if (dispCases != 0) totCases.Content = dispCases;
            else totCases.Content = "Data Not Available";

            if (dispDeaths != 0) deathCases.Content = dispDeaths;
            else deathCases.Content = "Data Not Available";

            if (dispActive != 0) activeCases.Content = dispActive;
            else activeCases.Content = "Data Not Available";

            if (dispRecovered != 0) recCases.Content = dispRecovered;
            else recCases.Content = "Data Not Available";

            if(dispBeds!=0) bedCases.Content = dispBeds;
            else bedCases.Content = "Data Not Available";

            if (dispVaccinated != 0) vacCases.Content = dispVaccinated;
            else vacCases.Content = "Data Not Available";

        }

        private async Task LoadList()
        {
            var casesinfo = await Api.StateControl.LoadCases(txtBox.Text, "CASES");
            var bedsinfo = await Api.StateControl.LoadCases(txtBox.Text, "BEDS");
            var vaccineinfo= await Api.StateControl.LoadCases(txtBox.Text, "VACCINE");
            var india = casesinfo.data.summary;
            var totIndia = india.total;
            var deathIndia = india.deaths;
            var recIndia = india.discharged;
            var actIndia = totIndia - (recIndia + deathIndia);
            var regional = casesinfo.data.regional;

            var vacIndia = vaccineinfo.topBlock.vaccination.total_doses;

            var bedIndia = bedsinfo.data.summary.totalBeds;

            List<string> lst= new List<string>();
            foreach (var x in regional)
            {
                var curState = x.loc;
                lst.Add(curState);
            }

            txtBox.ItemsSource = lst;
            totCases.Content = totIndia;
            deathCases.Content = deathIndia;
            recCases.Content = recIndia;
            vacCases.Content = vacIndia;
            activeCases.Content = actIndia;
            bedCases.Content = bedIndia;
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadList();
        }

        private async void TrackCases_Click(object sender, RoutedEventArgs e)
        {
            await LoadApi();
        }
    }

}