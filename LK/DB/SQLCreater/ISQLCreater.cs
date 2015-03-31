using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace LK.DB.SQLCreater
{
    public interface ISQLCreater
    {
        OrderLimit getSplitOrderLimit();    
        void setSplitOrderLimit(OrderLimit limit);
        decimal? getStartCount() ;
        decimal? getFetchCount() ;
        string SQL();
        string FetchAllSQL();  
        void genParameter(string name, object value);
        void addParameter(System.Data.IDataParameter param);
        void removeSelfParameter();
        IDbDataParameter createDbParameter(System.Data.IDbDataParameter param, ParameterDirection pdirection);
        List<System.Data.IDataParameter> PARAMETER();       
    }
}
