using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
//using System.Data.SqlClient; //notar que se incluyen las clases de este namespace

public class DatabaseScript : MonoBehaviour
{
    [SerializeField] string status;
    
    public void Access()
    {
        string conn = @"Data Source = 127.0.0.1;Database=DemoUsandoCS;User Id=newuser; Password = newuser";
        //string conn = @"Server=localhost\SQLExpress;Database=DemoUsandoCS;Trusted_Connection=True;";   

        using (SqlConnection connection = new SqlConnection(conn))   //esta sentencia permite que connection se destruya al salir de bloque , no hay que usar connection.close() en otras palabras
        { //https://stackoverflow.com/questions/4717789/in-a-using-block-is-a-sqlconnection-closed-on-return-or-exception
            print("Connecting to SQL Server ... ");
            connection.Open();
            print("Done.");

            string sql = "INSERT INTO TablaDemo(Nombre,Matricula) VALUES('CANACOEXPRESS', 'P50969')";//nunca van a hacer esto
            string sql2 = "SELECT * FROM TablaDemo"; //esto tampoco

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.ExecuteNonQuery();
                print("Done.");
            }

            using (SqlCommand command = new SqlCommand(sql2, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //print("{0} {1} {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                    }
                }
            }
        }

    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
       

    }
}
