using Npgsql;
using Npgsql.Replication.PgOutput.Messages;
using www1.Controllers;

namespace www1.Components;

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// <summary>
///
///
/// 
/// </summary>
public abstract class DbHandler
{
    public abstract void LogRequest(HttpContext context);

    public abstract void LogLlm(string sid, DateTime ts0, string qpQuery, Q1Response q1Response);
    public abstract void TryUpdate();
}

public class DbInstance
{
    public static DbHandler Create()
    {
        return new NullDbHandler();
    }
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// <summary>
///
///
/// 
/// </summary>
public class NullDbHandler : DbHandler
{
    public override void LogRequest(HttpContext context)
    {
        ;
    }

    public override void LogLlm(string sid, DateTime ts0, string qpQuery, Q1Response q1Response)
    {
        ;
    }

    public override void TryUpdate()
    {
        ;
    }
}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// <summary>
///
///
/// 
/// </summary>
public class NpgsqlDbHandler : DbHandler, IDisposable
{
    public override void LogRequest(HttpContext context)
    {
        using (var cmd = new NpgsqlCommand("insert into httplog values (@ts, @rh, @q, @sid)", _Conn))
        {
            cmd.Parameters.AddWithValue("@ts", DateTime.Now);
            cmd.Parameters.AddWithValue("@rh", context.Connection.RemoteIpAddress.ToString());
            cmd.Parameters.AddWithValue("@q", context.Request.Path.ToString());
            cmd.Parameters.AddWithValue("@sid", context.Session.Id);
            cmd.ExecuteNonQuery();
        }
    }

    public override void LogLlm(string sid, DateTime ts0, string qpQuery, Q1Response q1Response)
    {
        using (var cmd = new NpgsqlCommand("insert into llmlog values (@ts, @sid, @t0, @t1, @q, @t2, @llmid, @r)", _Conn))
        {
            cmd.Parameters.AddWithValue("@ts", DateTime.Now);
            cmd.Parameters.AddWithValue("@sid", sid);
            cmd.Parameters.AddWithValue("@t0", ts0);
            cmd.Parameters.AddWithValue("@t1", q1Response.ts1);
            cmd.Parameters.AddWithValue("@t2", q1Response.ts2);
            cmd.Parameters.AddWithValue("@q", qpQuery);
            cmd.Parameters.AddWithValue("@llmid", q1Response.llmid);
            cmd.Parameters.AddWithValue("@r", q1Response.llmmessage);
            cmd.ExecuteNonQuery();
        }
    }

    //==============================================================================================================================
    /// <summary>
    /// 
    /// </summary>
    private NpgsqlConnection _Conn;

    private NpgsqlDbHandler(string localhost)
    {
        NpgsqlConnectionStringBuilder b = new()
        {
            Host = "localhost",
            Username = "postgres",
            Database = "kione"
        };
        _Conn = new NpgsqlConnection(b.ConnectionString);
        _Conn.Open();
    }

    public static DbHandler Create()
    {
        DbHandler h = new NpgsqlDbHandler("localhost");
        h.TryUpdate();
        return h;
    }


    public void Dispose()
    {
        try
        {
            _Conn.Close();
        }
        finally
        {
            _Conn = null;
        }
    }


    //private string CurrentVersion = "1.0.0.2";

    /// <summary>
    /// 
    /// </summary>
    public override void TryUpdate()
    {
        string v = null;
        using (var cmd = new NpgsqlCommand("select * from information_schema.tables where table_schema = 'public' and table_name = '_version'", _Conn)) v = (string?) cmd.ExecuteScalar();
        if (string.IsNullOrWhiteSpace(v))
        {
            Update(null);
        }
        else
        {
            v = (string) sq("select v from _version");
            Update(v);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="vfrom"></param>
    private void Update(string vfrom)
    {
        if (string.IsNullOrWhiteSpace(vfrom))
        {
            nq(@"create table httplog
(
    ts timestamp,
    rh text,
    q  varchar(255)
)");
            ;
            //
            using (var cmd = new NpgsqlCommand(@"create table _version(v varchar)", _Conn)) cmd.ExecuteNonQuery();
            using (var cmd = new NpgsqlCommand(@"insert into _version values('1.0.0.1')", _Conn)) cmd.ExecuteNonQuery();
            vfrom = "1.0.0.1";
        }

        //
        if (vfrom.CompareTo("1.0.0.2") < 0)
        {
            nq("alter table httplog add sid text");
            nq("update _version set v = '1.0.0.2'");
            vfrom = "1.0.0.2";
        }

        //
        if (vfrom.CompareTo("1.0.0.3") < 0)
        {
            nq(@"create table llmlog
(
    ts    timestamp,
    sid   text,
    ts0   timestamp,
    ts1   timestamp,
    q     text,
    ts2   timestamp,
    llmid text,
    r     text
)");
            nq("update _version set v = '1.0.0.3'");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stmt"></param>
    private void nq(string stmt)
    {
        using (var cmd = new NpgsqlCommand(stmt, _Conn)) cmd.ExecuteNonQuery();
    }

    private object sq(string stmt)
    {
        object o = null;
        using (var cmd = new NpgsqlCommand(stmt, _Conn)) o = cmd.ExecuteScalar();
        return o;
    }
}