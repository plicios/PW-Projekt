using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Gorny.KetchupCatalog.Interfaces;

namespace Gorny.KetchupCatalog.KetchupCatalogUI.ViewModels
{
    public class ProducerListViewModel : ViewModel
    {
        public EventList<ProducerViewModel> AllProducers { get; set; }
        public ObservableCollection<ProducerViewModel> Producers { get; set; }

        private ProducerViewModel _producerViewModel;

        public ProducerViewModel SelectedProducer
        {
            get => _producerViewModel;
            set
            {
                _producerViewModel = value;
                OnPropertyChanged(nameof(SelectedProducer));
            }
        }

        public Command SaveProducerCommand => new Command(param => SaveProducer(), _ => CanSaveProducer());

        public Command AddNewProducerCommand => new Command(param => AddProducer());

        public Command FilterProducerCommand => new Command(param => FilterProducer());

        public Command ClearFilterProducerCommand => new Command(param => ClearFilter());

        public Command DeleteProducerCommand => new Command(param => DeleteProducer(), _ => CanDeleteProducer());

        public string FilterValue { get; set; }


        public ProducerListViewModel()
        {
            LoadProducers();

            SelectedProducer = Producers.First();
        }

        private void LoadProducers()
        {
            AllProducers = new EventList<ProducerViewModel>();
            AllProducers.ItemsChangedEvent += AllProducers_ItemsChangedEvent;
            foreach (IProducer producer in (Application.Current as App)?.DataProvider.Producers ?? new List<IProducer>())
            {
                AllProducers.Add(new ProducerViewModel(producer));
            }
        }

        private void AllProducers_ItemsChangedEvent(object sender, EventArgs e)
        {
            Producers = new ObservableCollection<ProducerViewModel>(AllProducers);
            OnPropertyChanged(nameof(Producers));
        }

        private void SaveProducer()
        {
            (Application.Current as App)?.DataProvider.SaveProducer(SelectedProducer.Producer);
            if (!AllProducers.Contains(SelectedProducer))
            {
                AllProducers.Add(SelectedProducer);
                (Application.Current as App)?.SelectedKetchupViewModel.RefreshProducsers();
            }
        }

        private bool CanSaveProducer()
        {
            return !SelectedProducer?.HasErrors ?? false;
        }

        private bool _firtsTime = true;

        private bool CanDeleteProducer()
        {
            if (_firtsTime)
            {
                _firtsTime = false;
                return true;
            }

            foreach (IKetchup ketchup in (Application.Current as App)?.DataProvider.Ketchups ?? new List<IKetchup>())
            {
                if (ketchup.Producer.Equals(SelectedProducer.Producer))
                {
                    MessageBox.Show("Can't delete producer that is used in any ketchup",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return false;
                }
            }

            return true;
        }

        private void AddProducer()
        {
            IProducer producer = (Application.Current as App)?.DataProvider.AddProducer();
            SelectedProducer = new ProducerViewModel(producer);
        }

        private void DeleteProducer()
        {
            IProducer producer = SelectedProducer.Producer;
            if (AllProducers.Contains(SelectedProducer))
            {
                AllProducers.Remove(SelectedProducer);
            }
            (Application.Current as App)?.DataProvider.DeleteProducer(producer);
        }

        private void ClearFilter()
        {
            FilterValue = string.Empty;
            OnPropertyChanged(nameof(FilterValue));
            FilterProducer();
        }

        private void FilterProducer()
        {
            if (string.IsNullOrEmpty(FilterValue))
            {
                Producers = new ObservableCollection<ProducerViewModel>(AllProducers);
            }
            else
            {
                Producers = new ObservableCollection<ProducerViewModel>(AllProducers.Where(p => p.FilterValue.IndexOf(FilterValue, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList());
            }
            OnPropertyChanged(nameof(Producers));
        }
    }
}
