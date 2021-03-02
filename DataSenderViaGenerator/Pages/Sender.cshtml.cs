using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DataSenderViaGenerator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DataSenderViaGenerator.Pages
{
    public class SenderModel : PageModel
    {
        [BindProperty]
        public SenderSettings SenderSettings { get; set; }

        public void OnGet()
        {
            SenderSettings = new SenderSettings()
            {
                Url = @"https://hetzner1.k-matika.ru:49004",
                ServiceName = "KmkContactService",
                EndPoint = "Contacts",
                Username = "Supervisor",
                Password = "Supervisor"
            };
        }

        public IActionResult OnPostPregenerated(string data)
        {
            SenderSettings = new SenderSettings();
            SenderSettings.Data = data;
            return Page();
        }

        public void OnPost()
        {
            if (SenderSettings.Url[SenderSettings.Url.Length - 1] == '/')
            {
                SenderSettings.Url = SenderSettings.Url.Remove(SenderSettings.Url.Length - 1);
            }

            try
            {
                Authorize();
            }
            catch(Exception e)
            {

            }

        }

        public async void OnPostSender(string json)
        {
        }

        private CookieContainer AuthCookie { get; set; } = new CookieContainer();

        private string _authUrl;

        private string Authorize()
        {
            string result;

            _authUrl = $"{SenderSettings.Url}/ServiceModel/AuthService.svc/Login";
         
            string body = "{\"UserName\": \"" + SenderSettings.Username + "\",\"UserPassword\": \""+ SenderSettings.Password +"\"}";

            HttpWebRequest request = WebRequest.CreateHttp(_authUrl);
            request.Method = "POST";
            request.CookieContainer = AuthCookie;

            request.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(body);
            }


            try
            {
                using (var response = request.GetResponse() as HttpWebResponse)
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                result = $"Ошибка авторизации на приложении Creatio: {e.Message}";
                throw;
            }
            return result;
        }

        public string CallService(string data)
        {
            string result;
            string requestUrl = $"{SenderSettings.Url}/0/rest/{SenderSettings.ServiceName}/{SenderSettings.EndPoint}";

            HttpWebRequest request = WebRequest.CreateHttp(requestUrl);
            request.Method = "POST";
            request.CookieContainer = AuthCookie;
            request.ContentType = "application/json";
            request.Timeout = int.MaxValue;

            var bpmcsrf = AuthCookie.GetCookies(new Uri(_authUrl))["BPMCSRF"].Value;
            request.Headers.Add("BPMCSRF", bpmcsrf);

            //var sw = new Stopwatch();
            //sw.Start();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            try
            {
                using (var response = request.GetResponse() as HttpWebResponse)
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                result = $"Ошибка при выполнении запроса: {e.Message}";
                throw;
            }

            //sw.Stop();

            return result;
        }

    }
}

