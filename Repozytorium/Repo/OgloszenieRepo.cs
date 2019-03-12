using Repozytorium.IRepo;
using Repozytorium.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Repozytorium.Repo
{
    public class OgloszenieRepo : IOgloszenieRepo
    {
        private IOglContext _db = new OglContext();


        public OgloszenieRepo(IOglContext db)
        {
            _db = db;
        }

        //zla praktyka, nie powinno sie zwracac IQueryable, poniewaz wtedy filtrowanie wynikow odbywa sie poza repo i wtedy kod nie jest testowalny w 100%
        //powinno sie zwracac IEnumerable i jako parametr jakis filtr
        public IQueryable<Ogloszenie> PobierzOgloszenia()
        {
            _db.Database.Log = message => Trace.WriteLine(message);
            var ogloszenie = _db.Ogloszenia.AsNoTracking();
            return ogloszenie;
        }

        public Ogloszenie GetOgloszenieById(int id)
        {
            Ogloszenie ogloszenie = _db.Ogloszenia.Find(id);
            return ogloszenie;
        }

        public void UsunOgloszenie(int id)
        {
            UsunPowiazanieOgloszenieKategoria(id);
            Ogloszenie ogloszenie = _db.Ogloszenia.Find(id);
            _db.Ogloszenia.Remove(ogloszenie);
        }

        private void UsunPowiazanieOgloszenieKategoria(int idOgloszenia)
        {
            var list = _db.Ogloszenie_Kategoria.Where(o => o.OgloszenieId == idOgloszenia);
            foreach(var item in list)
            {
                _db.Ogloszenie_Kategoria.Remove(item);
            }
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public void Dodaj(Ogloszenie ogloszenie)
        {
            _db.Ogloszenia.Add(ogloszenie);
        }
    }
}