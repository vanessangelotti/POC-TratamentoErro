using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace POC_TratamentoErro
{
    public class Program
    {

        static void Main(string[] args)
        {

            //ToDo: ver se é necessario o array []
            EventGridEvent[] eventGridEvent = new EventGridEvent[3];

            //ToDo: Colocar em um arquivo de configuração ou variável de ambiente
            String _configuration = "colocar o connection String do Storage Account";

            try
            {
                PopularEventGrid(eventGridEvent);

                foreach (EventGridEvent amsEvent in eventGridEvent)
                {

                    if (amsEvent.EventType == EventTypes.EventHubCaptureFileCreatedEvent)
                    {
                        Data objData;
                        if(TryParse<Data>(amsEvent.Data.ToString(), out objData) && objData != null)
                        {
                            List<string> jsonEvents = StorageAccount.Dump(objData.fileUrl, _configuration);

                            foreach (string jsonEvent in jsonEvents)
                            {

                            }
                         
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        //ToDo: tranformar método em uma common (para todas as controllers) ou um nugget para todos os projetos.
        public static bool TryParse<T>(string input, out T obj)
        {
            bool result = true;
            obj = default;

            try
            {
                obj = JsonConvert.DeserializeObject<T>(input);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao deserializar a string para Json", ex);
                result = false;
            }

            return result;
       }

        private static void PopularEventGrid(EventGridEvent[] eventGridEvent)
        {
            eventGridEvent[0] = new EventGridEvent
            {
                Data = @"{'fileUrl': 'https://rpmgamorascd.blob.core.windows.net/azure-webjobs-hosts/rpm-gamora-eh2-d/rpm-gamora-tmfinancialbillingbillitem/0/2021/02/01/16/55/01.avro','roles': ['Developer', 'Administrator']}",
                EventType = EventTypes.EventHubCaptureFileCreatedEvent
            };

            eventGridEvent[1] = new EventGridEvent
            {
                Data = @"{'ContractId':6,'TransactionId':0,'VehicleAccounts':[],'OriginBlockId':'519b9766 - df26 - 41df - ab09 - 2a7cec5fc522','AutomaticBlockUnblock':2, 'fileUrl': 'https://rpmgamorascd.blob.core.windows.net/azure-webjobs-hosts/rpm-gamora-eh2-d/rpm-gamora-tmfinancialbillingbillitem/0/2021/02/01/16/55/01.avro'}",
                EventType = EventTypes.EventHubCaptureFileCreatedEvent
            };

            eventGridEvent[2] = new EventGridEvent
            {
                //JSON.Parse
                Data = @"{'name': 'Arnie Admin','roles': ['Developer', 'Administrator']},{'name': 'Arnie Admin','roles': ['Developer', 'Administrator']}",
                EventType = EventTypes.EventHubCaptureFileCreatedEvent
            };
        }
    }
}
