using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineSweeper1.Models;
using MineSweeper1.DAL.SQLHelper;
using System.Data.SqlClient;

namespace MineSweeper1.DAL
{
    public class PlayerService
    {
        private SQLHelper.SQLHelper sqlHelper = new SQLHelper.SQLHelper();
        public int AddPlayer(Player player,string difficulty)
        {          
            string sql = $"insert into {difficulty} (PlayerName,score,GameDaT)";
            sql += $" Values('{player.PlayerName}',{player.Score},'{player.GameDate}')";
            return Convert.ToInt32(SQLHelper.SQLHelper.GetSingleResult(sql));
        }
        public List<Player> QueryPlayer(string difficulty)
        {
            string sql = $"select PlayerName,Score,GameDaT from {difficulty}";
            SqlDataReader reader = SQLHelper.SQLHelper.GetReader(sql);
            List<Player> players = new List<Player>();
            while (reader.Read())
            {
                players.Add(new Player()
                {
                    PlayerName = reader["PlayerName"].ToString(),
                    Score = Convert.ToInt32(reader["Score"]),
                    GameDate = Convert.ToDateTime(reader["GameDaT"]).ToString("yyyy-MM-dd:hh:mm:ss")
                });
            }
            reader.Close(); 
            return players;
        }
    }
}
