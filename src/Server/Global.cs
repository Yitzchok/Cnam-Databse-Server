using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Raven.Client;
using Raven.Client.Embedded;

namespace Server
{
    public class Global
    {
        public static IDocumentStore Store { get; private set; }

        public Global()
        {
            //Assembly.LoadFile("Raven.Storage.Managed.dll");
            //Type type = Type.GetType("Raven.Storage.Managed.TransactionalStorage, Raven.Storage.Managed");
            //if (type == null)
            //    throw new InvalidOperationException("Could not find transactional storage type: " );
            

            var documentStore = new EmbeddableDocumentStore { DataDirectory = "Data" };
            documentStore.Configuration.DefaultStorageTypeName = "munin";
            //documentStore.Conventions.IdentityPartsSeparator = "-";
            //documentStore.Configuration.MemoryCacheLimitMegabytes = 128;
            Store = documentStore;
            Store.Initialize();
        }
    }
}
