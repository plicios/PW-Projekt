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
    class KetchupListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<KetchupViewModel> Ketchups { get; set; }

        private KetchupViewModel _ketchupViewModel;

        public KetchupViewModel SelectedKetchup
        {
            get => _ketchupViewModel;
            set
            {
                _ketchupViewModel = value;
                OnPropertyChanged(nameof(SelectedKetchup));
            }
        }

        public Command SaveKetchupCommand => new Command(param => SaveKetchup());

        public Command AddNewKetchupCommand => new Command(param => AddKetchup());

        public Command FilterKetchupCommand => new Command(param => FilterKetchup());

        public Command ClearFilterKetchupCommand => new Command(param => ClearFilter());

        public string FilterValue { get; set; }

        private readonly ListCollectionView _kethupListCollectionView;

        public KetchupListViewModel()
        {
            LoadKetchups();
            _kethupListCollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(Ketchups);

            SelectedKetchup = Ketchups.First();
        }

        private void LoadKetchups()
        {
            Ketchups = new ObservableCollection<KetchupViewModel>();
            foreach (IKetchup ketchup in (Application.Current as App)?.DataProvider.Ketchups ?? new List<IKetchup>())
            {
                Ketchups.Add(new KetchupViewModel(ketchup, (Application.Current as App)?.DataProvider.Producers as List<IProducer>));
            }

            OnPropertyChanged(nameof(Ketchups));
        }

        private void SaveKetchup()
        {
            (Application.Current as App)?.DataProvider.SaveKetchup(SelectedKetchup.Ketchup);
            if (!Ketchups.Contains(SelectedKetchup))
            {
                Ketchups.Add(SelectedKetchup);
            }
            OnPropertyChanged(nameof(Ketchups));
        }

        private void AddKetchup()
        {
            IKetchup ketchup = (Application.Current as App)?.DataProvider.AddKetchup();
            SelectedKetchup = new KetchupViewModel(ketchup, (Application.Current as App)?.DataProvider.Producers as List<IProducer>);
            OnPropertyChanged(nameof(SelectedKetchup));
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
                _kethupListCollectionView.Filter = null;
            }
            else
            {
                _kethupListCollectionView.Filter = ketchupViewModel =>
                    (ketchupViewModel as KetchupViewModel)?.FilterValue.IndexOf(FilterValue, StringComparison.InvariantCultureIgnoreCase) >= 0;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
