using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace LK.DB
{
    public interface IDBAction
    {
        TableBase Save(ref TableBase tbase,RecordBase record);
        TableBase Update(ref TableBase tbase, RecordBase record);
        TableBase Delete(ref TableBase tbase, RecordBase record);
        TableBase SaveAll(ref TableBase tbase);
        TableBase UpdateAll(ref TableBase tbase);
        TableBase DeleteAll(ref TableBase tbase);
    }
}
