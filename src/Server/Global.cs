using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;
using Raven.Client.Document;

namespace Server
{
    public class Global
    {
        public static IDocumentStore DocumentStore { get; private set; }

        public Global()
        {
            DocumentStore = new DocumentStore().Initialize();
        }
    }
}
