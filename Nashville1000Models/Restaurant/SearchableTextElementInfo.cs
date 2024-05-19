using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nashville1000Models.Restaurant
{
    public class SearchableTextElementInfo
    {
        public SearchableTextElementInfo(string searchText, string className, string propertyName)
        {
            SearchText = searchText;
            ClassName = className;
            PropertyName = propertyName;
        }

        public SearchableTextElementInfo(string className, string propertyName)
        {
            ClassName = className;
            PropertyName = propertyName;
        }

        public string SearchText { get; set; }
        public string ClassName { get; set; }
        public string PropertyName { get; set; }
        public object PropertyValue { get; set; }
    }
}
