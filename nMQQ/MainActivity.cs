using Android.App;
using Android.Widget;
using Android.OS;
using uPLibrary.Networking.M2Mqtt;
using System;
using System.Net;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text;
using Android.App;

namespace nMQQ
{
	[Activity(Label = "nMQQ", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			// Xamarin.Insights.Initialize(global::nMQQ.XamarinInsights.ApiKey, this);
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it



			Button button = FindViewById<Button>(Resource.Id.myButton);




			MqttClient client = new MqttClient("192.168.0.12");

			//			byte code = client.Connect (Guid.NewGuid ().ToString ());
			client.ProtocolVersion = MqttProtocolVersion.Version_3_1;

			byte code = client.Connect(Guid.NewGuid().ToString(), null, null,
							true, // will retain flag
							MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, // will QoS
							true, // will flag
							"/will_topic", // will topic
							"will_message", // will message
							true,
							60);

			client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

			button.Click += delegate
			{

				client.MqttMsgPublished += client_MqttMsgPublished;
				//				ushort msgId = client.Publish ("/home/XOne", // topic
				//					               Encoding.UTF8.GetBytes ("21"), // message body
				//					               MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, // QoS level
				//					               true); // retained



				//ushort msgIdX = client.Subscribe (new string[] { "/home/XOne", "/home/XOne" },



				ushort msgIdX = client.Subscribe(new string[] { "outTopic", "outTopic" },

									new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,
						MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE
					});

				client.MqttMsgSubscribed += Client_MqttMsgSubscribed;



				//client.Subscribe(new string[] { "/home/XOne" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

				try
				{

					//					client.ProtocolVersion = MqttProtocolVersion.Version_3_1;
					//					client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
					//					client.MqttMsgSubscribed += Client_MqttMsgSubscribed;
					//
					//
					//					client.Subscribe (new string[] { "/home/XOne" }, new byte[] {
					//						MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,
					//						MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE
					//					});





				}
				catch
				{
					Console.WriteLine("Something wrong");
				}





				//ushort msgId = client.Publish("home/XOne", // topic
				//			  Encoding.UTF8.GetBytes("MyMessageBody"), // message body
				//			  MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, // QoS level
				//			  true); // retained






				//MqttMsgPublish publish = new MqttMsgPublish(e.Topic, e.Message, false, e.QosLevel, e.Retain);

				//ushort msgId = client.Subscribe(new string[] { "/home/XOne", },
				//								new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,
				//			 MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });




				//client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;



				/*
				byte code = client.Connect(Guid.NewGuid().ToString());

				//client.ProtocolVersion = MqttProtocolVersion.Version_3_1;



				//client.MqttMsgPublished += client_MqttMsgPublished;

				//ushort msgId = client.Publish("/home/temperature", // topic
				//							  Encoding.UTF8.GetBytes("25"), // message body
				//							  MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, // QoS level
				//							  false); // retained




				//ushort msgId = client.Publish("/home/temperature", // topic
				//							  Encoding.UTF8.GetBytes("MyMessageBody"), // message body
				//							  MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, // QoS level
				//							  true); // retained

				ushort msgId = client.Subscribe(new string[] { "/home/temperature", "/topic_2" },
												new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,
							 MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

				client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;


				/*


				string[] topic = { "home/neo" };

				var x = client.Unsubscribe(topic);




				client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

				client.Connect(Guid.NewGuid().ToString());

				//string[] topic = { "sensor/temp", "sensor/humidity" };


				client.Publish("neo", Encoding.UTF8.GetBytes("halo"));

				string[] topics = { "neo" };


				client.Unsubscribe(topics);
				var f = client.Unsubscribe(topics);




				Console.Write(f);



				/*


					string[] topic = { "neo" };

					byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE };
					var df = client.Subscribe(topic, qosLevels);


					ushort msgId = client.Publish("/neo", // topic
												  Encoding.UTF8.GetBytes("MyMessageBody"), // message body
												  MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, // QoS level
												  true); // retained



					ushort msgId2 = client.Publish("neo", // topic
												  Encoding.UTF8.GetBytes("MyMessageBody"), // message body
												  MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, // QoS level
												  true); // retained


					var msgId3 = client.Subscribe(new string[] { "/neo", "neo" },
													new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,            
								 MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });



					Console.ReadLine();

	*/


				//Console.Write(df);

			};


		}

		void Client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
		{
			Console.WriteLine("Subscribed for id = " + e.MessageId);


		}

		void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
		{
			MqttClient client = (MqttClient)sender;

			// create PUBLISH message to publish
			// [v3.1.1] DUP flag from an incoming PUBLISH message is not propagated to subscribers
			//          It should be set in the outgoing PUBLISH message based on transmission for each subscriber
			MqttMsgPublish publish = new MqttMsgPublish(e.Topic, e.Message, false, e.QosLevel, e.Retain);

			// publish message through publisher manager
			//this.publisherManager.Publish(publish);
		}



		void client_MqttMsgSubscribed(object sender, MqttMsgSuback e)
		{
			Console.WriteLine("Subscribed for id = " + e.MessageId);
		}



		void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
		{
			var doX = "Received = " + Encoding.UTF8.GetString(e.Message) + " on topic " + e.Topic;

			Console.WriteLine("Received = " + Encoding.UTF8.GetString(e.Message) + " on topic " + e.Topic);
		}


		void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
		{
			Console.WriteLine("MessageId = " + e.MessageId + " Published = " + e.IsPublished);
		}


	}

}

//		void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
//		{
//			try
//			{
//				Console.Write(e.Message);


//			}
//			catch (Exception ex)
//			{

//			}
//		}
//	}
//}