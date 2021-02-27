using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Hadoop.Avro.Container;

namespace POC_TratamentoErro
{
    public class StorageAccount
    {
        //ToDo: substituir as classe da microsoft depreciadas por:
        //Microsoft.Azure.Storage =  https://www.nuget.org/packages/Azure.Storage.Blobs

        public static List<string> Dump(Uri fileUri, string storageConnection)
        {
            List<string> listObjects = null;

            try
            {
                if (ValidateParameter(fileUri, storageConnection))
                {
                    var storageAccount = CloudStorageAccount.Parse(storageConnection);
                    var blobClient = storageAccount.CreateCloudBlobClient();
                    var blob = blobClient.GetBlobReferenceFromServer(fileUri);
                    using (var reader = AvroContainer.CreateGenericReader(blob.OpenRead()))
                    {
                        if (ValidateReader(reader))
                        {
                            listObjects = new List<string>();

                            while (reader.MoveNext())
                            {
                                foreach (dynamic record in reader.Current.Objects)
                                {
                                    listObjects.Add(Encoding.UTF8.GetString(record.Body));
                                }
                            }
                        }

                    }
                }
               
                //blob.DeleteAsync();
                return listObjects;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private static bool ValidateParameter(Uri fileUri, string storageConnection)
        {
            
            if (fileUri is null)
            {
                Console.WriteLine("URL do blob storage está vazia"); // Log de Erro
                return false;
            }

            if  (String.IsNullOrEmpty(storageConnection))
            {
                Console.WriteLine("URL do blob storage está vazia"); // Log de Erro
                return false;

            }
            return true;
        }

        private static bool ValidateReader(object reader)
        {

            if (reader is null)
            {
                Console.WriteLine("URL do blob storage está vazia"); // Log de Erro
                return false;
            }

            return true;
        }
    }
}
