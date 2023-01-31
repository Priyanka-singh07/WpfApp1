using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp1.Repository
{
    public interface Ioperations
    {
        // object KeyPairDetails();
        List<dynamic> KeyPairDetails();

        void Save(string key,string val);
        void update(string key, string val);

        Hashtable Getkeyvaluedata();
    }
}
