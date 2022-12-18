using Npgsql;

public interface IDBConnection
{
    NpgsqlConnection GetConnection();
}

public class DBConnection : IDBConnection
{
    public NpgsqlConnection con = new NpgsqlConnection(
        "Host=localhost;Username=postgres;Password=root;Database=FinalProject"
    );

    NpgsqlConnection IDBConnection.GetConnection()
    {
        return con;
    }
}
