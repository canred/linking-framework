using System.Collections.Generic;
using System.Web;

namespace ExtDirect.Direct
{
    interface IAction
    {
        Response ExecuteLoad(Request request);
        Response ExecuteCRUD(Request request, List<Dictionary<string, string>> dataataList);
        Response ExecuteCreate(Request request);
        Response ExecuteUpdate(Request request, List<Dictionary<string, string>> dataataList);
        Response ExecuteDelete(Request request);
        Response ExecuteSave(Request request);
        Response ExecuteForm(HttpRequest httpRequest);
        Response ExecuteNormalAction(Request request);
    }
}
