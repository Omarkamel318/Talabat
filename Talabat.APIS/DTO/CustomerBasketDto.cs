﻿using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities;

namespace Talabat.APIS.DTO
{
	public class CustomerBasketDto
	{
		[Required]
		public string Id { get; set; }
		[Required]
		public List<BasketItemDto> Items { get; set; }
	}
}
