using Lucene.Net.Documents;
using Lucene.Net.Index;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;

namespace Fotobel.Classes
{
    public static class WriterExtension
    {
        public static void Write(this IndexWriter writer, RouteEntity elem)
        {
          
            if(string.IsNullOrEmpty(elem.ToString()))
                return;


            var masterclass = elem as Masterclass;
            if (masterclass != null)
            {
                return;
                //document.Add(new Field("Name", masterclass.AuthorName, Field.Store.YES, Field.Index.ANALYZED));
            }
            var document = new Document();

            document.Add(new Field("Name", elem.GetName().ToLower(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("Description", elem.Description ?? "", Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("Type", elem.GetType().BaseType.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("ID", elem.ID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));

            writer.AddDocument(document);
        }
    }
}