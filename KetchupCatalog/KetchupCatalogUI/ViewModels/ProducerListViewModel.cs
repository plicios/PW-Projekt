using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using Gorny.KetchupCatalog.Interfaces;
using Gorny.KetchupCatalog.KetchupCatalogUI.Annotations;
using KetchupCatalogUI;

namespace Gorny.KetchupCatalog.KetchupCatalogUI.ViewModels
{
    class ProducerListViewModel : INotifyPropertyChanged
    {
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Command SaveProducerCommand => new Command(param => SaveKetchup());

        public Command AddNewProducerCommand => new Command(param => AddKetchup());

        public Command FilterProducerCommand => new Command(param => FilterKetchup());

        public Command ClearFilterProducerCommand => new Command(param => ClearFilter());

        public string FilterValue { get; set; }

        private readonly ListCollectionView _producerListCollectionView;

        public ProducerListViewModel()
        {
            LoadProducers();

            SelectedProducer = Producers.First();
            _producerListCollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(Producers);
        }

        private void LoadProducers()
        {
            Producers = new ObservableCollection<ProducerViewModel>();
            foreach (IProducer producer in (Application.Current as App)?.DataProvider.Producers ?? new List<IProducer>())
            {
                Producers.Add(new ProducerViewModel(producer));
            }

            OnPropertyChanged(nameof(Producers));
        }

        private void SaveKetchup()
        {
            (Application.Current as App)?.DataProvider.SaveProducer(SelectedProducer.Producer);
            if (!Producers.Contains(SelectedProducer))
            {
                Producers.Add(SelectedProducer);
            }

            OnPropertyChanged(nameof(Producers));
        }

        private void AddKetchup()
        {
            IProducer producer = (Application.Current as App)?.DataProvider.AddProducer();
            SelectedProducer = new ProducerViewModel(producer);
            OnPropertyChanged(nameof(Producers));
        }

        private void ClearFilter()
        {
            FilterValue = string.Empty;
            OnPropertyChanged(nameof(FilterValue));
            FilterKetchup();
        }

        private void FilterKetchup()
        {
            if (string.IsNullOrEmpty(FilterValue))
            {
                _producerListCollectionView.Filter = null;
            }
            else
            {
                _producerListCollectionView.Filter = producerViemModel =>
                    (producerViemModel as ProducerViewModel)?.FilterValue.IndexOf(FilterValue, StringComparison.InvariantCultureIgnoreCase) >= 0;
            }
        }
    }
}
