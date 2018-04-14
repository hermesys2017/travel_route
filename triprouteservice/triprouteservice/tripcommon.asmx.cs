using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace triprouteservice
{
    /// <summary>
    /// Summary description for tripcommon
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]

    


    public class tripcommon : System.Web.Services.WebService
    {

    
        string conString = "Data Source=Web;Initial Catalog=trip;Integrated Security=False;User Id=tripadmin;Password=apriltrip;";
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public DataSet  requestList()
        {          
            DataTable dt = null;
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                DataSet ds = new DataSet();
               
                String query = "SELECT [AutoID]  ,[RequestTime]," +
                    "[RequestWHO_IP] ,[RequestWHO_email],[RequestWHO_name] ,[Request_Data]  ,[Output_Status]  ," +
                    "[Output_CompletedTime]  ,[Output_Data]  ,[Output_Path],[remark] ,[Output_Route]" +
                    "FROM[trip].[dbo].[ToDO] " ;
                    
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
                {  
                    adapter.Fill(ds, "list");
                    return (ds);
                }
          }          
        }
        [WebMethod]
        public string requestTravleRoute()
        {
            string _query = "INSERT INTO [request_list] (req_email, req_type_code, req_type_title," +
           "  req_servicetype_code, req_servicetype_title, upload_path, req_result) values (@req_email, @req_type_code, @req_type_title," +
           " @req_servicetype_code, @req_servicetype_title , @upload_path, @req_result)";

            SqlConnection conn = new SqlConnection(conString);
            if (conn.State == ConnectionState.Open)
                return "connected";
            else
                return "disconnected";

            /*
              using (SqlConnection conn = new SqlConnection(conString))
              {
                  if (conn.State == ConnectionState.Open)
                      return "connected";
                  else
                      return "disconnected";

                  using (SqlCommand comm = new SqlCommand())
                  {
                      comm.Connection = conn;
                      comm.CommandType = CommandType.Text;
                      comm.CommandText = _query;
                      comm.Parameters.AddWithValue("@req_email", req_email);
                      comm.Parameters.AddWithValue("@req_type_code", req_type_code);
                      comm.Parameters.AddWithValue("@req_type_title", req_type_title);
                      comm.Parameters.AddWithValue("@req_servicetype_code", req_servicetype_code);
                      comm.Parameters.AddWithValue("@req_servicetype_title", req_servicetype_title);
                      comm.Parameters.AddWithValue("@upload_path", uploadPath);
                      comm.Parameters.AddWithValue("@req_result", "N");
                      try
                      {
                          conn.Open();
                          comm.ExecuteNonQuery();
                      }
                      catch (SqlException ex)
                      {
                          throw ex;
                      }
                  }
                  @RequestTime
, @RequestWHO_IP
, @RequestWHO_email
, @RequestWHO_name
, @Request_Data
, @Output_Status
, @Output_CompletedTime
, @Output_Data
, @Output_Path
, @remark
, @Output_Route

            requesttime, request

RequestTime
RequestWHO_IP
RequestWHO_email
RequestWHO_name
Request_Data
Output_Status
Output_CompletedTime
Output_Data
Output_Path
remark
Output_Route

              
            }
            */
        }
    }
}
