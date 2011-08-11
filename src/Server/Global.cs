using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;

namespace Server
{
    public class Global
    {
        public static IDocumentStore Store { get; private set; }

        public Global()
        {
            Store = new EmbeddableDocumentStore { DataDirectory = "Data" }.Initialize();
        }
    }
}
