using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel.Syndication;

namespace konusarakogren.DAL
{
	public class WiredDAL
	{
		public List<Entity.Wired> GetRSS()
		{
			List<Entity.Wired> wiredPosts = new List<Entity.Wired>();

			string url = "https://www.wired.com/feed/rss";
			XmlReader reader = XmlReader.Create(url);
			SyndicationFeed feed = SyndicationFeed.Load(reader);
			reader.Close();

			int counter = 0;
			foreach (var item in feed.Items)
			{
				Entity.Wired wired = new Entity.Wired();
				wired.Title = item.Title.Text;
				wired.Description = item.Summary.Text;
				wired.Id = counter;
				wiredPosts.Add(wired);
				counter++;
				if (counter >= 5)
					break;
			}
			return wiredPosts;
		}

	}
}
