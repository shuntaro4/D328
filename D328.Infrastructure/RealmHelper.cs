using Realms;
using System;

namespace D328.Repository
{
    public class RealmHelper
    {
        public static Realm GetInstance()
        {
            var dbpath = $@"{AppDomain.CurrentDomain.BaseDirectory}D328.realm";
            var config = new RealmConfiguration(dbpath);
            return Realm.GetInstance(config);
        }
    }
}
