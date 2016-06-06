using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Linq;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Version = Lucene.Net.Util.Version;

namespace PhotoMSK.Classes
{
    public interface ILuceneProvider
    {
        IndexSearcher Searcher { get; }
        IndexWriter Writer { get; }
        Query ParseQuery(string[] fields, string query);
    }

    public class LuceneProvider : ILuceneProvider
    {
        readonly Lazy<IndexSearcher> _searcher = new Lazy<IndexSearcher>(() =>
        {
            string path = HttpContext.Current.Server.MapPath("~/App_Data/LuceneIndex");

            var info = new DirectoryInfo(path);

            var directory = new SimpleFSDirectory(info);
            var fssession = FSDirectory.Open(info);

            Analyzer std = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30); //Version parameter is used for backward compatibility. Stop words can also be passed to avoid indexing certain words
            return new IndexSearcher(fssession);
        });
        public IndexSearcher Searcher
        {
            get { return _searcher.Value; }
        }

        readonly Lazy<IndexWriter> _writer = new Lazy<IndexWriter>(() =>
        {
            string path = HttpContext.Current.Server.MapPath("~/App_Data/LuceneIndex");

            var info = new DirectoryInfo(path);

            var fssession = FSDirectory.Open(info);

            Analyzer std = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30); //Version parameter is used for backward compatibility. Stop words can also be passed to avoid indexing certain words
            return new IndexWriter(fssession, std, true, IndexWriter.MaxFieldLength.UNLIMITED); //Create an Index writer object.

        });
        public IndexWriter Writer { get { return _writer.Value; } }

        public Query ParseQuery(string[] fields,string query)
        {

           
            QueryParser parser = new MultiFieldQueryParser(Version.LUCENE_30, fields,
                new StandardAnalyzer(Version.LUCENE_30));
            parser.AllowLeadingWildcard = true;
            return parser.Parse(query);
        }
    }
}