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
    }
}