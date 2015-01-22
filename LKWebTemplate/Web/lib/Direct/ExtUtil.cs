namespace ExtDirect.Direct
{
    internal static class Util
    {
        internal static bool IsSpecialField(this Request req)
        {
          
            try
            {
                string data = req.data[0].ToString();
                if (data.Contains("{") || data.Contains("["))
                {
                    return true;
                }
                else
                {
                    data = req.data[1].ToString();
                    if (data.Contains("{") || data.Contains("["))
                    {
                        return true;
                    }
                    return false;
                }
                
            }
            catch
            {
                return false;
            }
        }
        internal static bool hasData(this Request req)
        {
            try
            {
                var len = req.data.Count;
                if(len>0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
           
        }
        internal static bool IsNullData(this Request req)
        {
            try
            {
                var data = req.data[0].ToString();
                if (data.Length > 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return true;
            }

        }
        internal static bool IsUpdateRequest(this Request req)
        {
            try
            {
                var len = req.data.Count;
                if (len == 2)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
            
    }
}
