﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAutomationRepositories.Entities
{
	public class DeviceEntity
	{
		[BsonId]
		public ObjectId Id { get; set; }
		public string Name { get; set; }
	}
}
