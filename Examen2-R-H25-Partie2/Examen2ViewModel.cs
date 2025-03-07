using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Examen2_R_H25_Partie2
{
    public class Examen2ViewModel : INotifyPropertyChanged
    {
        // TODO : Ajouter des propriétés avec encapsulation pour le binding
        // Les propriétés sont 2 ObservableCollection : Bibliotheque et Panier,
        private ObservableCollection<Livre> _listeLivresBiblio;
        private ObservableCollection<Livre> _listeLivresPanier;

        private int _indexPanier;
        private int _indexBiblio;

        private Livre _selectedLivrePanier;
        private Livre _selectedLivreBiblio;


        public ObservableCollection<Livre> ListeLivresPanier 
        {
            get => _listeLivresPanier;
            set 
            {
                _listeLivresPanier = value;
                OnPropertyChanged(nameof(ListeLivresPanier));
            }
        }
        public ObservableCollection<Livre> ListeLivresBiblio
        {
            get => _listeLivresBiblio;
            set
            {
                _listeLivresBiblio = value;
                OnPropertyChanged(nameof(ListeLivresBiblio));
            }
        }

        public Livre SelectedLivrePanier 
        {
            get => _selectedLivrePanier;
            set 
            {
                _selectedLivrePanier = value;
                OnPropertyChanged(nameof(SelectedLivrePanier));
            }
        }
        public Livre SelectedLivreBiblio 
        {
            get => _selectedLivreBiblio;
            set 
            {
                _selectedLivreBiblio = value;
                OnPropertyChanged(nameof(SelectedLivreBiblio));
            }
        }
        public int IndexPanier
        {
            get { return _indexPanier; }
            set
            {
                if (_indexPanier != value)
                {
                    _indexPanier = value;
                    OnPropertyChanged(nameof(IndexPanier));
                }
            }
        }
        public int IndexBiblio
        {
            get { return _indexBiblio; }
            set
            {
                if (_indexBiblio != value)
                {
                    _indexBiblio = value;
                    OnPropertyChanged(nameof(IndexBiblio));
                }
            }
        }
        // et 2 Livres : celui sélectionné dans le DataGrid et celui sélectionné dans le ListBox.
        // Ajouter aussi les 3 ICommand pour le binding sur les boutons et le menu.

        public ICommand OuvrirCommand { get; }
        public ICommand EmprunterCommand { get; }
        public ICommand RetournerCommand { get; }
        public ICommand SaveCommand { get; }
        public Examen2ViewModel()
        {
            Ouvrir();
            // TODO : Lire le fichier JSON pour initialiser et remplir la Bibliotheque;
            IndexBiblio = 0;
            SelectedLivreBiblio = ListeLivresBiblio[IndexBiblio];

            // TODO : Initialiser le panier à une liste vide
            ListeLivresPanier = new();

            // TODO : Initialiser les RelayCommand pour attacher les boutons à leurs gestionnaires d'évènements
            OuvrirCommand = new RelayCommand(Ouvrir);
            EmprunterCommand = new RelayCommand(Emprunter, CanEmprunter);
            RetournerCommand = new RelayCommand(Retourner, CanRetourner);
            SaveCommand = new RelayCommand(Save);
        }
        private void Ouvrir() 
        {
            string file = "livres.json";
            
            ListeLivresBiblio = JsonSerializer.Deserialize<ObservableCollection<Livre>>(File.ReadAllText(file));
            
        }
        // Gestionnaires d'évènements
        private bool CanEmprunter() => SelectedLivreBiblio != null;
        private void Emprunter()
        {
            // TODO : Ajouter le livre selectionné au panier
            ListeLivresPanier.Add(SelectedLivreBiblio);
            // Reset la sélection
            SelectedLivreBiblio = null;

        }
        private bool CanRetourner() => SelectedLivrePanier != null;
        private void Retourner()
        {
            ListeLivresPanier.Remove(SelectedLivrePanier);
            // Reset la sélection
            SelectedLivrePanier = null;

        }

        private void Save()
        {
            // TODO : Sauvegarder la Bibliotheque dans le fichier json
            try 
            {
                string file = "livres.json";
                File.WriteAllText(file, JsonSerializer.Serialize(ListeLivresBiblio));
                MessageBox.Show("Sauvegarde réussie!");
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }


        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
