﻿using System;
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

        [WebMethod(Description = "전체 요청 목록 조회")]
        public DataSet  requestList()
        {          
           
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
                    adapter.Fill(ds, "todolist");
                    return (ds);
                }
          }          
        }
        [WebMethod(Description = "해당 email에 대한 요청 목록 조회")] 
        public DataSet requestListbyEmail(string _email)
        {
            string email = checkInputData(_email);

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                DataSet ds = new DataSet();

                String query =  string.Format("SELECT [AutoID]  ,[RequestTime]," +
                    "[RequestWHO_IP] ,[RequestWHO_email],[RequestWHO_name] ,[Request_Data]  ,[Output_Status]  ," +
                    "[Output_CompletedTime]  ,[Output_Data]  ,[Output_Path],[remark] ,[Output_Route]" +
                    "FROM[trip].[dbo].[ToDO] where requestwho_email ='{0}'", email);

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
                {
                    adapter.Fill(ds, "todolist");
                    return (ds);
                }
            }
        }

        [WebMethod(Description = "경로 요청 입력")]        
        public string requestTravelRoute(string ip, string email, string name, string data)
        {
            string requestwho_ip = checkInputData ( ip );
            string requestwho_email = checkInputData(email );
            string requestwho_name = checkInputData(name);
            string request_data = checkInputData(data);

            string _query = "INSERT INTO [ToDO] ( requestwho_ip, requestwho_email, requestwho_name, request_data) " +
            "values ( @requestwho_ip, @requestwho_email, @requestwho_name, @request_data )";
            
            using (SqlConnection conn = new SqlConnection(conString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;
                    comm.Parameters.AddWithValue("@requestwho_ip", requestwho_ip);
                    comm.Parameters.AddWithValue("@requestwho_email", requestwho_email);
                    comm.Parameters.AddWithValue("@requestwho_name", requestwho_name);
                    comm.Parameters.AddWithValue("@request_data", request_data);
                     
                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                        return "OK";                        
                    }
                    catch (SqlException ex)
                    {
                        // throw ex;
                        return ex.Message; 
                    }
                    catch ( Exception ex)
                    {
                        return ex.Message;
                    }
                }
            }
        }
        public static string checkInputData(string _inputValue)
        {
            _inputValue = _inputValue.Replace("'", "");
            _inputValue = _inputValue.Replace("--", "");
            _inputValue = _inputValue.Replace("--, #", " ");
            _inputValue = _inputValue.Replace("/* */", " ");
            _inputValue = _inputValue.Replace("' or 1=1--", " ");
            _inputValue = _inputValue.Replace("union", " ");
            _inputValue = _inputValue.Replace("select", " ");
            _inputValue = _inputValue.Replace("delete", " ");
            _inputValue = _inputValue.Replace("insert", " ");
            _inputValue = _inputValue.Replace("update", " ");
            _inputValue = _inputValue.Replace("drop", " ");
            _inputValue = _inputValue.Replace("on error resume", " ");
            _inputValue = _inputValue.Replace("execute", " ");
            _inputValue = _inputValue.Replace("windows", " ");
            _inputValue = _inputValue.Replace("boot", " ");
            _inputValue = _inputValue.Replace("-1 or", " ");
            _inputValue = _inputValue.Replace("-1' or", " ");
            _inputValue = _inputValue.Replace("../", " ");
            _inputValue = _inputValue.Replace("unexisting", " ");
            _inputValue = _inputValue.Replace("win.ini", " ");
            return _inputValue;
        }
    }
}
