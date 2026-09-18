using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using E_Invoice.Domain.Helpers;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ess;
using Org.BouncyCastle.Asn1.X509;

namespace E_Invoice.Domain.Services;

public class TokenSigner
{
	private static string DllLibPath = System.Configuration.ConfigurationManager.AppSettings["TokenDllPath"] ?? "eps2003csp11.dll";

	private static string TokenPin = Info._setting.TokenPass;

	public string PutSignatures(string Json)
	{
		JObject jObject = JsonConvert.DeserializeObject<JObject>(Json, new JsonSerializerSettings
		{
			NullValueHandling = NullValueHandling.Ignore,
			Formatting = Formatting.Indented,
			FloatFormatHandling = FloatFormatHandling.String,
			FloatParseHandling = FloatParseHandling.Decimal,
			DateFormatHandling = DateFormatHandling.IsoDateFormat,
			DateParseHandling = DateParseHandling.None
		});
		string text = Serialize(jObject);
		byte[] bytes = Encoding.UTF8.GetBytes(text);
		File.WriteAllBytes("CanonicalString.txt", Encoding.UTF8.GetBytes(text));
		string text2 = "";
		text2 = ((!(jObject["documentTypeVersion"].Value<string>() == "0.9")) ? SignWithCMS(text) : "ANY");
		string text3 = Encoding.UTF8.GetBytes(text2).ToString();
		JObject item = new JObject(new JProperty("signatureType", "I"), new JProperty("value", text2));
		JArray jArray = new JArray();
		jArray.Add(item);
		jObject.Add("signatures", jArray);
		string s = jObject.ToString();
		File.WriteAllBytes("FullSignedDocument.json", Encoding.UTF8.GetBytes(s));
		return s;
	}

	public static byte[] Hash(string input)
	{
		using SHA256 sHA = SHA256.Create();
		return sHA.ComputeHash(Encoding.UTF8.GetBytes(input));
	}

	public static byte[] HashBytes(byte[] input)
	{
		using SHA256 sHA = SHA256.Create();
		return sHA.ComputeHash(input);
	}

