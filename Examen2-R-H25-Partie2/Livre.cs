using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen2_R_H25_Partie2
{
    public class Livre
    {
        private string _isbn;
        public string Titre { get; set; }
        public string Auteur { get; set; }

        private float _prix;

        private int _nombrePages;
        
        private int _anneePublication;

        public string ISBN
        {
            get => _isbn;
            set
            {
                string chiffres = value.Replace("-", "");
                if (!((chiffres.Length==10 || chiffres.Length == 13)
                    && long.TryParse(chiffres, out _)))
                    throw new ArgumentException("ISBN invalide. Il doit contenir 10 ou 13 chiffres avec ou sans tirets.");
                _isbn = value;
            }
        }

        public float Prix
        {
            get => _prix;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Le prix ne peut pas être négatif.");
                _prix = value;
            }
        }

        public int NombrePages
        {
            get => _nombrePages;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Le nombre de pages doit être positif.");
                _nombrePages = value;
            }
        }

        public int AnneePublication
        {
            get => _anneePublication;
            set
            {
                if (value < 1450 || value > DateTime.Now.Year)
                    throw new ArgumentException("L'année de publication doit être comprise entre 1450 et l'année actuelle.");
                _anneePublication = value;
            }
        }
        public Livre()
        {
        }
        public Livre(string isbn, string titre, string auteur, float prix, int nombrePages, int anneePublication)
        {
            ISBN = isbn;
            Titre = titre;
            Auteur = auteur;
            Prix = prix;
            NombrePages = nombrePages;
            AnneePublication = anneePublication;
        }

        public override string ToString()
        {
            return $"Livre: {Titre} | Auteur: {Auteur} | ISBN: {ISBN} | Prix: {Prix}$ | Pages: {NombrePages} | Année: {AnneePublication}";
        }
    }
}