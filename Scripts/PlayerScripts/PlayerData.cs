using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class PlayerData : MonoBehaviour {
    private LifeManager lifeSystem;
    public GameObject text;
    string conn = @"Data Source = 127.0.0.1;Database=DemoUsandoCS;User Id=newuser; Password = newuser";  //data source = localhost;

    public int ammo = 300;
    public int pickHealth = 0;
    public int pickJump = 0;
    public int pickPower = 0;
    public int pickLife = 0;
    public int lives;
    public int jumps = 0;
    public int deaths = 0;
    public int pickups = 0;
    public int kills = 0;
    public int archerKills = 0;
    public int gruntKills = 0;
    DateTime date = DateTime.Now;
    public int score = 100;
    public string clear = "NotCleared";

    public void pHAdd()
    {
        pickHealth++;
        score+=5;
        pickUp();
    }

    public void pJAdd()
    {
        pickJump++;
        score+=5;
        pickUp();
    }

    public void pLAdd()
    {
        pickLife++;
        score+=5;
        pickUp();
    }
    public void pPAdd()
    {
        pickPower++;
        score += 5;
        pickUp();
    }

    public void ammoUse()
    {
        ammo--;
        score--;
    }

    public void jumpUse()
    {
        jumps++;
        score--;
    }

    public void playerDeath()
    {
        deaths++;
        score-=10;
    }

    public void lAdd()
    {
        lives = lifeSystem.lifeCounter;
        
    }

    public void pickUp()
    {
        pickups++;
    }
    
    public void killAdd()
    {
        kills++;
        score += 25;
    }

    public void arcKill()
    {
        archerKills++;
        killAdd();
    }

    public void grKill()
    {
        gruntKills++;
        killAdd();
    }
    public void EnemySet()
    {
        using (SqlConnection connection = new SqlConnection(conn))   //esta sentencia permite que connection se destruya al salir de bloque , no hay que usar connection.close() en otras palabras
        {
            connection.Open();
            Console.WriteLine("Done.");

            using (SqlCommand command = new SqlCommand("dbo.EnemyInsert", connection))
            { //https://stackoverflow.com/questions/293311/whats-the-best-method-to-pass-parameters-to-sqlcommand
                command.CommandType = System.Data.CommandType.StoredProcedure; //si no le ponen esto no funciona
                if (gruntKills > archerKills)
                {
                    command.Parameters.Add("@Type", SqlDbType.NChar).Value = "Grunt";
                    command.Parameters.Add("@Killed", SqlDbType.Int).Value = gruntKills;
                }
                else if (archerKills > gruntKills)
                {
                    command.Parameters.Add("@Type", SqlDbType.NChar).Value = "Archer";
                    command.Parameters.Add("@Killed", SqlDbType.Int).Value = archerKills;
                }
                else
                {
                    command.Parameters.Add("@Type", SqlDbType.NChar).Value = "Both";
                    command.Parameters.Add("@Killed", SqlDbType.Int).Value = gruntKills+archerKills;
                }
                command.ExecuteNonQuery();
                print("Lista escritura con un SP.");
            }

        }
    }
    public void ScoreSet(int score)
    {
        using (SqlConnection connection = new SqlConnection(conn))   //esta sentencia permite que connection se destruya al salir de bloque , no hay que usar connection.close() en otras palabras
        {
            connection.Open();
            Console.WriteLine("Done.");

            using (SqlCommand command = new SqlCommand("dbo.ScoreSet", connection))
            { //https://stackoverflow.com/questions/293311/whats-the-best-method-to-pass-parameters-to-sqlcommand
                command.CommandType = System.Data.CommandType.StoredProcedure; //si no le ponen esto no funciona
                
                command.Parameters.Add("@Total_Score", SqlDbType.Int).Value = score;

                command.ExecuteNonQuery();
                print("Lista escritura con un SP.");
            }
        }
    }
    public void PEset()
    {
        using (SqlConnection connection = new SqlConnection(conn))   //esta sentencia permite que connection se destruya al salir de bloque , no hay que usar connection.close() en otras palabras
        {
            connection.Open();
            Console.WriteLine("Done.");

            using (SqlCommand command = new SqlCommand("dbo.PlayerEnemyInsert", connection))
            { //https://stackoverflow.com/questions/293311/whats-the-best-method-to-pass-parameters-to-sqlcommand
                command.CommandType = System.Data.CommandType.StoredProcedure; //si no le ponen esto no funciona

                command.Parameters.Add("@Type", SqlDbType.NChar).Value = "Grunt";
                command.Parameters.Add("@Killed", SqlDbType.Int).Value = gruntKills;
                command.ExecuteNonQuery();

                command.Parameters.Add("@Type", SqlDbType.NChar).Value = "Archer";
                command.Parameters.Add("@Killed", SqlDbType.Int).Value = archerKills;                
                command.ExecuteNonQuery();
                print("Lista escritura con un SP.");
            }
        }
    }
    public void PLSSet(int score)
    {
        using (SqlConnection connection = new SqlConnection(conn))   //esta sentencia permite que connection se destruya al salir de bloque , no hay que usar connection.close() en otras palabras
        {
            connection.Open();
            Console.WriteLine("Done.");

            using (SqlCommand command = new SqlCommand("dbo.PlayerLevelScoreInsert", connection))
            { //https://stackoverflow.com/questions/293311/whats-the-best-method-to-pass-parameters-to-sqlcommand
                command.CommandType = System.Data.CommandType.StoredProcedure; //si no le ponen esto no funciona

                command.Parameters.Add("@Score", SqlDbType.Int).Value = score;
                command.ExecuteNonQuery();
                print("Lista escritura con un SP.");
            }
        }
    }
    public void LevelInit(string clear)
    {
        
        
        using (SqlConnection connection = new SqlConnection(conn))   //esta sentencia permite que connection se destruya al salir de bloque , no hay que usar connection.close() en otras palabras
        {
            connection.Open();
            Console.WriteLine("Done.");

            using (SqlCommand command = new SqlCommand("dbo.LevelInsert", connection))
            { //https://stackoverflow.com/questions/293311/whats-the-best-method-to-pass-parameters-to-sqlcommand
                command.CommandType = System.Data.CommandType.StoredProcedure; //si no le ponen esto no funciona

                //command.Parameters.Add("@ID", SqlDbType.Int).Value = "1";
                command.Parameters.Add("@Checkpoints", SqlDbType.Int).Value = "3";
                command.Parameters.Add("@Cleared",SqlDbType.NChar).Value=clear;
                command.Parameters.Add("@Enemies", SqlDbType.Int).Value = "2";

                command.ExecuteNonQuery();
                print("Lista escritura con un SP.");
            }
        }


    }

	// Use this for initialization
	void Start () {
        lifeSystem = text.GetComponent<LifeManager>();
        PlayerInsert();
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public void PlayerInsert()
    {

        //string conn = @"Server=localhost\SQLExpress;Database=DemoUsandoCS;Trusted_Connection=True;";   

        using (SqlConnection connection = new SqlConnection(conn))   //esta sentencia permite que connection se destruya al salir de bloque , no hay que usar connection.close() en otras palabras
        {
            connection.Open();
            Console.WriteLine("Done.");


            using (SqlCommand command = new SqlCommand("dbo.PlayerQuery", connection))
            { //https://stackoverflow.com/questions/293311/whats-the-best-method-to-pass-parameters-to-sqlcommand
                command.CommandType = System.Data.CommandType.StoredProcedure; //si no le ponen esto no funciona

                command.Parameters.Add("@Ammo", SqlDbType.Int).Value = "200";
                command.Parameters.Add("@Lives", SqlDbType.Int).Value = "3";


                command.ExecuteNonQuery();
                print("Lista escritura con un SP.");
            }
        }

    }


}



