using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Universites.Models;

namespace Universites.ViewModel
{
    public class UniversitesViewModel : INotifyPropertyChanged
    {
        public UniversitesViewModel()
        {
            Task.Run(async () =>
             {
                 await LoadData();
             });
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
                
                Countries = new List<Country>();
                countries.Add(new Country { Name = "All countries" });
                foreach(var item in Universites.OrderBy(i => i.Country).ToList().GroupBy(i => i.Country).ToList())
                    Countries.Add(new Country { Code = item.First().alpha_two_code, Name = item.First().Country });
            }
            catch (Exception)
            {

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