	public static string SignWithCMS(string serializedJson)
	{
		Pkcs11InteropFactories pkcs11InteropFactories = new Pkcs11InteropFactories();
		using IPkcs11Library pkcs11Library = pkcs11InteropFactories.Pkcs11LibraryFactory.LoadPkcs11Library(pkcs11InteropFactories, DllLibPath, AppType.MultiThreaded);
		ISlot slot = pkcs11Library.GetSlotList(SlotsType.WithTokenPresent).FirstOrDefault();
		if (slot == null)
		{
			return "No slots found";
		}
		ITokenInfo tokenInfo = slot.GetTokenInfo();
		ISlotInfo slotInfo = slot.GetSlotInfo();
		using ISession session = slot.OpenSession(SessionType.ReadWrite);
		session.Login(CKU.CKU_USER, Encoding.UTF8.GetBytes(TokenPin));
		X509Store x509Store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
		x509Store.Open(OpenFlags.MaxAllowed);
		X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindByIssuerName, "Egypt Trust Sealing CA", validOnly: false);
		if (x509Certificate2Collection.Count == 0)
		{
			return "no device detected";
		}
		X509Certificate2 x509Certificate = x509Certificate2Collection[0];
		x509Store.Close();
		byte[] bytes = Encoding.UTF8.GetBytes(serializedJson);
		ContentInfo contentInfo = new ContentInfo(new Oid("1.2.840.113549.1.7.5"), bytes);
		SignedCms signedCms = new SignedCms(contentInfo, detached: true);
		HashAlgorithm hashAlgorithm = SHA256.Create();
		byte[] certHash = hashAlgorithm.ComputeHash(x509Certificate.RawData);
		EssCertIDv2 essCertIDv = new EssCertIDv2(new Org.BouncyCastle.Asn1.X509.AlgorithmIdentifier(new DerObjectIdentifier("1.2.840.113549.1.9.16.2.47")), certHash);
		SigningCertificateV2 signingCertificateV = new SigningCertificateV2(new EssCertIDv2[1] { essCertIDv });
		CmsSigner cmsSigner = new CmsSigner(x509Certificate);
		cmsSigner.DigestAlgorithm = new Oid("2.16.840.1.101.3.4.2.1");
		cmsSigner.SignedAttributes.Add(new Pkcs9SigningTime(DateTime.UtcNow));
		cmsSigner.SignedAttributes.Add(new AsnEncodedData(new Oid("1.2.840.113549.1.9.16.2.47"), signingCertificateV.GetEncoded()));
		signedCms.ComputeSignature(cmsSigner);
		byte[] inArray = signedCms.Encode();
		return Convert.ToBase64String(inArray);
	}

	public static string Serialize(JObject request)
	{
		return SerializeToken(request);
	}

	public static string SerializeToken(JToken request)
	{
		string text = "";
		if (request.Parent == null)
		{
			SerializeToken(request.First);
		}
		else
		{
			if (request.Type == JTokenType.Property)
			{
				string text2 = ((JProperty)request).Name.ToUpper();
				text = text + "\"" + text2 + "\"";
				foreach (JToken item in (IEnumerable<JToken>)request)
				{
					if (item.Type == JTokenType.Object)
					{
						text += SerializeToken(item);
					}
					if (item.Type == JTokenType.Boolean || item.Type == JTokenType.Integer || item.Type == JTokenType.Float || item.Type == JTokenType.Date)
					{
						text = text + "\"" + item.Value<string>() + "\"";
					}
					if (item.Type == JTokenType.String)
					{
						text += JsonConvert.ToString(item.Value<string>());
					}
					if (item.Type != JTokenType.Array)
					{
						continue;
					}
					foreach (JToken item2 in item.Children())
					{
						text = text + "\"" + ((JProperty)request).Name.ToUpper() + "\"";
						text += SerializeToken(item2);
					}
				}
			}
			if (request.Type == JTokenType.String)
			{
				text += JsonConvert.ToString(request.Value<string>());
			}
		}
		if (request.Type == JTokenType.Object)
		{
			foreach (JToken item3 in request.Children())
			{
				if (item3.Type == JTokenType.Object || item3.Type == JTokenType.Property)
				{
					text += SerializeToken(item3);
				}
			}
		}
		return text;
	}

	public static void ListCertificates()
	{
		X509Store x509Store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
		x509Store.Open(OpenFlags.MaxAllowed);
		X509Certificate2Collection certificates = x509Store.Certificates;
		X509Certificate2Collection x509Certificate2Collection = certificates.Find(X509FindType.FindBySerialNumber, "2b1cdda84ace68813284519b5fb540c2", validOnly: true);
		X509Certificate2Enumerator enumerator = x509Certificate2Collection.GetEnumerator();
		while (enumerator.MoveNext())
		{
			X509Certificate2 current = enumerator.Current;
			try
			{
				byte[] rawData = current.RawData;
				Console.WriteLine("Content Type: {0}{1}", X509Certificate2.GetCertContentType(rawData), Environment.NewLine);
				Console.WriteLine("Friendly Name: {0}{1}", current.FriendlyName, Environment.NewLine);
				Console.WriteLine("Certificate Verified?: {0}{1}", current.Verify(), Environment.NewLine);
				Console.WriteLine("Simple Name: {0}{1}", current.GetNameInfo(X509NameType.SimpleName, forIssuer: true), Environment.NewLine);
				Console.WriteLine("Signature Algorithm: {0}{1}", current.SignatureAlgorithm.FriendlyName, Environment.NewLine);
				Console.WriteLine("Public Key: {0}{1}", current.PublicKey.Key.ToXmlString(includePrivateParameters: false), Environment.NewLine);
				Console.WriteLine("Certificate Archived?: {0}{1}", current.Archived, Environment.NewLine);
				Console.WriteLine("Length of Raw Data: {0}{1}", current.RawData.Length, Environment.NewLine);
				current.Reset();
			}
			catch (CryptographicException ex)
			{
				Console.WriteLine("Information could not be written out for this certificate.");
				throw ex;
			}
		}
		x509Store.Close();
	}
}
