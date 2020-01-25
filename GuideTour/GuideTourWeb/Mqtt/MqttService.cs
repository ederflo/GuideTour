using GuideTourData.Models;
using GuideTourData.Services;
using GuideTourLogic.Logics;
using GuideTourWeb.Helpers.ObjectHelpers;
using GuideTourWeb.Hubs;
using GuideTourWeb.Models.MqttModels;
using GuideTourWeb.Models.TourViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace GuideTourWeb.Mqtt
{
    public class MqttService
    {
        public static readonly string StartUrl = "tdot/tours/started";
        public static readonly string StartAckUrl = "tdot/tours/startedACK";
        public static readonly string EndedAckUrl = "tdot/tours/endedACK";
        public static readonly string CanceldAck = "tdot/tours/canceldACK";

        private static readonly string[] topics = { StartUrl };
        private static readonly string brokerUrl = "guide.informatik.app";
        private static readonly string clientId = "GuideTour";
        private static MqttService instance = null;
        private static readonly object padlock = new object();

        private readonly IDocumentDbRepository _ddb;
        private readonly IHubContext<TourHub> _hubcontext;

        public readonly MqttClient client;

        public static MqttService Instance {
            get {
                lock(padlock)
                {
                    return instance;
                }
            }

            set {
                instance = value;
            }
        }

        public MqttService(IDocumentDbRepository ddb, IHubContext<TourHub> hubcontext)
        {
            try
            {
                client = new MqttClient(brokerUrl);
                client.MqttMsgPublishReceived += MqttMsgPublishReceivedAsync;
                client.Connect(clientId);
                _ddb = ddb;
                _hubcontext = hubcontext;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        public static void Init(IDocumentDbRepository ddb, IHubContext<TourHub> hubcontext)
        {
            if (instance == null)
                instance = new MqttService(ddb, hubcontext);
            Instance.client.Subscribe(topics, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        public async void MqttMsgPublishReceivedAsync(object sender, MqttMsgPublishEventArgs e)
        {
            TourLogic tourLogic = new TourLogic(_ddb);
            GuideLogic guideLogic = new GuideLogic(_ddb);
            TeamLogic teamLogic = new TeamLogic(_ddb);
            try
            {
                if (e.Topic.Equals(StartUrl))
                {
                    string jsonString = Encoding.UTF8.GetString(e.Message);
                    TourIfGuideAppModel tourApp = JsonConvert.DeserializeObject<TourIfGuideAppModel>(jsonString);

                    Guide g = await guideLogic.GetByEmail(tourApp.GuideMail);
                    if (g != null)
                    {
                        Team t = await teamLogic.Get(g.TeamId);
                        if (t != null)
                        {
                            await tourLogic.Add(TourLogic.NewTour(g.Id, null, null, tourApp.IfGuideAppId));
                            Tour tour = await tourLogic.GetByIfGuideAppId(tourApp.IfGuideAppId);
                            if (tour != null && tour.StartedTour == null && tour.GuideId == g.Id)
                            {
                                TourViewModel tourVM = TourHelper.ToViewModel(tour, g, t);
                                await _hubcontext.Clients.All.SendAsync("NewRequestedTour", tourVM);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
