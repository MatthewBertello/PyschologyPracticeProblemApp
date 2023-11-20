using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp.Model.Sql; 
public class DatabaseCache {
    public LinkedList<HistoryLog> History { get; private set; }
    public Boolean IsDirty { get; set; } = true;

}
