using BecauseImClever.AutomationModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BecauseImClever.AutomationUI.Pages
{
	public class IndexBase : ComponentBase
	{
		[CascadingParameter]
		protected List<Room> RoomList { get; set; }
	}
}
