using System.Net;
using System.Text;
using Newtonsoft.Json;
using www1.Components;
using www1.Models;

namespace www1;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(30); });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        if(false){
            app.UseSession();
            app.Use(async (context, next) =>
            {
                DbHandler dbh = DbInstance.Create();
                dbh.LogRequest(context);
                await next();
            });
            app.Use(async (context, next) =>
            {
                context.Session.Set("S", Encoding.UTF8.GetBytes("1"));
                var url = context.Request.Path.Value;
                int? userid = context.Session.GetInt32("userid");
                var rq = context.Request;
                if (userid == null || userid < 1)
                {
                    userid = 0;
                    var username = "";
                    var ppp = "";
                    if (rq.HasFormContentType)
                    {
                        try
                        {
                            username = context.Request.Form["username"];
                        }
                        catch (Exception e)
                        {
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(username))
                    {
                        try
                        {
                            ppp = context.Request.Form["password"];
                            int uid = UserHandler.GetUserId(username, ppp);
                            if (uid > 0)
                            {
                                context.Session.SetInt32("userid", uid);
                                userid = uid;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }

                if (url.ToLower().Contains("logout"))
                {
                    userid = 0;
                    context.Session.SetInt32("userid", 0);
                }

                if (userid < 1) context.Request.Path = "/Login";
                ;
                await next();
            });
        }

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}