using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Gorny.KetchupCatalog.Interfaces;
using Gorny.KetchupCatalog.KetchupCatalogUI.Annotations;
using KetchupCatalogUI;

namespace Gorny.KetchupCatalog.KetchupCatalogUI.ViewModels
{
    class KetchupListViewModel : ViewModel
    {
        public EventList<KetchupViewModel> AllKetchups { get; set; }

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

        public Command SaveKetchupCommand => new Command(_ => SaveKetchup(), _ => CanSaveKetchup());

        public Command AddNewKetchupCommand => new Command(_ => AddKetchup());

        public Command FilterKetchupCommand => new Command(_ => FilterKetchup());

        public Command ClearFilterKetchupCommand => new Command(_ => ClearFilter());

        public Command DeleteKetchupCommand => new Command(_ => DeleteKetchup());

        public string FilterValue { get; set; }

        public KetchupListViewModel()
        {
            LoadKetchups();
            SelectedKetchup = Ketchups.First();
        }

        private void LoadKetchups()
        {
            AllKetchups = new EventList<KetchupViewModel>();
            AllKetchups.ItemsChangedEvent += AllKetchups_ItemsChangedEvent;
            foreach (IKetchup ketchup in (Application.Current as App)?.DataProvider.Ketchups ?? new List<IKetchup>())
            {
                AllKetchups.Add(new KetchupViewModel(ketchup));
            }
        }

        private void AllKetchups_ItemsChangedEvent(object sender, EventArgs e)
        {
            Ketchups = new ObservableCollection<KetchupViewModel>(AllKetchups);
            OnPropertyChanged(nameof(Ketchups));
        }

        private void SaveKetchup()
        {
            (Application.Current as App)?.DataProvider.SaveKetchup(SelectedKetchup.Ketchup);
            if (!Ketchups.Contains(SelectedKetchup))
            {
                AllKetchups.Add(SelectedKetchup);
            }
        }

        private bool CanSaveKetchup()
        {
            return !SelectedKetchup?.HasErrors ?? false;
        }

        private void AddKetchup()
        {
            IKetchup ketchup = (Application.Current as App)?.DataProvider.AddKetchup();
            SelectedKetchup = new KetchupViewModel(ketchup);
            OnPropertyChanged(nameof(SelectedKetchup));
        }

        private void ClearFilter()
        {
            FilterValue = string.Empty;
            OnPropertyChanged(nameof(FilterValue));
            FilterKetchup();
        }

        private void DeleteKetchup()
        {
            IKetchup ketchup = SelectedKetchup.Ketchup;
            if (Ketchups.Contains(SelectedKetchup))
            {
                AllKetchups.Remove(SelectedKetchup);
            }
            (Application.Current as App)?.DataProvider.DeleteKetchup(ketchup);
        }

        private void FilterKetchup()
        {
            if (string.IsNullOrEmpty(FilterValue))
            {
                Ketchups = new ObservableCollection<KetchupViewModel>(AllKetchups);
            }
            else
            {
                Ketchups = new ObservableCollection<KetchupViewModel>(AllKetchups.Where(p => p.FilterValue.IndexOf(FilterValue, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList());
            }
            OnPropertyChanged(nameof(Ketchups));
        }
    }
}
