using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using E_Invoice.Domain.Helpers;

namespace E_Invoice.Domain.Services
{
    public class TokenSigner
    {
        static string DllLibPath = "eps2003csp11.dll";


        static string TokenPin = Info._setting.TokenPass;
        //public string TokenPin = "";
        public string PutSignatures(string Json)
        {

            //     //   TokenSigner tokenSigner = new Class.TokenSigner();
            JObject request = JsonConvert.DeserializeObject<JObject>(Json, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                FloatFormatHandling = FloatFormatHandling.String,
                FloatParseHandling = FloatParseHandling.Decimal,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateParseHandling = DateParseHandling.None
            });

            //        //Start serialize
            String canonicalString = Serialize(request);
            byte[] strCanonicalStringBytes = System.Text.Encoding.UTF8.GetBytes(canonicalString);
                   File.WriteAllBytes( @"CanonicalString.txt", System.Text.Encoding.UTF8.GetBytes(canonicalString));
            //        // retrieve cades
            String cades = "";
            if (request["documentTypeVersion"].Value<string>() == "0.9")
            {
                cades = "ANY";
            }
            else
            {
                cades = SignWithCMS(canonicalString);
            }
            string strCades = System.Text.Encoding.UTF8.GetBytes(cades).ToString();

            JObject signaturesObject = new JObject(
                                   new JProperty("signatureType", "I"),
                                   new JProperty("value", cades));
            JArray signaturesArray = new JArray();
            signaturesArray.Add(signaturesObject);
            request.Add("signatures", signaturesArray);

            //string strFullSignedDocument = System.Text.Encoding.UTF8.GetBytes(request.ToString()).ToString();
            string strFullSignedDocument = request.ToString();
            File.WriteAllBytes(@"FullSignedDocument.json", System.Text.Encoding.UTF8.GetBytes(strFullSignedDocument));
            return strFullSignedDocument;
        }


