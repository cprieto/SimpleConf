using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.DictionaryAdapter;

namespace SimpleConf
{
    public class CascadingMultipleDictionaryAdapter : AbstractDictionaryAdapter
    {
        private readonly IList<IDictionary<string, string>> _sources = new List<IDictionary<string, string>>();

        public CascadingMultipleDictionaryAdapter()
        {
        }

        public CascadingMultipleDictionaryAdapter(IEnumerable<IDictionary<string, string>> sources)
        {
            if (sources == null)
                throw new ArgumentNullException();

            _sources = new List<IDictionary<string, string>>(sources);
        }

        public IEnumerable<IDictionary<string, string>> Sources
        {
            get { return _sources; }
        }

        public void AddSource(IDictionary<string, string> source)
        {
            if (source == null)
                throw new ArgumentNullException();

            _sources.Add(source);
        }

        public override object this[object key]
        {
            get
            {
                string value;
                return TryGet(key as string, out value) ? value : null;
            }
            set { throw new NotSupportedException(); }
        }

        public override bool IsReadOnly
        {
            get { return true; }
        }

        public override bool Contains(object key)
        {
            if (key == null)
                throw new ArgumentNullException();

            return _sources.Any(x => x.ContainsKey(key as string));
        }

        public bool TryGet(string key, out string value)
        {
            foreach (var source in Sources.Reverse())
            {
                if (source.TryGetValue(key, out value))
                    return true;
            }
            value = null;
            return false;
        }
    }
}