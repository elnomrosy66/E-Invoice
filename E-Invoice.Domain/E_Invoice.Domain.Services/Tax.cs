using System;
using System.Net;
using E_Invoice.Domain.Helpers;
using E_Invoice.Domain.Interfaces;
using E_Invoice.Domain.Models;
using E_Invoice.Domain.Models.EInvoiceModels;
using E_Invoice.Domain.Models.EInvoiceModels.ResponceModels;
using Newtonsoft.Json;
using RestSharp;

namespace E_Invoice.Domain.Services;

public class Tax
{
	public IUnitOfWork _unitOfWork;

	private JsonSerializerSettings jss = new JsonSerializerSettings
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
		RestClient restClient = ((!Info._setting.ProdEnv) ? new RestClient("https://id.preprod.eta.gov.eg/connect/token") : new RestClient("https://id.eta.gov.eg/connect/token"));
		restClient.Timeout = -1;
		RestRequest restRequest = new RestRequest(Method.POST);
		restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
		restRequest.AddParameter("grant_type", "client_credentials");
		restRequest.AddParameter("client_id", Info._setting.ClientId);
		restRequest.AddParameter("client_secret", Info._setting.ClientSecret);
		restRequest.AddParameter("scope", "InvoicingAPI");
		IRestResponse restResponse = restClient.Execute(restRequest);
		Access access = new Access();
		if (restResponse.StatusCode == HttpStatusCode.OK)
		{
			return JsonConvert.DeserializeObject<Access>(restResponse.Content);
		}
		if (restResponse.StatusCode == HttpStatusCode.BadRequest)
		{
			access.AccessError = "خطاء في بيانات الدخول تحقق من client_id & client_secret";
			return access;
		}
		access.AccessError = "error login2";
		return access;
	}

	public docsummary GetRecentDocuments(int page, int size)
	{
		RestClient restClient = ((!Info._setting.ProdEnv) ? new RestClient("https://api.preprod.invoicing.eta.gov.eg/api/v1.0/documents/recent?pageNo=" + page + "&pageSize=" + size) : new RestClient("https://api.invoicing.eta.gov.eg/api/v1.0/documents/recent?pageNo=" + page + "&pageSize=" + size));
		restClient.Timeout = 9000000;
		RestRequest restRequest = new RestRequest(Method.GET);
		restRequest.AddHeader("Authorization", "Bearer " + GetAccess().access_token);
		IRestResponse restResponse = restClient.Execute(restRequest);
		docsummary docsummary2 = new docsummary();
		if (restResponse.StatusCode == HttpStatusCode.OK)
		{
			docsummary2 = JsonConvert.DeserializeObject<docsummary>(restResponse.Content);
		}
		if (restResponse.StatusCode == HttpStatusCode.GatewayTimeout)
		{
			docsummary2.error = "الخادم لا يستجيب الان الرجاء المحاولة في وقت لاحق";
			return docsummary2;
		}
		if (restResponse.StatusCode == HttpStatusCode.Unauthorized)
		{
			docsummary2.error = "غير مصرح لك بالدخول";
			return docsummary2;
		}
		return docsummary2;
	}

	public GetDocumentResponse GetDocument(string UUID)
	{
		RestClient restClient = ((!Info._setting.ProdEnv) ? new RestClient("https://api.preprod.invoicing.eta.gov.eg/api/v1/documents/" + UUID + "/raw") : new RestClient("https://api.invoicing.eta.gov.eg/api/v1/documents/" + UUID + "/raw"));
		RestRequest restRequest = new RestRequest(Method.GET);
		restRequest.AddHeader("Authorization", "Bearer " + GetAccess().access_token);
		IRestResponse restResponse = restClient.Execute(restRequest);
		GetDocumentResponse getDocumentResponse = new GetDocumentResponse();
		if (restResponse.StatusCode == HttpStatusCode.OK)
		{
			return getDocumentResponse = JsonConvert.DeserializeObject<GetDocumentResponse>(restResponse.Content);
		}
		return getDocumentResponse;
	}

	public ErrorResponce CancleDocument(string UUID, string reason)
	{
		RestClient restClient = new RestClient("https://api.preprod.invoicing.eta.gov.eg/api/v1.0/documents/state/" + UUID + "/state");
		if (Info._setting.ProdEnv)
		{
			restClient = new RestClient("https://api.invoicing.eta.gov.eg/api/v1.0/documents/state/" + UUID + "/state");
		}
		RestRequest restRequest = new RestRequest(Method.PUT);
		restRequest.AddHeader("Content-Type", "application/json");
		restRequest.AddHeader("Authorization", "Bearer " + GetAccess().access_token);
		cancledoc cancledoc2 = new cancledoc();
		cancledoc2.status = "cancelled";
		cancledoc2.reason = reason;
		string value = JsonConvert.SerializeObject(cancledoc2, jss);
		restRequest.AddParameter("application/json", value, ParameterType.RequestBody);
		IRestResponse restResponse = restClient.Execute(restRequest);
		ErrorResponce errorResponce = new ErrorResponce();
		if (restResponse.StatusCode != HttpStatusCode.OK)
		{
			errorResponce = JsonConvert.DeserializeObject<ErrorResponce>(restResponse.Content);
		}
		return errorResponce;
	}

	public ErrorResponce RejectDocument(string UUID, string reason)
	{
		RestClient restClient = new RestClient("https://api.preprod.invoicing.eta.gov.eg/api/v1.0/documents/state/" + UUID + "/state");
		RestRequest restRequest = new RestRequest(Method.PUT);
		restRequest.AddHeader("Content-Type", "application/json");
		restRequest.AddHeader("Authorization", "Bearer {{generatedAccessToken}}");
		cancledoc cancledoc2 = new cancledoc();
		cancledoc2.status = "rejected";
		cancledoc2.reason = reason;
		string value = JsonConvert.SerializeObject(cancledoc2, jss);
		restRequest.AddParameter("application/json", value, ParameterType.RequestBody);
		IRestResponse restResponse = restClient.Execute(restRequest);
		ErrorResponce errorResponce = new ErrorResponce();
		if (restResponse.StatusCode != HttpStatusCode.OK)
		{
			errorResponce = JsonConvert.DeserializeObject<ErrorResponce>(restResponse.Content);
		}
		return errorResponce;
	}

	public PackageRequestResponse RequestPakage(PackageRequest packageRequest)
	{
		RestClient restClient = new RestClient("https://api.preprod.invoicing.eta.gov.eg/api/v1/documentPackages/requests");
		restClient.Timeout = -1;
		RestRequest restRequest = new RestRequest(Method.POST);
		restRequest.AddHeader("Authorization", "Bearer " + GetAccess().access_token);
		restRequest.AddHeader("Content-Type", "application/json");
		string value = JsonConvert.SerializeObject(packageRequest, jss);
		restRequest.AddParameter("application/json", value, ParameterType.RequestBody);
		IRestResponse restResponse = restClient.Execute(restRequest);
		PackageRequestResponse packageRequestResponse = new PackageRequestResponse();
		if (restResponse.StatusCode == HttpStatusCode.OK)
		{
			return JsonConvert.DeserializeObject<PackageRequestResponse>(restResponse.Content);
		}
		return JsonConvert.DeserializeObject<PackageRequestResponse>(restResponse.Content);
	}

	public PackageRequests GetPackageRequests()
	{
		RestClient restClient = ((!Info._setting.ProdEnv) ? new RestClient("https://api.preprod.invoicing.eta.gov.eg/api/v1/documentPackages/requests?pageSize=100&pageNo=1") : new RestClient("https://api.invoicing.eta.gov.eg/api/v1/documentPackages/requests?pageSize=100&pageNo=1"));
		RestRequest restRequest = new RestRequest(Method.GET);
		restRequest.AddHeader("Authorization", "Bearer " + GetAccess().access_token);
		IRestResponse restResponse = restClient.Execute(restRequest);
		PackageRequests packageRequests = new PackageRequests();
		if (restResponse.StatusCode == HttpStatusCode.OK)
		{
			return JsonConvert.DeserializeObject<PackageRequests>(restResponse.Content);
		}
		if (restResponse.StatusCode == HttpStatusCode.BadRequest)
		{
			return packageRequests;
		}
		return packageRequests;
	}

	public PrintInvoiceResponse PrintInvoice(string UUID)
	{
		RestClient restClient = ((!Info._setting.ProdEnv) ? new RestClient("https://api.preprod.invoicing.eta.gov.eg/api/v1/documents/" + UUID + "/pdf") : new RestClient("https://api.invoicing.eta.gov.eg/api/v1/documents/" + UUID + "/pdf"));
		RestRequest restRequest = new RestRequest(Method.GET);
		restRequest.AddHeader("Authorization", "Bearer " + GetAccess().access_token);
		IRestResponse restResponse = restClient.Execute(restRequest);
		PrintInvoiceResponse printInvoiceResponse = new PrintInvoiceResponse();
		if (restResponse.StatusCode == HttpStatusCode.OK)
		{
			printInvoiceResponse.Pdf = restResponse.RawBytes;
			printInvoiceResponse.Success = true;
		}
		else
		{
			printInvoiceResponse.Message = restResponse.Content;
		}
		return printInvoiceResponse;
	}

	public DownloadPackageResponse DownloadPackage(string rid)
	{
		RestClient restClient = ((!Info._setting.ProdEnv) ? new RestClient("https://api.preprod.invoicing.eta.gov.eg/api/v1/documentPackages/" + rid) : new RestClient("https://api.invoicing.eta.gov.eg/api/v1/documentPackages/" + rid));
		RestRequest restRequest = new RestRequest(Method.GET);
		restRequest.AddHeader("Authorization", "Bearer " + GetAccess().access_token);
		IRestResponse restResponse = restClient.Execute(restRequest);
		DownloadPackageResponse downloadPackageResponse = new DownloadPackageResponse();
		if (restResponse.StatusCode == HttpStatusCode.OK)
		{
			downloadPackageResponse.zip = restResponse.RawBytes;
		}
		else
		{
			downloadPackageResponse = JsonConvert.DeserializeObject<DownloadPackageResponse>(restResponse.Content);
		}
		return downloadPackageResponse;
	}

	public ItemCodeResponce CreateEGSCode(ItemCodesRoot itemCodes)
	{
		RestClient restClient = new RestClient("https://api.preprod.invoicing.eta.gov.eg/api/v1.0/codetypes/requests/codes");
		RestRequest restRequest = new RestRequest(Method.POST);
		restRequest.AddHeader("Authorization", "Bearer " + GetAccess().access_token);
		restRequest.AddHeader("Content-Type", "application/json");
		string value = JsonConvert.SerializeObject(itemCodes, jss);
		restRequest.AddParameter("application/json", value, ParameterType.RequestBody);
		IRestResponse restResponse = restClient.Execute(restRequest);
		ItemCodeResponce itemCodeResponce = new ItemCodeResponce();
		if (restResponse.StatusCode == HttpStatusCode.OK)
		{
			return JsonConvert.DeserializeObject<ItemCodeResponce>(restResponse.Content);
		}
		throw new Exception(restResponse.Content);
	}
}