        public static byte[] Hash(string input)
        {
            using (SHA256 sha = SHA256.Create())
            {
                var output = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
                return output;
            }
        }
        public static byte[] HashBytes(byte[] input)
        {
            using (SHA256 sha = SHA256.Create())
            {
                var output = sha.ComputeHash(input);
                return output;
            }
        }
        public static string SignWithCMS(String serializedJson)
        {
            //byte[] data = Encoding.UTF8.GetBytes(serializedJson);
            Pkcs11InteropFactories factories = new Pkcs11InteropFactories();
            using (IPkcs11Library pkcs11Library = factories.Pkcs11LibraryFactory.LoadPkcs11Library(factories, DllLibPath, AppType.MultiThreaded))
            {
                ISlot slot = pkcs11Library.GetSlotList(SlotsType.WithTokenPresent).FirstOrDefault();

                if (slot == null)
                {
                    return "No slots found";
                }

                ITokenInfo tokenInfo = slot.GetTokenInfo();

                ISlotInfo slotInfo = slot.GetSlotInfo();


                using (var session = slot.OpenSession(SessionType.ReadWrite))
                {
                    //your previous code is ok but this is just more improvement
                    // did you knew where is the proplem no problems here lets check other 
                    session.Login(CKU.CKU_USER, Encoding.UTF8.GetBytes(TokenPin));

                    //var certificateSearchAttributes = new List<IObjectAttribute>()
                    //{
                    //    session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_CERTIFICATE),
                    //    session.Factories.ObjectAttributeFactory.Create(CKA.CKA_TOKEN, true),
                    //    session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CERTIFICATE_TYPE, CKC.CKC_X_509)
                    //};

                    //IObjectHandle certificate = session.FindAllObjects(certificateSearchAttributes).FirstOrDefault();

                    //if (certificate == null)
                    //{
                    //    return "Certificate not found";
                    //}

                    X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                    store.Open(OpenFlags.MaxAllowed);

                    // find cert by thumbprint  //do tou use egypt trust token? yes ok
                    var foundCerts = store.Certificates.Find(X509FindType.FindByIssuerName, "Egypt Trust CA G6", false);

                    //var foundCerts = store.Certificates.Find(X509FindType.FindBySerialNumber, "2b1cdda84ace68813284519b5fb540c2", true);

                    if (foundCerts.Count == 0)
                        return "no device detected";

                    var certForSigning = foundCerts[0];
                    store.Close();

                    // this code is ok====================================================================
                    byte[] byteData = Encoding.UTF8.GetBytes(serializedJson);
                    ContentInfo content = new ContentInfo(new Oid("1.2.840.113549.1.7.5"), byteData);
                    SignedCms cms = new SignedCms(content, true);
                    HashAlgorithm algorithm = SHA256.Create();
                    byte[] certBytes = algorithm.ComputeHash(certForSigning.RawData);
                    EssCertIDv2 bouncyCertificate = new EssCertIDv2(
                        new Org.BouncyCastle.Asn1.X509.AlgorithmIdentifier(
                            new DerObjectIdentifier("1.2.840.113549.1.9.16.2.47")), certBytes);

                    SigningCertificateV2 signerCertificateV2 = new SigningCertificateV2(new EssCertIDv2[] { bouncyCertificate });
                    CmsSigner signer = new CmsSigner(certForSigning);
                    signer.DigestAlgorithm = new Oid("2.16.840.1.101.3.4.2.1");
                    signer.SignedAttributes.Add(new Pkcs9SigningTime(DateTime.UtcNow));
                    signer.SignedAttributes.Add(new AsnEncodedData(new Oid("1.2.840.113549.1.9.16.2.47"), signerCertificateV2.GetEncoded()));

                    cms.ComputeSignature(signer);

                    var output = cms.Encode();

                    return Convert.ToBase64String(output);
                }
            }
        }
        public static string Serialize(JObject request)
        {
            return SerializeToken(request);
        }
        public static string SerializeToken(JToken request)
        {
            string serialized = "";
            if (request.Parent == null)
            {
                SerializeToken(request.First);
            }
            else
            {

                if (request.Type == JTokenType.Property)
                {
                    string name = ((JProperty)request).Name.ToUpper();
                    serialized += "\"" + name + "\"";
                    foreach (var property in request)
                    {
                        if (property.Type == JTokenType.Object)
                        {
                            serialized += SerializeToken(property);
                        }
                        if (property.Type == JTokenType.Boolean || property.Type == JTokenType.Integer || property.Type == JTokenType.Float || property.Type == JTokenType.Date)
                        {
                            serialized += "\"" + property.Value<string>() + "\"";
                        }
                        if (property.Type == JTokenType.String)
                        {
                            serialized += JsonConvert.ToString(property.Value<string>());
                        }
                        if (property.Type == JTokenType.Array)
                        {
                            foreach (var item in property.Children())
                            {
                                serialized += "\"" + ((JProperty)request).Name.ToUpper() + "\"";
                                serialized += SerializeToken(item);
                            }
                        }
                    }
                }
                if (request.Type == JTokenType.String)
                {
                    serialized += JsonConvert.ToString(request.Value<string>());
                }
            }
            if (request.Type == JTokenType.Object)
            {
                foreach (var property in request.Children())
                {

                    if (property.Type == JTokenType.Object || property.Type == JTokenType.Property)
                    {
                        serialized += SerializeToken(property);
                    }
                }
            }

            return serialized;
        }
        public static void ListCertificates()
        {

            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.MaxAllowed);
            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection fcollection = (X509Certificate2Collection)collection.Find(X509FindType.FindBySerialNumber, "2b1cdda84ace68813284519b5fb540c2", true);
            foreach (X509Certificate2 x509 in fcollection)
            {
                try
                {
                    byte[] rawdata = x509.RawData;
                    Console.WriteLine("Content Type: {0}{1}", X509Certificate2.GetCertContentType(rawdata), Environment.NewLine);
                    Console.WriteLine("Friendly Name: {0}{1}", x509.FriendlyName, Environment.NewLine);
                    Console.WriteLine("Certificate Verified?: {0}{1}", x509.Verify(), Environment.NewLine);
                    Console.WriteLine("Simple Name: {0}{1}", x509.GetNameInfo(X509NameType.SimpleName, true), Environment.NewLine);
                    Console.WriteLine("Signature Algorithm: {0}{1}", x509.SignatureAlgorithm.FriendlyName, Environment.NewLine);
                    Console.WriteLine("Public Key: {0}{1}", x509.PublicKey.Key.ToXmlString(false), Environment.NewLine);
                    Console.WriteLine("Certificate Archived?: {0}{1}", x509.Archived, Environment.NewLine);
                    Console.WriteLine("Length of Raw Data: {0}{1}", x509.RawData.Length, Environment.NewLine);
                    x509.Reset();
                }
                catch (CryptographicException ex)
                {
                    Console.WriteLine("Information could not be written out for this certificate.");
                    throw ex;
                }
            }
            store.Close();
        }
    }
}
