using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Universites.Classes;
using Universites.Models;

namespace Universites.ViewModel
{
    public class UniversitesViewModel : INotifyPropertyChanged
    {
        public UniversitesViewModel()
        {
            Task.Run(async () => await LoadData());

            CountryChangedCommand = new RelayCommand(obj => CountryChanged());
        }
        private ICollection<University> universites;
        public ICollection<University> Universites
        {
            get { return universites; }
            set
            {
                universites = value;
                OnPropertyChanged("Universites");
            }
        }

        private ICollection<University> AllUniversites;

        private ICollection<Country> countries;
        public ICollection<Country> Countries
        {
            get { return countries; }
            set
            {
                countries = value;
                OnPropertyChanged("Countries");
            }
        }

        private Country selectionCountry;
        public Country SelectionCountry
        {
            get { return selectionCountry; }
            set
            {
                selectionCountry = value;
                OnPropertyChanged("SelectionCountry");
            }
        }


        public RelayCommand CountryChangedCommand { get; }


        private async Task LoadData()
        {
            string url = "http://universities.hipolabs.com/search?country=";

            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                var response = await client.GetAsync(client.BaseAddress);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                Universites = JsonConvert.DeserializeObject<BindingList<University>>(content);
                Universites = Universites.OrderBy(i => i.Name).ToList();
                AllUniversites = Universites;

                Countries = new List<Country>();
                countries.Add(new Country { Name = "All countries" });
                foreach (var item in Universites.OrderBy(i => i.Country).ToList().GroupBy(i => i.Country).ToList())
                    Countries.Add(new Country { Code = item.First().AlphaTwoCode, Name = item.First().Country });
            }
            catch (Exception)
            {

            }
        }

        private void CountryChanged()
        {
            Universites = AllUniversites;

            if (selectionCountry.Name != "All countries")
                Universites = Universites.Where(i => i.Country == SelectionCountry.Name).ToList();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
