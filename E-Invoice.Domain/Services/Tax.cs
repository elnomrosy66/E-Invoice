using E_Invoice.Domain.Helpers;
using E_Invoice.Domain.Interfaces;
using E_Invoice.Domain.Models;
using E_Invoice.Domain.Models.EInvoiceModels;
using E_Invoice.Domain.Models.EInvoiceModels.ResponceModels;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace E_Invoice.Domain.Services
{
    public class Tax
    {
        public IUnitOfWork _unitOfWork;
        JsonSerializerSettings jss = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
            FloatFormatHandling = FloatFormatHandling.String,
            FloatParseHandling = FloatParseHandling.Decimal,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateParseHandling = DateParseHandling.None
        };
        public Access GetAccess()
        {

            RestClient client;
            if (Info._setting.ProdEnv == true)
            {
                client = new RestClient("https://id.eta.gov.eg/connect/token");
            }
            else
            {
                client = new RestClient("https://id.preprod.eta.gov.eg/connect/token");
            }
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", Info._setting.ClientId);
            request.AddParameter("client_secret", Info._setting.ClientSecret);
            request.AddParameter("scope", "InvoicingAPI");
            IRestResponse response = client.Execute(request);
            Access access = new Access();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                access = JsonConvert.DeserializeObject<Access>(response.Content);
                return access;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                access.AccessError = "خطاء في بيانات الدخول تحقق من client_id & client_secret";
                return access;
            }
            access.AccessError = "error login2";
            return access;
        }

        public docsummary GetRecentDocuments(int page, int size)
        {

            RestClient client;
            if (Info._setting.ProdEnv == true)
                client = new RestClient("https://api.invoicing.eta.gov.eg/api/v1.0/documents/recent?pageNo=" + page + "&pageSize=" + size);
            
            else
                client = new RestClient("https://api.preprod.invoicing.eta.gov.eg/api/v1.0/documents/recent?pageNo=" + page + "&pageSize=" + size);

            client.Timeout = 9000000;
            var request = new RestRequest(Method.GET);

            request.AddHeader("Authorization", "Bearer " + GetAccess().access_token);
            IRestResponse response = client.Execute(request);
            docsummary docs = new docsummary();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                docs = JsonConvert.DeserializeObject<docsummary>(response.Content);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.GatewayTimeout)
            {
                docs.error = "الخادم لا يستجيب الان الرجاء المحاولة في وقت لاحق";
                return docs;
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                docs.error = "غير مصرح لك بالدخول";
                return docs;
            }

            return docs;
        } 
        public GetDocumentResponse GetDocument(string  UUID)
        {
            RestClient client;
            if (Info._setting.ProdEnv == true)
                client = new RestClient("https://api.invoicing.eta.gov.eg/api/v1/documents/"+ UUID + "/raw");
            else
                client = new RestClient("https://api.preprod.invoicing.eta.gov.eg/api/v1/documents/" + UUID + "/raw");

            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + GetAccess().access_token);
            IRestResponse response = client.Execute(request);
            GetDocumentResponse docs = new GetDocumentResponse();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
               return docs = JsonConvert.DeserializeObject<GetDocumentResponse>(response.Content);
            }
            return docs;    
        }
        public ErrorResponce CancleDocument (string UUID , string reason)
        {
            var client = new RestClient("https://api.preprod.invoicing.eta.gov.eg/api/v1.0/documents/state/"+UUID+"/state");
            if (Info._setting.ProdEnv == true)
            {
                client = new RestClient("https://api.invoicing.eta.gov.eg/api/v1.0/documents/state/" + UUID + "/state");
            }
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer "+GetAccess().access_token);
            cancledoc cd = new cancledoc();
            cd.status = "cancelled";
            cd.reason = reason;
            string jsonInvoice = JsonConvert.SerializeObject(cd, jss);
            request.AddParameter("application/json", jsonInvoice, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ErrorResponce erro = new ErrorResponce();
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                erro = JsonConvert.DeserializeObject<ErrorResponce>(response.Content);
            
            return erro;
           
        }
        public ErrorResponce RejectDocument( string UUID , string reason)
        {
            var client = new RestClient("https://api.preprod.invoicing.eta.gov.eg/api/v1.0/documents/state/" + UUID + "/state");
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer {{generatedAccessToken}}");
            cancledoc cd = new cancledoc();
            cd.status = "rejected";
            cd.reason = reason;
            string jsonInvoice = JsonConvert.SerializeObject(cd, jss);
            request.AddParameter("application/json", jsonInvoice, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ErrorResponce erro = new ErrorResponce();
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                erro = JsonConvert.DeserializeObject<ErrorResponce>(response.Content);

            return erro;
        }

        public PackageRequestResponse RequestPakage(PackageRequest packageRequest)
        {
            var client = new RestClient("https://api.preprod.invoicing.eta.gov.eg/api/v1/documentPackages/requests");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer "+GetAccess().access_token);
            request.AddHeader("Content-Type", "application/json");
            string body = JsonConvert.SerializeObject(packageRequest, jss);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            PackageRequestResponse respo = new PackageRequestResponse();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                respo  = JsonConvert.DeserializeObject<PackageRequestResponse>(response.Content);
            }
            else
            {
                respo = JsonConvert.DeserializeObject<PackageRequestResponse>(response.Content);
            }
            return respo;
        }

        public PackageRequests GetPackageRequests()
        {
            RestClient client;
            if (Info._setting.ProdEnv == true)
            {
                client = new RestClient("https://api.invoicing.eta.gov.eg/api/v1/documentPackages/requests?pageSize=100&pageNo=1");

            }
            else
            {
                client = new RestClient("https://api.preprod.invoicing.eta.gov.eg/api/v1/documentPackages/requests?pageSize=100&pageNo=1");

            }
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer "+GetAccess().access_token);
            
            IRestResponse response = client.Execute(request);
            PackageRequests package = new PackageRequests();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                package = JsonConvert.DeserializeObject<PackageRequests>(response.Content);
                return package;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return package;
            }
            //access.AccessError = "error login2";
            return package;

        }

        public void PrintInvoice (string UUID)
        {
            RestClient client;
            if (Info._setting.ProdEnv)
            {
                client = new RestClient("https://api.invoicing.eta.gov.eg/api/v1/documents/" + UUID + "/pdf");
            }
            else
            {
                client = new RestClient("https://api.preprod.invoicing.eta.gov.eg/api/v1/documents/" + UUID + "/pdf");
            }

            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + GetAccess().access_token);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
               File.WriteAllBytes ("4.pdf",response.RawBytes);

            }
        }

        public DownloadPackageResponse DownloadPackage(string rid)
        {
            RestClient client;
            if (Info._setting.ProdEnv)
            {
                client = new RestClient("https://api.invoicing.eta.gov.eg/api/v1/documentPackages/" + rid);
            }
            else
            {
                client = new RestClient("https://api.preprod.invoicing.eta.gov.eg/api/v1/documentPackages/" +rid);
            }

            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + GetAccess().access_token);
            IRestResponse response = client.Execute(request);
            DownloadPackageResponse downloadPackageResponse = new DownloadPackageResponse();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //File.WriteAllBytes("4.zip", response.RawBytes);
                downloadPackageResponse.zip = response.RawBytes;
            }
            else
                downloadPackageResponse = JsonConvert.DeserializeObject<DownloadPackageResponse>(response.Content);

            return downloadPackageResponse;
        }

    }


    public class Access
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }
        public string AccessError { get; set; }
    }

    public class badrec
    {
        public string erroe { get; set; }
        public string error_description { get; set; }
        public string error_uri { get; set; }
    }

    public class docsummary
    {
        public List<result> result { get; set; }
        public metadata metadata { get; set; }
        public string error { get; set; }
    }

    public class result
    {
        public string publicUrl { get; set; }
        public string uuid { get; set; }
        public string submissionUUID { get; set; }
        public string longId { get; set; }
        public string internalId { get; set; }
        public string typeName { get; set; }
        public string typeVersionName { get; set; }
        public string issuerId { get; set; }
        public string issuerName { get; set; }
        public string receiverId { get; set; }
        public string receiverName { get; set; }
        public DateTime dateTimeIssued { get; set; }
        public DateTime dateTimeReceived { get; set; }
        public decimal totalSales { get; set; }
        public decimal totalDiscount { get; set; }
        public decimal netAmount { get; set; }
        public decimal total { get; set; }
        public string status { get; set; }
        public string cancelRequestDate { get; set; }
        public string rejectRequestDate { get; set; }
        public string cancelRequestDelayedDate { get; set; }
        public string rejectRequestDelayedDate { get; set; }
        public string declineCancelRequestDate { get; set; }
        public string declineRejectRequestDate { get; set; }
    }

    public class metadata
    {
        public int totalPages { get; set; }
        public int totalCount { get; set; }
    }
}
