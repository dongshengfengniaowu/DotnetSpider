﻿using DotnetSpider.Core;
using DotnetSpider.Core.Selector;
using DotnetSpider.Extension;
using DotnetSpider.Extension.Model;
using DotnetSpider.Extension.Model.Attribute;
using DotnetSpider.Extension.Pipeline;
using System;
using System.Collections.Generic;

namespace DotnetSpider.Sample.docs
{
	public class CtripCitySpider : EntitySpider
	{
		public CtripCitySpider() : base(new Site
		{
			Headers = new Dictionary<string, string>
				{
					{"Cache-Control","max-age=0" },
					{"Upgrade-Insecure-Requests","1" },
					{"Accept-Encoding","gzip, deflate, sdch" },
					{"Accept-Language","zh-CN,zh;q=0.8" }
				},
			UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36",
			Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8"
		})
		{
		}

		protected override void MyInit(params string[] arguments)
		{
			AddStartUrl("http://www.ctrip.com/");
			AddEntityType<CtripCity>();
			AddPipeline(new ConsoleEntityPipeline());
		}

		[TableInfo("ctrip", "city", Uniques = new[] { "city_id,run_id" })]
		[EntitySelector(Expression = "//div[@class='city_item']//a")]
		class CtripCity
		{
			[Field(Expression = ".", Length = 100)]
			public string name { get; set; }

			[Field(Expression = "./@title", Length = 100)]
			public string title { get; set; }

			[Field(Expression = "./@data-id", Length = 100)]
			public string city_id { get; set; }

			[Field(Expression = "Today", Type = SelectorType.Enviroment)]
			public DateTime run_id { get; set; }
		}
	}
}